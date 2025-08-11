from __future__ import annotations
import os
import time
from dataclasses import dataclass
from .utils.canonical_json import to_canonical_bytes
from .crypto.signing import load_priv, generate, sign
from .crypto.capabilities import mint_capability
from .policy.constitution_loader import load_constitution
from .utils.json_logger import get_logger
from .audit.audit import record

log = get_logger()

@dataclass(frozen=True)
class Decision:
    payload: dict
    signature: bytes

def _ensure_keys():
    os.makedirs(".secrets", exist_ok=True)
    priv_path = ".secrets/governor_priv.key"
    pub_path = ".secrets/governor_pub.key"
    if not os.path.exists(priv_path) or not os.path.exists(pub_path):
        from .crypto.signing import save_priv, save_pub
        kp = generate()
        save_priv(kp.priv, priv_path)
        save_pub(kp.pub, pub_path)

def evaluate_and_sign(run_id: str, step_id: int, tool_id: str, args: dict, tool_code_hash: str) -> tuple[Decision, dict]:
    _ensure_keys()
    from nacl.signing import SigningKey
    priv: SigningKey = load_priv(".secrets/governor_priv.key")

    constitution, policy_hash = load_constitution()
    allow, msg = _evaluate_rules(constitution, tool_id, args)

    canonical_args_hash = "sha256:" + __import__("hashlib").sha256(to_canonical_bytes(args)).hexdigest()
    expiry = time.strftime("%Y-%m-%dT%H:%M:%SZ", time.gmtime(time.time() + 60))
    payload = {
        "run_id": run_id,
        "step_id": step_id,
        "governor_version": "0.2.0",
        "policy_hash": policy_hash,
        "tool_id": tool_id,
        "tool_code_hash": tool_code_hash,
        "canonical_args_hash": canonical_args_hash,
        "decision": "ALLOW" if allow else "DENY",
        "expiry": expiry,
        "nonce": os.urandom(16).hex(),
        "message": msg,
    }
    sig = sign(priv, to_canonical_bytes(payload))
    decision = Decision(payload, sig)
    record("decision", run_id, step_id, {"payload": payload, "sig": decision.signature.hex()})

    cap = None
    if allow:
        cap = mint_capability(priv, run_id=run_id, step_id=step_id, tool_id=tool_id, scopes=tuple(_derive_scopes(tool_id, args)), ttl_seconds=60)
    log.info("decision", run_id=run_id, step_id=step_id, allow=allow, reason=msg)
    return decision, {"capability": cap}

def _derive_scopes(tool_id: str, args: dict) -> list[str]:
    if tool_id.startswith("fs.write"):
        return [f"fs.write:{args.get('path','')}", "fs.bytes<=1000000"]
    if tool_id.startswith("web.search"):
        return ["web.search:any"]
    if tool_id.startswith("net.email"):
        return ["net.email:any"]
    if tool_id.startswith("audio.listen"):
        return ["mic.access","no_persist.audio"]
    if tool_id.startswith("audio.speak"):
        return ["speaker.output"]
    return ["unknown"]

def _evaluate_rules(doc: dict, tool_id: str, args: dict) -> tuple[bool, str]:
    from .utils.path_safe import SCRATCH_ROOT
    if tool_id.startswith("web.search"):
        q = args.get("query","")
        if len(q) <= 256:
            domains = args.get("domains")
            if not domains or all(d in ["example.org","arxiv.org","docs.example.com"] for d in domains):
                return True, "allow web.search"
    if tool_id.startswith("fs.write"):
        path = args.get("path","")
        try:
            import os as _os
            if _os.path.abspath(path).startswith(SCRATCH_ROOT + _os.sep) and len(args.get("bytes", b"")) <= 1_000_000:
                return True, "allow fs.write to scratch"
        except Exception:
            pass
    if tool_id.startswith("audio.listen"):
        mode = args.get("mode","push")
        if mode in ("push","wake"):
            return True, "allow audio.listen (stub)"
    if tool_id.startswith("audio.speak"):
        text = args.get("text","")
        if len(text) <= 1000:
            return True, "allow audio.speak (stub)"
    return False, "default deny"
