from janice.crypto.signing import generate, sign, verify
from janice.utils.canonical_json import to_canonical_bytes

def test_sign_verify():
    kp = generate()
    payload = to_canonical_bytes({"x":1})
    sig = sign(kp.priv, payload)
    assert verify(kp.pub, payload, sig)
