from __future__ import annotations
import os
import typer
import uuid
import hashlib
import importlib
import inspect
from .proposer import propose
from .governor import evaluate_and_sign
from .executor import execute_step
from .audit.audit import verify_chain

app = typer.Typer(help="Janice CLI")

@app.command()
def gen_keys(rotate: bool = typer.Option(False, help="Rotate keys")):
    os.makedirs(".secrets", exist_ok=True)
    from .crypto.signing import generate, save_priv, save_pub
    priv_path = ".secrets/governor_priv.key"
    pub_path = ".secrets/governor_pub.key"
    if rotate or not (os.path.exists(priv_path) and os.path.exists(pub_path)):
        kp = generate()
        save_priv(kp.priv, priv_path)
        save_pub(kp.pub, pub_path)
        typer.echo("Keys generated.")
    else:
        typer.echo("Keys already exist. Use --rotate to replace.")

@app.command()
def run(task: str, adversarial: bool = False):
    run_id = uuid.uuid4().hex
    prop = propose(run_id, task, adversarial=adversarial)
    for i, step in enumerate(prop["steps"], start=1):
        tool_id = step["tool_id"]
        args = step["args"]
        module = importlib.import_module(f"janice.tools.{tool_id.split('.')[0]}_{tool_id.split('.')[1].split('@')[0]}")
        src = inspect.getsource(module)
        tool_code_hash = "sha256:" + hashlib.sha256(src.encode("utf-8")).hexdigest()
        decision, extras = evaluate_and_sign(run_id, i, tool_id, args, tool_code_hash)
        try:
            res = execute_step(run_id, i, decision.payload, decision.signature, tool_id, args, extras.get("capability"))
            typer.echo(f"Step {i} [{tool_id}]: {res}")
        except Exception as e:
            typer.echo(f"Step {i} FAILED: {e}")

@app.command("audit-verify")
def audit_verify():
    ok = verify_chain()
    typer.echo("Audit chain OK" if ok else "Audit chain BROKEN")

@app.command("voice")
def voice(ptt: bool = True):
    # Dry-run stub voice loop
    typer.echo("Voice stub starting...")
    run_id = uuid.uuid4().hex
    step = {"tool_id": "audio.listen@1.0.0", "args": {"mode": "push", "interim": True}}
    tool_code_hash = "sha256:stub"
    decision, extras = evaluate_and_sign(run_id, 1, step["tool_id"], step["args"], tool_code_hash)
    try:
        res = execute_step(run_id, 1, decision.payload, decision.signature, step["tool_id"], step["args"], extras.get("capability"))
        typer.echo(f"listen -> {res}")
    except Exception as e:
        typer.echo(f"listen FAILED: {e}")
