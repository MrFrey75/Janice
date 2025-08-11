# Security

- Private keys live in `.secrets/` with `0600` perms. Rotate via `janice gen-keys --rotate`.
- Audit log is append‑only and hash‑chained; verify via `janice audit-verify`.
- Executor rejects unsigned/expired approvals, arg hash mismatches, or tool code hash drift.

## Voice Input Privacy
- Wake‑word or push‑to‑talk required; raw audio not stored by default.
- Transcripts are redacted before audit.
- While mic is active, risky egress tools can be auto‑denied.

## Voice Output Privacy
- `audio.speak` is denied in `privacy_mode: silent`.
- Option to log only SHA‑256 of spoken text.
