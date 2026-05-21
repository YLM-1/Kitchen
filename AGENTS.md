# AGENTS.md

## Project Facts
- Unity project; open with Unity `2022.3.56f1c1` from `ProjectSettings/ProjectVersion.txt`.
- Main build scene is `Assets/Scenes/GameScene.unity`; it is the only enabled scene in `ProjectSettings/EditorBuildSettings.asset`.
- Runtime scripts live under `Assets/_Assets/Scripts`; there are no `.asmdef` files, so runtime code compiles into `Assembly-CSharp` and editor-only code into `Assembly-CSharp-Editor`.
- The project uses URP, Cinemachine, TextMeshPro, UGUI, Visual Scripting, and the new Input System from `Packages/manifest.json`.

## Commands
- No repo-local build/test/lint scripts are defined. Use Unity Editor for normal compile/play verification.
- If Unity is available on PATH, a headless compile smoke test can be run with `Unity -batchmode -quit -projectPath . -logFile Logs/unity-batch.log`; inspect `Logs/unity-batch.log` after failures.
- There are no test files or test asmdefs in this repo right now, despite `com.unity.test-framework` being installed.

## Files To Avoid Editing
- Do not edit `Assembly-CSharp.csproj`, `Assembly-CSharp-Editor.csproj`, or `Kitchen.sln`; Unity regenerates them and `.gitignore` excludes these patterns.
- Do not edit `Assets/PlayerInputActions.cs` directly. It is generated from `Assets/PlayerInputActions.inputactions`; change the `.inputactions` asset and regenerate via Unity/Input System if bindings change.
- Avoid changing ignored Unity transient folders: `Library/`, `Temp/`, `Obj/`, `Build/`, `Builds/`, `Logs/`, and `UserSettings/`.

## Code Shape
- Player input is wired through `GameInput`, which creates `PlayerInputActions`, enables the `Player` action map, and exposes `OnInteractAction` and `OnInteractAlternateAction` events.
- `Player` is a singleton and raycasts against `countersLayerMask` in the last movement direction to select a `BaseCounter`.
- Counter interactions are polymorphic through `BaseCounter.Interact(Player)` and `InteractAlternate(Player)`; kitchen object ownership is handled by `IKitchenObjecrParent` and `KitchenObject.SetKitchenObjectParent`.
- Recipes and kitchen object data are `ScriptableObject` assets under `Assets/ScriptableObjects`; recipe lookup is by exact `KitchenObjectSO` reference, not by name.

## Gotchas
- Preserve current misspelled public type/member names unless intentionally doing a serialized-reference migration: `IKitchenObjecrParent`, `IHasprogress`, `PlayerAnimatior`, `CountainerCounterVisual`, and recipe helper names like `HasRecupeWithInput`/`GetBuringRecipeSOWithInput`.
- Many scene/prefab references depend on serialized field names. Rename serialized fields only when you also handle Unity serialization migration.
- Existing comments include mojibake/non-English text; do not mass-rewrite comments unless directly relevant to the change.
