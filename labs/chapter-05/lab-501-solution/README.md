# Lab 501 Solution — Ultimate Snake with Instructions, Prompt Files, and Skills

This folder contains one possible completed result for **Lab 501**.

It keeps the same Snake mechanics as the chapter 4 solution, but also includes the **repository instructions**, **prompt files**, and **skill file** that shape the build workflow in the starter lab.

For workshop demos, the solution also includes an `instructions\` folder with a small set of **reference examples** that participants can open side-by-side while discussing what each asset does.

## Included gameplay target

The finished app:

- runs as a **.NET 10 ASP.NET Core** static-files app
- renders a browser-based **Ultimate Snake** game
- uses **arrow keys** for movement
- uses **spacebar** for start, pause, and restart
- wraps through the edges
- grows when food is eaten
- ends on self-collision
- shows score, state, and status in the HUD

## Why this solution exists

Use it as the reference result after participants experiment with:

- `.github/copilot-instructions.md`
- prompt files in `.github/prompts/`
- the validation skill in `.github/skills/ultimate-snake-playtest/`

## Reference instruction assets

The `instructions\` folder intentionally keeps the reference set small:

- `instructions\copilot-instructions.md`
- `instructions\prompts\scaffold-ultimate-snake-host.prompt.md`
- `instructions\prompts\finish-ultimate-snake.prompt.md`
- `instructions\skills\ultimate-snake-playtest\SKILL.md`

These are there for **reading and discussion** during the workshop. The active files that GitHub Copilot uses in this solution still live under `.github\`.
