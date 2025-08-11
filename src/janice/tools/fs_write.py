from __future__ import annotations
from ..utils.path_safe import safe_write
from .base import ToolInfo

INFO = ToolInfo(
    id="fs.write",
    version="2.1.0",
    arg_schema={"type":"object","properties":{"path":{"type":"string"},"bytes":{}},"required":["path","bytes"]}
)

def run(*, args: dict, capability) -> dict:
    path = args["path"]
    data = args["bytes"]
    if isinstance(data, str):
        data = data.encode("utf-8")
    safe_write(path, data)
    return {"ok": True, "written": len(data), "path": path}
