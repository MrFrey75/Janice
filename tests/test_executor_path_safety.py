import os, shutil, pytest
from janice.utils.path_safe import SCRATCH_ROOT, safe_write

def setup_module():
    os.makedirs(SCRATCH_ROOT, exist_ok=True)

def teardown_module():
    shutil.rmtree(SCRATCH_ROOT, ignore_errors=True)

def test_write_inside_scratch(tmp_path):
    p = os.path.join(SCRATCH_ROOT, "ok.txt")
    safe_write(p, b"hi")
    assert os.path.exists(p)

def test_write_outside_rejected(tmp_path):
    with pytest.raises(PermissionError):
        safe_write("/etc/passwd", b"nope")
