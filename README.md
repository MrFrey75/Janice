# Janice

**Janice** is a safety-first autonomous agent framework with a hard separation of concerns:

- **Proposer** — plans possible actions.
- **Governor** — enforces the constitution and signs approvals.
- **Executor** — verifies approvals, runs allowed actions, logs outcomes.
- **Voicebox** — (optional) voice interface layer with speech-to-text and text-to-speech tools.

All steps are **policy-gated**, **signed**, and **audit-logged**.

---

## Features

- **Signed Approvals** — Governor signs decisions with Ed25519.
- **Canonical Args Hashing** — ensures approved args are unmodified.
- **Capability Tokens** — short-lived, scope-bound permissions for tools.
- **Path-Safe Writes** — prevents filesystem escapes.
- **Audit Trail** — hash-chained, tamper-evident logs.
- **Voice Support** *(new!)* — `audio.listen` (STT) and `audio.speak` (TTS) tools with wake-word / push-to-talk gating.

---

## Quickstart

```bash
python -m venv .venv && source .venv/bin/activate
pip install -e .
mkdir -p scratch audit .secrets
janice gen-keys
janice run --task "research 'safe sandboxes' and write summary"
# or try stub voice mode
janice voice --ptt
```

---

## Repository Structure

```
janice/
├─ [README.md](README.md)                  # Project overview and quickstart
├─ [DATA_TRANSPARENCY.md](DATA_TRANSPARENCY.md)       # Full data handling and privacy details
├─ [LEARNING_ENHANCEMENTS.md](LEARNING_ENHANCEMENTS.md)   # Roadmap for autonomous learning capabilities
├─ [GLOSSARY.md](GLOSSARY.md)                # Definitions of key Janice terms and concepts
├─ [SECURITY.md](SECURITY.md)                # Security and privacy practices
├─ [POLICY.md](POLICY.md)                    # Policy model and constitution guidance
├─ [CONTRIBUTING.md](CONTRIBUTING.md)        # Contribution guidelines
├─ [constitution.yaml](constitution.yaml)          # Policy rules enforced by the Governor
├─ [pyproject.toml](pyproject.toml)              # Project metadata and dependencies
├─ [.gitignore](.gitignore)                  # Git ignore rules
├─ src/
│  └─ janice/
│     ├─ [__init__.py](src/janice/__init__.py)
│     ├─ [cli.py](src/janice/cli.py)               # CLI entrypoints (run, voice, audit-verify, etc.)
│     ├─ [config.py](src/janice/config.py)            # Runtime configuration dataclasses
│     ├─ [proposer.py](src/janice/proposer.py)          # Proposer component
│     ├─ [governor.py](src/janice/governor.py)          # Governor component
│     ├─ [executor.py](src/janice/executor.py)          # Executor component
│     ├─ audit/
│     │  └─ [audit.py](src/janice/audit/audit.py)
│     ├─ crypto/
│     │  ├─ [signing.py](src/janice/crypto/signing.py)
│     │  └─ [capabilities.py](src/janice/crypto/capabilities.py)
│     ├─ policy/
│     │  └─ [constitution_loader.py](src/janice/policy/constitution_loader.py)
│     ├─ tools/
│     │  ├─ [base.py](src/janice/tools/base.py)
│     │  ├─ [fs_write.py](src/janice/tools/fs_write.py)
│     │  ├─ [web_search.py](src/janice/tools/web_search.py)
│     │  ├─ [net_email.py](src/janice/tools/net_email.py)
│     │  ├─ [audio_listen.py](src/janice/tools/audio_listen.py)
│     │  └─ [audio_speak.py](src/janice/tools/audio_speak.py)
│     └─ utils/
│        ├─ [canonical_json.py](src/janice/utils/canonical_json.py)
│        ├─ [json_logger.py](src/janice/utils/json_logger.py)
│        └─ [path_safe.py](src/janice/utils/path_safe.py)
└─ tests/
   ├─ [test_canonical_json.py](tests/test_canonical_json.py)
   ├─ [test_signing_and_verification.py](tests/test_signing_and_verification.py)
   ├─ [test_executor_path_safety.py](tests/test_executor_path_safety.py)
   └─ [test_proposal_schema.py](tests/test_proposal_schema.py)
```
---

## Documentation Index

- [README.md](README.md) — Project overview, quickstart, features, and architecture.
- [DATA_TRANSPARENCY.md](DATA_TRANSPARENCY.md) — Full breakdown of what Janice collects, stores, and never stores; privacy controls.
- [LEARNING_ENHANCEMENTS.md](LEARNING_ENHANCEMENTS.md) — Roadmap for autonomous learning and plugin creation.
- [GLOSSARY.md](GLOSSARY.md) — Definitions of key terms and components used in Janice.
- [SECURITY.md](SECURITY.md) — Security model, signature scheme, and operator safeguards.
- [POLICY.md](POLICY.md) — Structure and usage of `constitution.yaml`; guidance for writing policy rules.
- [CONTRIBUTING.md](CONTRIBUTING.md) — Guidelines for contributing code, tools, and policy packs.
- [constitution.yaml](constitution.yaml) — The active policy ruleset enforced by the Governor.

---

## Voice Support

Janice can run in a voice loop using two tools:

- `audio.listen` — Speech‑to‑text from mic with interim and final transcripts.
- `audio.speak` — Text‑to‑speech output, streamable, with barge‑in support.

**Privacy defaults**: no raw audio is stored; transcripts are redacted before audit; risky egress tools can be denied while the mic is live.

---

## Data Transparency

*(see full [DATA_TRANSPARENCY.md](DATA_TRANSPARENCY.md) for details)*

- **What’s Collected**: Task inputs, tool results, audit metadata.
- **What’s Stored**: Hash‑chained audit logs, tool registry metadata, configuration.
- **Optional**: Raw audio or unredacted transcripts — only if explicitly enabled.
- **What’s Never Stored by Default**: Raw mic audio, sensitive data in plain form.
- **What Never Leaves Your System**: No phoning home; external calls only via approved tools that pass policy checks.

---

## Future Development

- **Autonomous Learning** — Unknown tasks → learn semantics → generate spec → scaffold plugin → test & register.
- **Plugin Foundry** — Safe subsystem for designing and deploying new tools.

Read the full [Learning Enhancements Roadmap](LEARNING_ENHANCEMENTS.md).
