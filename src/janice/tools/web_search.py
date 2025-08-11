from __future__ import annotations
from .base import ToolInfo

INFO = ToolInfo(
    id="web.search",
    version="1.0.0",
    arg_schema={"type":"object","properties":{"query":{"type":"string"},"domains":{"type":"array","items":{"type":"string"}}},"required":["query"]}
)

def run(*, args: dict, capability) -> dict:
    q = args["query"]
    doms = args.get("domains") or ["example.org"]
    return {"results": [{"title": f"About {q}", "domain": d, "url": f"https://{d}/?q={q}"} for d in doms]}
