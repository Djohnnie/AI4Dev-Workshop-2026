---
title: Scaffold Ultimate Snake host
description: Create the minimal .NET 10 web host and initial static files for the chapter 5 Snake lab.
mode: agent
---
Use `#file:README.md` and `#file:.github/copilot-instructions.md` as the primary context.

Create the first draft of the chapter 5 Ultimate Snake lab:

1. Scaffold a **.NET 10 ASP.NET Core** web app in the current folder.
2. Configure `Program.cs` to serve default files and static files.
3. Add a `wwwroot/` folder with:
   - `index.html`
   - `styles.css`
   - `snake.js`
4. Build only the UI shell and starter wiring first:
   - page title
   - HUD for score, state, and status
   - canvas playfield
   - control instructions
5. Keep the actual game loop incomplete for now, but make the app run in the browser.

Constraints:

- no external packages
- no TypeScript
- no build tooling
- follow the repository instructions automatically
