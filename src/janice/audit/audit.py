from __future__ import annotations
import os, json, hashlib, time
from dataclasses import dataclass, asdict
from typing import Optional

AUDIT_DIR = "audit"
os.makedirs(AUDIT_DIR, exist_ok=True)
AUDIT_FILE = os.path.join(AUDIT_DIR, "janice.log")

@dataclass
class AuditEntry:
    ts: float
    run_id: str
    step_id: int
    kind: str   # "proposal" | "decision" | "execution"
    data: dict
    prev_hash: Optional[str] = None
    hash: Optional[str] = None

def _hash_entry(e: AuditEntry) -> str:
    payload = json.dumps({
        "ts": e.ts, "run_id": e.run_id, "step_id": e.step_id, "kind": e.kind, "data": e.data, "prev_hash": e.prev_hash
    }, sort_keys=True, separators=(",", ":")).encode("utf-8")
    return hashlib.sha256(payload).hexdigest()

def append(entry: AuditEntry) -> str:
    prev = None
    try:
        with open(AUDIT_FILE, "rb") as f:
            f.seek(0, os.SEEK_END)
            size = f.tell()
            if size:
                f.seek(-min(size, 20000), os.SEEK_END)
                tail = f.read().splitlines()
                for line in reversed(tail):
                    if line.strip():
                        prev = json.loads(line)["hash"]
                        break
    except FileNotFoundError:
        pass
    entry.prev_hash = prev
    entry.hash = _hash_entry(entry)
    with open(AUDIT_FILE, "a", encoding="utf-8") as f:
        f.write(json.dumps(asdict(entry), separators=(",", ":")) + "\n")
    return entry.hash

def record(kind: str, run_id: str, step_id: int, data: dict):
    entry = AuditEntry(time.time(), run_id, step_id, kind, data)
    return append(entry)

def verify_chain() -> bool:
    prev = None
    if not os.path.exists(AUDIT_FILE):
        return True
    with open(AUDIT_FILE, "r", encoding="utf-8") as f:
        for line in f:
            if not line.strip(): continue
            item = json.loads(line)
            calc = _hash_entry(AuditEntry(item["ts"], item["run_id"], item["step_id"], item["kind"], item["data"], item.get("prev_hash"), None))
            if calc != item["hash"]: return False
            if item.get("prev_hash") != prev: return False
            prev = item["hash"]
    return True
