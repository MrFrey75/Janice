from __future__ import annotations
from .base import ToolInfo

INFO = ToolInfo(
    id="audio.speak",
    version="1.0.0",
    arg_schema={
        "type":"object",
        "properties":{
            "text":{"type":"string"},
            "voice_id":{"type":"string"},
            "rate":{"type":"number"},
            "pitch":{"type":"number"},
            "style":{"type":"string"},
            "stream":{"type":"boolean"}
        },
        "required":["text"],
        "additionalProperties": False
    }
)

def run(*, args: dict, capability) -> dict:
    text = args["text"] if "text" in args else "(none)"
    return {"queued": True, "chars": len(text)}
