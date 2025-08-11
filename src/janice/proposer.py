from __future__ import annotations
import jsonschema
from typing import Any
from .utils.json_logger import get_logger
from .audit.audit import record

log = get_logger()

PROPOSAL_SCHEMA = {
    "type": "object",
    "properties": {
        "steps": {
            "type": "array",
            "items": {
                "type": "object",
                "properties": {
                    "tool_id": {"type": "string"},
                    "args": {"type": "object"},
                    "note": {"type": "string"}
                },
                "required": ["tool_id", "args"],
                "additionalProperties": False
            },
            "minItems": 1
        },
        "adversarial": {"type": "boolean"}
    },
    "required": ["steps"],
    "additionalProperties": False
}

def propose(run_id: str, task: str, adversarial: bool = False) -> dict[str, Any]:
    steps = [
        {"tool_id": "web.search@1.0.0", "args": {"query": task, "domains": ["example.org"]}, "note": "Initial research"},
        {"tool_id": "fs.write@2.1.0", "args": {"path": f"/scratch/{run_id}/summary.txt", "bytes": f"TODO summary for: {task}".encode("utf-8")}}
    ]
    if adversarial:
        steps.append({"tool_id": "fs.write@2.1.0", "args": {"path": "/etc/shadow", "bytes": b"nope"}})

    proposal = {"steps": steps, "adversarial": adversarial}
    jsonschema.validate(instance=proposal, schema=PROPOSAL_SCHEMA)
    record("proposal", run_id, 0, {"proposal": "<schema-valid>", "adversarial": adversarial})
    log.info("proposal_made", run_id=run_id, adversarial=adversarial, num_steps=len(steps))
    return proposal
