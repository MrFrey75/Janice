# Janice

**Janice** is a safety-first autonomous agent framework with a hard separation of concerns. It is designed to be **local-first** and **policy-governed**, ensuring that all actions are vetted, signed, and logged in a tamper-evident audit trail.

---

## Project Philosophy

The core philosophy of Janice is built on three pillars: **Safety**, **Transparency**, and **Separation of Concerns**.

1.  **Safety-First Design**: Every action is a transaction that must be explicitly approved and signed. The system uses a combination of signed approvals, capability tokens with limited scope and short TTLs, and canonical argument hashing to prevent unauthorized or modified actions. All file system operations are constrained to a designated "scratch" directory to prevent unintended side effects.

2.  **Transparency and Auditability**: All proposals, decisions, and executions are recorded in a hash-chained, tamper-evident audit log. This provides a verifiable record of everything the system has done. The `janice audit-verify` command allows an operator to confirm the integrity of this log at any time. Data handling is designed to be transparent, with clear policies on what is collected and stored.

3.  **Separation of Concerns**: The architecture strictly separates the main components to ensure a robust and secure workflow:
    *   **Proposer**: The creative engine. It analyzes a task and breaks it down into a series of discrete steps or actions for the system to take.
    *   **Governor**: The gatekeeper. It evaluates each proposed step against a human-readable policy file (`constitution.yaml`). If a step is permitted, the Governor signs it, creating a verifiable approval.
    *   **Executor**: The worker. It receives the signed approval, verifies its authenticity and integrity, and only then executes the action. It has no ability to act on its own.

This model ensures that the agent's capabilities are always constrained by operator-defined policy, and every action is auditable.

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

## Project Structure

Below is a more detailed breakdown of the repository structure and the purpose of key files and directories.

```
janice/
├─ README.md                  # This file: Project overview, philosophy, and quickstart.
├─ INSTALL.md                 # Detailed installation instructions.
├─ USER_GUIDE.md              # Guide to using the Janice CLI.
├─ constitution.yaml          # The core policy file enforced by the Governor.
├─ pyproject.toml             # Project metadata and Python dependencies.
├─ docs/                      # Contains all markdown documentation.
│  ├─ ...
├─ src/
│  └─ janice/
│     ├─ cli.py               # Defines the command-line interface (e.g., `run`, `audit-verify`).
│     ├─ proposer.py          # The Proposer component: plans actions.
│     ├─ governor.py          # The Governor component: evaluates and signs actions.
│     ├─ executor.py          # The Executor component: verifies and runs actions.
│     ├─ audit/
│     │  └─ audit.py          # Manages the append-only, hash-chained audit log.
│     ├─ crypto/
│     │  ├─ signing.py       # Handles cryptographic signing (Ed25519) and verification.
│     │  └─ capabilities.py  # Manages the creation and validation of capability tokens.
│     ├─ policy/
│     │  └─ constitution_loader.py # Loads and validates the constitution.yaml file.
│     ├─ tools/
│     │  └─ ...               # Contains all available tools (e.g., web search, file I/O).
│     └─ utils/
│        ├─ canonical_json.py # Ensures that JSON is serialized deterministically for hashing.
│        └─ path_safe.py      # Provides utilities for safe file system access.
└─ tests/
   └─ ...                      # Unit and integration tests for the framework.
```

---

## Documentation Index

- [README.md](README.md) — Project overview, quickstart, features, and architecture.
- [INSTALL.md](INSTALL.md) — Detailed installation instructions.
- [USER_GUIDE.md](USER_GUIDE.md) — How to use the `janice` CLI.
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
