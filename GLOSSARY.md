# Janice Glossary

**Action** — A tool invocation executed by the Executor.  
**Adversarial Mode** — Proposer generates risky steps to test policy.  
**Audit Log** — Append‑only, hash‑chained ledger of proposals/decisions/executions.  
**Capability Token** — Signed, short‑lived permission scope for a tool.  
**Canonical JSON** — Deterministic JSON used for hashing/signing.  
**Constitution** — Operator policy rules (`constitution.yaml`).  
**Context** — Session/environment flags passed to the Governor.  
**Executor** — Verifies approvals, runs tools, logs results.  
**Governor** — Enforces constitution and signs allow/deny decisions.  
**Learning Enhancements** — Future ability to auto‑create tools from unknown tasks.  
**Local‑First** — Keep processing and storage local by default.  
**Plugin Foundry** — Planned subsystem for tool generation and publishing.  
**Proposer** — Plans candidate steps for a task.  
**Scope** — Named permission (e.g., `fs.write:/scratch/**`).  
**Shadow Mode** — Evaluate new tools/plans without side effects.  
**STT/TTS** — Speech‑to‑text / Text‑to‑speech (`audio.listen` / `audio.speak`).  
**Tool** — Discrete capability with ID, version, schema, code hash.  
**Tool Registry** — Catalog of installed tools and metadata.  
**Tool Blueprint** — Spec that defines purpose, schema, scopes, tests.  
**Voicebox** — Voice I/O layer; still policy‑gated.
