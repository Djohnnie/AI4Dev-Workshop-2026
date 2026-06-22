# Copilot Instructions

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

## Lifecycle expectations

- Start with analysis and planning before scaffolding code.
- Prefer small vertical slices over one giant generation step.
- Add just enough diagnostics or debug visibility to explain gameplay issues quickly.
- Leave behind documentation that would help the next developer understand the app.
- End with output that is review-friendly: summary, risks, and checklist.

## Implementation constraints

- Keep the server tiny; all gameplay runs in `wwwroot/snake.js`.
- Prefer a small number of focused files instead of inventing extra layers.
- Do not leave placeholder TODOs in finished code.
- If you change the UI text, keep it workshop-friendly and easy to demo.

## Output preferences

- When scaffolding, explain the next command or file briefly before making a big jump.
- When planning, make assumptions and risks explicit instead of hiding them.
- When reviewing the game, check it against the lab expectations instead of giving generic advice.
