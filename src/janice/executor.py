from __future__ import annotations
import time
import importlib
import inspect
import hashlib
from typing import Any
from nacl.signing import VerifyKey
from .utils.canonical_json import to_canonical_bytes
from .utils.json_logger import get_logger
from .audit.audit import record
from .crypto.capabilities import check_capability
from .crypto.signing import load_pub

log = get_logger()

def _tool_code_hash(tool_module) -> str:
    src = inspect.getsource(tool_module)
    return "sha256:" + hashlib.sha256(src.encode("utf-8")).hexdigest()

def execute_step(run_id: str, step_id: int, decision: dict, signature: bytes, tool_id: str, args: dict, capability, *, now=None) -> Any:
    now = now or time.time()
    pub: VerifyKey = load_pub(".secrets/governor_pub.key")

    payload = to_canonical_bytes(decision)
    pub.verify(payload, signature)

    if decision["decision"] != "ALLOW":
        log.warning("skipping_denied", run_id=run_id, step_id=step_id)
        return None

    exp = time.mktime(time.strptime(decision["expiry"], "%Y-%m-%dT%H:%M:%SZ"))
    if now > exp:
        raise PermissionError("Decision expired")

    canon = to_canonical_bytes(args)
    calc_hash = "sha256:" + __import__("hashlib").sha256(canon).hexdigest()
    if calc_hash != decision["canonical_args_hash"]:
        raise PermissionError("Args changed since approval")

    mod_name = tool_id.split("@")[0]
    module = importlib.import_module(f"janice.tools.{mod_name.replace('.','_')}")
    if _tool_code_hash(module) != decision["tool_code_hash"] and decision["tool_code_hash"] != "sha256:stub":
        raise PermissionError("Tool code hash mismatch")

    if capability is not None and not check_capability(pub, capability):
        raise PermissionError("Invalid or expired capability")

    result = module.run(args=args, capability=capability)
    record("execution", run_id, step_id, {"tool": tool_id, "result": str(result)[:500]})
    log.info("executed", run_id=run_id, step_id=step_id, tool=tool_id)
    return result
