## Janice: A Guide for Gemini CLI

This document outlines how to use the `janice` command-line interface (CLI) to leverage the Janice framework, a **local-first, policy-governed automation** system.

-----

### 1\. Overview of Janice

Janice operates with a strict separation of concerns, ensuring that all actions are vetted and logged:

- **Proposer**: Plans potential actions or steps for a given task.
- **Governor**: Enforces the operator-defined policy (the **Constitution**) and signs decisions to allow or deny actions.
- **Executor**: Verifies the Governor's signed approvals and then executes the allowed actions.
- **Audit Log**: Maintains a hash-chained, tamper-evident log of all proposals, decisions, and executions.

-----

### 2\. Key Concepts

- **Constitution**: The policy ruleset, defined in `constitution.yaml`, that the Governor enforces.
- **Capability Token**: A signed, short-lived permission that grants a tool specific, scoped access (e.g., `fs.write:/scratch/**`).
- **Canonical JSON**: A deterministic JSON format used for hashing and signing payloads to ensure they are not altered.
- **Local-First**: The principle that processing and storage are kept local by default, with no "phoning home".

-----

### 3\. CLI Usage for Automation

The primary command for running a task is `janice run`.

```bash
janice run --task "research 'safe sandboxes' and write summary"
```

This command performs the following sequence of events:

1.  The **Proposer** creates a plan (a `proposal`) based on the task.
2.  The **Governor** evaluates each step of the proposal against the `constitution.yaml` rules.
3.  The Governor generates a signed decision for each step, which the **Executor** then verifies.
4.  If the decision is `ALLOW` and all cryptographic checks pass, the Executor runs the tool.

#### Auditing and Security

To verify the integrity of the audit log, you can use the `audit-verify` command:

```bash
janice audit-verify
```

This command checks the hash-chained log to detect any tampering.

-----

### 4\. Policy and Safety Enforcement

Janice's safety is built on a few core principles:

- **Signed Approvals**: The Executor will reject any action that does not have a valid signature from the Governor.
- **Canonical Args Hashing**: The Executor recalculates the hash of the tool's arguments and compares it to the one in the signed decision payload. If they do not match, it rejects the action, preventing any modification of arguments after approval.
- **Capability Tokens**: Tools are given short-lived tokens with specific scopes, such as `fs.write` to a specific path like `/scratch/**`. The Executor verifies that the capability is valid and not expired before running the tool.
- **Policy Rules**: The `constitution.yaml` file contains rules that can `allow` or `deny` tools based on conditions like `tool_id`, `args`, and `context`. For example, a rule allows `web.search` only if the query length is less than 256 characters and the domains are a specific allowlist. Another rule denies certain network egress tools while the microphone is active.

-----

### 5\. Data Transparency

Janice is designed with a strong focus on data privacy and transparency:

- **Required Data Collection**: Only task inputs, tool results, and audit metadata (run IDs, decisions, hashes, timestamps) are collected by default.
- **Local Storage**: The hash-chained audit log, tool registry metadata, and configuration files are stored locally.
- **Data That Never Leaves**: There is no "phoning home" functionality; external calls only occur through approved tools that pass constitutional checks. No data is exported unless explicitly done by the operator.
- **Privacy Controls**: Operators can edit the `constitution.yaml` to enforce specific policies and use redaction patterns to strip sensitive content from transcripts before they are logged.