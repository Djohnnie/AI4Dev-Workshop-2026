---
title: Harden Ultimate Snake quality
description: Add chapter 6 style validation, diagnostics, and debugging support to the Snake lab.
mode: agent
---
Use `#file:README.md`, `#file:.github/copilot-instructions.md`, and the current implementation as context.

Improve the Snake lab with chapter 6 quality thinking.

Focus on:

1. the gameplay rules most likely to break
2. lightweight diagnostics that make debugging easier
3. a validation or regression checklist
4. user-visible states that should be easy to explain in a demo
5. any small documentation updates needed to explain the current design

Examples of useful improvements:

- clearer status text
- a small debug/status overlay
- better separation of fragile game rules in `snake.js`
- a manual verification checklist tied to wrap-around, growth, collision, pause, and restart

Constraints:

- stay lightweight
- do not add external packages or test frameworks
- prefer practical workshop value over enterprise ceremony
