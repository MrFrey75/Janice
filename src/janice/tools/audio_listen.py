from __future__ import annotations
from .base import ToolInfo
from datetime import datetime, timezone

INFO = ToolInfo(
    id="audio.listen",
    version="1.0.0",
    arg_schema={
        "type":"object",
        "properties":{
            "mode":{"type":"string","enum":["push","wake"]},
            "locale":{"type":"string"},
            "interim":{"type":"boolean"},
            "max_seconds":{"type":"integer"},
            "noise_suppression":{"type":"boolean"},
            "wake_word":{"type":"string"}
        },
        "required":["mode"],
        "additionalProperties": False
    }
)

def run(*, args: dict, capability) -> dict:
    ts = datetime.now(timezone.utc).isoformat()
    return {"events":[{"final":"(voice stub) hello world","ts": ts}]}
