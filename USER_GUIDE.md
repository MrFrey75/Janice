# User Guide

This guide explains how to use the `janice` command-line interface (CLI) to run tasks, manage security, and audit the system.

## Key Concepts

Janice operates on a "local-first, policy-governed" model. Here are the core components:

-   **Proposer**: Plans the steps for a given task.
-   **Governor**: Approves or denies each step based on the rules in `constitution.yaml`.
-   **Executor**: Executes approved steps after verifying the Governor's signature.
-   **Audit Log**: A tamper-evident log of all system activities.

For more details on the architecture, see the [README.md](README.md).

## CLI Commands

The `janice` CLI has the following commands:

### `gen-keys`

Generates cryptographic keys for the Governor.

```bash
janice gen-keys
```

This command creates a `.secrets` directory and saves the Governor's private and public keys inside.

To generate new keys, use the `--rotate` flag:

```bash
janice gen-keys --rotate
```

### `run`

Runs a task by proposing a series of steps, getting them approved by the Governor, and executing them.

```bash
janice run --task "Your task description here"
```

For example:

```bash
janice run --task "research 'safe sandboxes' and write summary"
```

### `audit-verify`

Verifies the integrity of the audit log.

```bash
janice audit-verify
```

This command checks the hash chain of the audit log to ensure it has not been tampered with. If the chain is valid, it will output "Audit chain OK". Otherwise, it will output "Audit chain BROKEN".

### `voice`

A stub for a voice interaction loop.

```bash
janice voice
```

This command is a placeholder for future voice-activated functionality.
