from __future__ import annotations
from dataclasses import dataclass

@dataclass(frozen=True)
class ToolInfo:
    id: str
    version: str
    arg_schema: dict
