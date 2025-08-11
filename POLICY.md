# Policy Model

- Constitution is data: `constitution.yaml`.
- Rules are evaluated top‑to‑bottom; end with `default-deny`.
- Prefer allowlists for paths/domains/MIME; add rate limits for chatty tools.

## Voice Rules Summary
- Allow `audio.listen` only with push‑to‑talk or wake word active.
- Redact sensitive patterns from transcripts before audit.
- Deny raw audio persistence by default.
- Allow `audio.speak` unless in privacy `silent` mode.
- Optionally deny network egress while the mic is live.
