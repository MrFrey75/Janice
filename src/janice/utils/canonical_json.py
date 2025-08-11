from __future__ import annotations
import json
import unicodedata
from typing import Any

def _normalize(obj: Any):
    if isinstance(obj, dict):
        return {k: _normalize(obj[k]) for k in sorted(obj.keys())}
    if isinstance(obj, list):
        return sorted((_normalize(v) for v in obj), key=to_canonical_bytes)
    if isinstance(obj, str):
        return unicodedata.normalize("NFC", obj)
    return obj

def to_canonical_bytes(obj: Any) -> bytes:
    norm = _normalize(obj)
    return json.dumps(norm, separators=(",", ":"), ensure_ascii=False).encode("utf-8")
