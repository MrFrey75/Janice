from janice.utils.canonical_json import to_canonical_bytes

def test_canonical_stable_order():
    a = {"b": 1, "a": [2, {"z":"å","y":"a"}]}
    b = {"a": [{"y":"a","z":"å"}, 2], "b": 1}
    assert to_canonical_bytes(a) == to_canonical_bytes(b)
