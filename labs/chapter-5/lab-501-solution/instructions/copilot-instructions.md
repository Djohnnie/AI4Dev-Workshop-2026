# Reference Copilot Instructions

This is a workshop-friendly reference copy of the main `.github/copilot-instructions.md` file.

## Tech stack

- Build a **.NET 10 ASP.NET Core** web app.
- Use `UseDefaultFiles()` and `UseStaticFiles()` in `Program.cs`.
- Keep the game in plain **HTML, CSS, and JavaScript** under `wwwroot/`.
- Do **not** add npm, TypeScript, bundlers, or external game libraries.

## Gameplay target

- Recreate the same **Ultimate Snake** mechanics used in the chapter 4 lab.
- Use a canvas-based board plus a simple HUD for score, state, and status.
- Support arrow keys for direction and **spacebar** for start, pause, and restart.
- The snake must wrap through the edges, grow on food, and end on self-collision.

## Implementation constraints

- Keep the server tiny; all gameplay runs in `wwwroot/snake.js`.
- Prefer a small number of focused files instead of inventing extra layers.
- Do not leave placeholder TODOs in finished code.
- If you change the UI text, keep it workshop-friendly and easy to demo.

## Output preferences

- When scaffolding, explain the next command or file briefly before making a big jump.
- When editing code, preserve the current file structure unless there is a strong reason to change it.
- When reviewing the game, check it against the lab expectations instead of giving generic advice.
