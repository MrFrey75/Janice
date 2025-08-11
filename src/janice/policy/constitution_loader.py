from __future__ import annotations
import yaml
import hashlib

def load_constitution(path: str = "constitution.yaml") -> tuple[dict, str]:
    with open(path, "r", encoding="utf-8") as f:
        text = f.read()
    doc = yaml.safe_load(text)
    policy_hash = "sha256:" + hashlib.sha256(text.encode("utf-8")).hexdigest()
    return doc, policy_hash
