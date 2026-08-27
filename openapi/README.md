# Etsy OpenAPI snapshot

`etsy-openapi-3.0.0.json` is a verbatim snapshot of Etsy's published OpenAPI
specification:

<https://www.etsy.com/openapi/generated/oas/3.0.0.json>

The `etsy-openapi-maintenance.yml` GitHub Actions workflow checks this endpoint
once a week (and on manual dispatch). When Etsy publishes a change, the
workflow replaces the snapshot, asks Codex to update the library, demo, and
tests as needed, runs the solution's validation commands, and opens a pull
request against `dev` for manual review.

Repository administrators must configure an `OPENAI_API_KEY` Actions secret so
the Codex step can run. The workflow's built-in `GITHUB_TOKEN` is used to push
the generated branch and create the pull request.
