# Lab 801 — Multiplayer Ultimate Snake

> **Chapter:** Chapter 8  
> **Skill focus:** Combining the workshop topics into one larger end-to-end build  
> **Difficulty:** ⭐⭐⭐⭐⭐

← Back to [Labs Index](../../README.md)

---

## 🎯 Goal

Build a **multiplayer web-based Ultimate Snake** game with **.NET 10**.

This capstone lab brings together the ideas from the earlier chapters:

- building a browser-based Snake experience
- designing a small but complete .NET web app
- coordinating multiple clients in real time
- using shared state on the server
- guiding implementation with Copilot workflows you have practiced earlier in the workshop

The final result should let players create or join **game rooms** using a **short unique code**. Players inside the same room should see the same board, the same food, and each other's snakes in real time.

---

## 🧩 Scenario

You are building a lightweight multiplayer party game.

One player creates a room and shares the room code with others. Other players join from their own browsers. When the room starts:

- every player controls their own snake
- all snakes move on the **same shared board**
- the room uses **one shared food item**
- the game uses **WebSockets** to synchronize state between the server and connected clients

This is not meant to be a production-grade multiplayer platform. The goal is to build a clear, working reference implementation that demonstrates the architecture and the core gameplay loop.

---

## ✅ Expectations

The finished app should support the following:

### Room flow

- a player can **create** a room
- the server generates a **short unique room code**
- another player can **join** with that code
- the UI clearly shows the current room code and connected players

### Multiplayer gameplay

- each player controls one snake
- all players see the same room state
- movement is synchronized through **WebSockets**
- the board contains **one shared food item**
- the first snake to reach the food claims it
- snakes grow after eating
- snakes wrap through the board edges
- snakes can crash into themselves or into another snake

### User experience

- the game runs in the browser
- the UI shows room state, player list, and score
- players can tell whether the room is waiting, running, paused, or finished
- the experience is understandable even with only two players connected

---

## 🏗️ Suggested implementation slices

If you want to build this incrementally, a good order is:

1. Create a minimal .NET 10 web app that serves static files.
2. Add a browser UI for creating and joining a room.
3. Add a simple in-memory room model on the server.
4. Add a WebSocket endpoint for real-time updates.
5. Render multiple snakes on the same board.
6. Add shared food, scoring, and collision logic.
7. Refine the room flow and restart behavior.

---

## 💡 Architecture hints

You do **not** need a database for this lab.

A good solution can stay fully in memory while the app is running:

- a room manager stores active rooms
- each room stores its players, food, and current game phase
- the server runs the game loop and broadcasts room updates
- clients send small input messages such as direction changes or start/pause actions

This keeps the design focused on gameplay and synchronization instead of infrastructure.

---

## 🧪 Completion criteria

You are done when:

- the app runs locally with `dotnet run`
- one browser can create a room
- another browser can join with the generated code
- both browsers see the same board state
- multiple snakes are rendered at the same time
- food is shared across all players
- collisions against self or other snakes are handled correctly

---

## 🏁 Reference

A possible solution is available in:

```text
../lab-801-solution
```

---

← Back to [Labs Index](../../README.md) | Previous: [Lab 601 — Ultimate Snake Across the Entire Lifecycle](../../chapter-06/lab-601/README.md)
