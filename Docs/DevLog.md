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