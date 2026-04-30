# AI Task Template

## Task
Describe one small task only.

## Allowed Scope
Allowed folders:
- Assets/_Project/Scripts
- Assets/_Project/Data only if data assets are explicitly requested

Do not modify:
- Scenes
- Prefabs
- ProjectSettings
- Packages
- Git files

## Requirements
- Unity version: 6000.4.4f1
- Code must compile.
- Keep the diff small.
- Use [SerializeField] private fields for Inspector wiring.
- Avoid allocations in gameplay hot paths.
- Avoid scene/prefab rewiring unless explicitly requested.

## Expected Response
After implementation, summarize:
1. Files changed.
2. Behavior added or changed.
3. Manual Unity setup required.
4. How to test in Unity.
5. Risks and assumptions.
