# Janice Data Transparency Statement

Janice is built for **local-first, policy-governed automation**. This document describes exactly what data Janice collects, stores, and shares, and how operators can control it.

## 1. What Janice Collects
**Required**
- Task inputs (prompts, tool args), context objects.
- Tool results and execution metadata.
- Audit metadata: run/step IDs, tool/version, signed decision payloads, hashes, timestamps.

**Optional (must be explicitly enabled)**
- Raw audio segments (voice mode) for debugging.
- Full, unredacted transcripts (bypass redaction rules).

## 2. What Janice Stores
- **Audit Log** (append-only, hash-chained).
- **Tool Registry Metadata** (IDs, versions, schemas, code hashes).
- **Configuration Files** (`constitution.yaml`, tool blueprints, operator settings).

## 3. Not Stored by Default
- Raw mic audio.
- Unredacted sensitive data (PII, credentials).
- External API responses beyond run scope.

## 4. What Never Leaves Your System
- No phoning home. External calls only through approved tools that pass constitutional checks.
- Nothing is exported unless you do it explicitly.

## 5. Your Control
- Edit `constitution.yaml` for allow/deny/redaction.
- Add redaction patterns to strip/hash sensitive content.
- `janice audit-verify` to validate chain; `janice export-audit` to export.
- Delete `audit/`, `scratch/`, `.secrets/` for full wipe.

## 6. Retention (Recommended)
| Data | Default | Configurable |
|---|---|---|
| Audit Logs | Indefinite | Yes |
| Scratch Data | Until cleanup | Yes |
| Raw Audio (opt) | Session only | Yes |
| Redacted Transcript | Indefinite | Yes |

**Last Updated:** 2025‑08‑11
