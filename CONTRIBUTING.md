# Contributing

- Run `pytest -q` and keep tests green.
- Keep tools pure and arg‑validated. Update `tool_code_hash` when code changes.
- Expand adversarial tests (unicode confusables, symlink races, replay).

## Voice Modules
- Respect constitution voice rules.
- Do not store raw audio by default; only redacted transcripts.
- Validate args; enforce capability tokens; stream partials/finals (listen) and audio frames (speak).
- Provide stub implementations for CI.

## Tests
- `audio.listen` redaction test.
- `audio.speak` respects `privacy_mode: silent`.
- Egress blocked while `voice_session_active` is true.

## Long‑Term Goals
See [LEARNING_ENHANCEMENTS.md](LEARNING_ENHANCEMENTS.md). Design code for dynamic, signed plugin loading.
