# Project Agent Instructions

## Project
Unity 6.4 2D top-down pixel art farming RPG.
Target platforms: PC Steam and Steam Deck.

## Hard Rules
- Modify only files under Assets/_Project/Scripts unless the task explicitly says otherwise.
- Do not edit scenes, prefabs, ProjectSettings, Packages, or serialized assets without explaining why.
- Keep changes small and compile-safe.
- Prefer data-driven systems using ScriptableObject definitions.
- Avoid garbage allocations in Update, combat hot paths, AI loops, and projectile/VFX systems.
- Use [SerializeField] private fields for Inspector references.
- Do not use FindObjectOfType, GameObject.Find, or Camera.main in gameplay hot paths.
- Preserve Unity serialization names unless a migration is explicitly planned.
- Add manual test notes after each task.

## Architecture Direction
- Boot scene owns global services.
- Gameplay scenes own local scene systems.
- Save system must support versioning, atomic write, backups, and migration.
- Maps should be split by scene or region; do not load the full world at once.
- Sprite atlases should be grouped by biome, scene, and UI, not one giant atlas.
- Pool projectiles, VFX, item drops, and damage numbers.
- NPCs outside screen should use simplified schedule simulation.
- Enemy AI should tick at intervals, not every frame.

## First Implementation Order
1. Input and player movement.
2. Camera follow and pixel-perfect setup notes.
3. Scene transition prototype.
4. Save/load foundation.
5. Inventory and item data.
6. Farming grid prototype.
7. Day, season, and weather prototype.
8. NPC schedule prototype.

## Required Output From Agent
For every code task, provide:
- Files changed.
- Why each file was changed.
- Manual Unity setup steps.
- Compile/test checklist.
- Known risks or assumptions.
