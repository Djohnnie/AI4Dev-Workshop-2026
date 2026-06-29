---
title: Finish Ultimate Snake gameplay
description: Example prompt file that completes the Snake mechanics after the host exists.
mode: agent
---

Use `#file:instructions/copilot-instructions.md` and the current `wwwroot/` files as context.

Finish the Snake game implementation.

Task:

- complete the JavaScript game loop
- wire keyboard input
- render the snake and food
- update the HUD

Target behavior:

- same gameplay as the chapter 4 Ultimate Snake lab
- space starts, pauses, and restarts
- arrow keys move the snake without instant reversal
- wrap-around movement
- food growth
- self-collision game over

Constraints:

- keep gameplay in `wwwroot/snake.js`
- keep the server host tiny
- do not add packages
- keep the result easy to demo in a workshop
