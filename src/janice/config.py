from __future__ import annotations
from dataclasses import dataclass

@dataclass(frozen=True)
class RunConfig:
    adversarial: bool = False
