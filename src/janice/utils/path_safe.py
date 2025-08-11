from __future__ import annotations
import os, errno

SCRATCH_ROOT = os.path.abspath("scratch")

def safe_path_join(*parts: str) -> str:
    path = os.path.abspath(os.path.join(SCRATCH_ROOT, *parts))
    if not path.startswith(SCRATCH_ROOT + os.sep):
        raise PermissionError("Path escapes scratch root")
    return path

def safe_write(path: str, data: bytes) -> None:
    path = safe_path_join(os.path.relpath(path, SCRATCH_ROOT))
    os.makedirs(os.path.dirname(path), exist_ok=True)
    fd = os.open(path + ".tmp", os.O_WRONLY | os.O_CREAT | os.O_TRUNC | getattr(os, "O_NOFOLLOW", 0), 0o600)
    try:
        with os.fdopen(fd, "wb") as f:
            f.write(data)
        os.replace(path + ".tmp", path)
    except Exception:
        try:
            os.unlink(path + ".tmp")
        except OSError as e:
            if e.errno != errno.ENOENT:
                raise
        raise
