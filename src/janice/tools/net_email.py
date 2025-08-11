from __future__ import annotations
from .base import ToolInfo

INFO = ToolInfo(
    id="net.email",
    version="0.9.0",
    arg_schema={"type":"object","properties":{"to":{"type":"string"},"subject":{"type":"string"},"body":{"type":"string"}},"required":["to","subject","body"]}
)

def run(*, args: dict, capability) -> dict:
    return {"queued": True, "to": args["to"], "subject": args["subject"]}
