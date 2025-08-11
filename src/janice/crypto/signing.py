from __future__ import annotations
from nacl import signing, exceptions
from nacl.encoding import RawEncoder
from dataclasses import dataclass

@dataclass(frozen=True)
class Keypair:
    priv: signing.SigningKey
    pub: signing.VerifyKey

def generate() -> Keypair:
    priv = signing.SigningKey.generate()
    return Keypair(priv, priv.verify_key)

def load_priv(path: str) -> signing.SigningKey:
    with open(path, "rb") as f:
        raw = f.read()
    return signing.SigningKey(raw, encoder=RawEncoder)

def load_pub(path: str) -> signing.VerifyKey:
    with open(path, "rb") as f:
        raw = f.read()
    return signing.VerifyKey(raw, encoder=RawEncoder)

def save_priv(key: signing.SigningKey, path: str) -> None:
    with open(path, "wb") as f:
        f.write(key.encode(RawEncoder))

def save_pub(key: signing.VerifyKey, path: str) -> None:
    with open(path, "wb") as f:
        f.write(key.encode(RawEncoder))

def sign(priv: signing.SigningKey, payload: bytes) -> bytes:
    return priv.sign(payload).signature

def verify(pub: signing.VerifyKey, payload: bytes, sig: bytes) -> bool:
    try:
        pub.verify(payload, sig)
        return True
    except exceptions.BadSignatureError:
        return False
