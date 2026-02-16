# Development Log

## S1 – Project Setup & Level Infrastructure
Estimated focused development time: ~2 hours

### Done
- Created Unity 6000.0.26f1 URP project.
- Defined project folder structure separating Core, Domain, Application and Framework layers.
- Created Main scene.
- Implemented path abstraction via `IPathProvider`.
- Implemented `IPathProvider` as `WaypointPath` with serialized waypoints.
- Implemented editor-only gizmo drawer using `[DrawGizmo]`.
- Designed and placed a 12-waypoint path.
- Created 9 tower slot prefabs placed intentionally across early/mid/late zones.
- Implemented `TowerSlot` runtime component with mandatory anchor.

### Notes
- TowerSlot designed minimal; will evolve when tower runtime exists.

### Next
- Implement application infrastructure:
  - EventBus
  - Game state machine (State pattern)
  - GameCompositionRoot
  
  
## S2 – Application Infrastructure & UI State Flow
Estimated focused development time: ~2.5 hours

### Done
- Implemented `IGameState` and `GameStateMachine` in Core (no UnityEngine dependency).
- Implemented EventBus abstraction `IEventBus` and `DictionaryEventBus` (Subscribe/Unsubscribe/Publish).
- Implemented framework `GameStateBase` with `SetNextState(IGameState)` and `GoToNextState()`.
- Created a single Canvas with three panels: MainMenu / Gameplay / GameOver.
- Implemented panel Views and wired them to raise UI events.
- Implemented `GameCompositionRoot` to instantiate and wire `GameStateMachine`, EventBus, and all states.
- Implemented `MainMenuState` to show panel, subscribe to Start, and transition to Gameplay.
- Implemented `LevelGameplayState` to show panel, subscribe to EndGame, and transition to GameOver.
- Implemented `GameOverState` to show panel, subscribe to Restart, and transition back to Gameplay.

### Next
- Implement enemies: prefab + basic runtime + domain, waypoint movement, spawner, and minimal waves.