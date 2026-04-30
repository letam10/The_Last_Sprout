# Project Agent Instructions

## Project
Unity 6.4 / 6000.4.4f1 2D top-down pixel art farming RPG.
Target platforms: PC Steam and Steam Deck.

## Hard Rules
- Read this AGENTS.md before doing any task.
- Modify only files under Assets/_Project/Scripts unless the task explicitly says otherwise.
- Modify Assets/_Project/Data only when ScriptableObject data assets are explicitly requested.
- Do not edit scenes, prefabs, ProjectSettings, Packages, Git files, or serialized assets without explicit permission.
- Do not install, remove, or upgrade Unity packages without explicit permission.
- Do not rename serialized fields unless a migration is explicitly planned.
- Keep changes small and compile-safe.
- Prefer data-driven systems using ScriptableObject definitions.
- Use [SerializeField] private fields for Inspector references.
- Avoid garbage allocations in Update, FixedUpdate, LateUpdate, combat hot paths, AI loops, projectile systems, VFX systems, and UI refresh loops.
- Do not use FindObjectOfType, GameObject.Find, Camera.main, Resources.Load, LINQ, reflection, or string-heavy logic in gameplay hot paths.
- Preserve Unity serialization names and prefab compatibility.
- Add manual test notes after each task.

## Unity MCP Rules
- Unity MCP tools may be used to inspect editor state, selected objects, console logs, open scenes, screenshots, and tests.
- Prefer read-only MCP tools first before modifying anything.
- Ask before using MCP tools that edit scenes, prefabs, assets, packages, or project settings.
- It is allowed to use MCP tools to:
  - read console logs
  - check editor play mode state
  - capture Game View / Scene View screenshots
  - run EditMode or PlayMode tests
  - inspect scene hierarchy
- It is not allowed to use MCP tools to:
  - mass delete assets
  - rewrite scenes
  - auto-fix the whole project
  - install packages
  - change build settings
  - push to Git

## Architecture Direction
- Boot scene owns global services.
- Gameplay scenes own local scene systems.
- Save system must support versioning, atomic write, backups, and migration.
- Maps should be split by scene or region; do not load the full world at once.
- Sprite atlases should be grouped by biome, scene, and UI, not one giant atlas.
- Pool projectiles, VFX, item drops, and damage numbers.
- NPCs outside screen should use simplified schedule simulation.
- Enemy AI should tick at intervals, not every frame.
- Prefer service boundaries over large manager classes.
- Prefer small systems that can be tested independently.

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
- Behavior added or changed.
- Manual Unity setup steps.
- Compile/test checklist.
- Known risks or assumptions.
- Whether Unity MCP tools were used.