# Last One Out

A polished turn-based strategy game built in Unity, showcasing clean architecture, game theory AI, and a full suite of multiplayer modes — all in a sleek 3D presentation.

---

## About the Game

**Last One Out** is a modern take on the classic mathematical strategy game [Nim](https://en.wikipedia.org/wiki/Nim). Players take turns removing items from a shared pool on a dinner table. The twist: who wins depends on who takes the *last* item — and that's configurable.

Simple rules, deep strategy.

---

## Game Modes

| Mode | Player 1 | Player 2 |
|------|----------|----------|
| PvP  | Human    | Human    |
| PvE  | Human    | AI       |
| EvP  | AI       | Human    |
| EvE  | AI       | AI       |

Watch two AIs battle it out in EvE mode, or go head-to-head with a friend locally.

---

## Features

- **Unbeatable AI** — The AI uses a game-theory solver based on the Nim winning formula. When in a winning position, it plays optimally. When losing, it plays on — because people make mistakes.
- **Configurable rules** — Total items, max items per turn, and win condition (last to take wins *or* loses) are all tunable via ScriptableObject settings.
- **4 game modes** — Every combination of human and AI players is supported.
- **Polished 3D presentation** — Items laid out on a dinner table with per-state material feedback (normal, selectable, forbidden, off-board).
- **Smooth UX** — DOTween-powered animations, round indicators, player turn display, and integrated audio.

---

## Architecture

The project is built on **PureMVC**, a strict Model-View-Controller framework, with additional patterns for scalability and testability:

- **Proxy layer** — Encapsulates all game state and settings access
- **Command pattern** — Every game action (hover, click, AI decision, round start) is a discrete, reusable command
- **Mediators** — Bridge views to the rest of the application without direct coupling
- **Notification system** — Loose event-based communication between all layers
- **Dependency injection** — Custom DI container for flexible component wiring
- **Asset Map system** — Type-safe, centralized asset management

```
Scripts/
├── Core/           — Foundation utilities and patterns
├── Game/           — Game logic (Solver, ItemControl, AssetMaps)
├── PureMvc/
│   ├── Commands/   — StartGame, StartRound, AiDecision, Navigate…
│   ├── Mediators/  — InGame, MainMenu, ChooseGameMenu
│   ├── Models/     — GameState, GameSettings
│   ├── Proxies/    — GameStateProxy, GameSettingsProxy
│   ├── Views/      — InGameView, MainMenuView, ChooseGameMenuView
│   └── Notifications/
└── Extensions/     — Utility helpers
```

---

## Tech Stack

| Tool | Purpose |
|------|---------|
| Unity | Engine & runtime |
| PureMVC | Application architecture |
| DOTween | UI and scene animations |
| Post Processing / BloomPro | Visual effects |

---

## AI Design

The AI solver implements the mathematical winning strategy for Nim:

> For a given `maxTake` per turn, the losing positions are multiples of `maxTake + 1`. The AI calculates the nearest winning position and takes exactly the right number of items.

When the AI is already in a losing position, it picks a random valid move — keeping the game competitive and giving the human a fair chance to blunder.

A simulated thinking delay (2–5 seconds) keeps the pacing natural.

---

## Author

Made by **Vitalii Vasylenko**
