from __future__ import annotations
from dataclasses import dataclass
from typing import Tuple
from datetime import datetime, timezone, timedelta
from .signing import sign, verify
from ..utils.canonical_json import to_canonical_bytes

@dataclass(frozen=True)
class Capability:
    run_id: str
    step_id: int
    tool_id: str
    scopes: Tuple[str, ...]
    exp: str  # ISO8601
    sig: bytes

def mint_capability(priv, *, run_id: str, step_id: int, tool_id: str, scopes: Tuple[str, ...], ttl_seconds: int) -> Capability:
    exp = (datetime.now(timezone.utc) + timedelta(seconds=ttl_seconds)).isoformat()
    body = {"run_id": run_id, "step_id": step_id, "tool_id": tool_id, "scopes": list(scopes), "exp": exp}
    sig = sign(priv, to_canonical_bytes(body))
    return Capability(run_id, step_id, tool_id, scopes, exp, sig)

def check_capability(pub, cap: Capability) -> bool:
    body = {"run_id": cap.run_id, "step_id": cap.step_id, "tool_id": cap.tool_id, "scopes": list(cap.scopes), "exp": cap.exp}
    if datetime.fromisoformat(cap.exp) <= datetime.now(timezone.utc):
        return False
    return verify(pub, to_canonical_bytes(body), cap.sig)
