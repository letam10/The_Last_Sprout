#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public static class ProjectFoundationSetup
{
    private static readonly string[] ProjectFolders =
    {
        "Assets/_Project",
        "Assets/_Project/Art",
        "Assets/_Project/Art/Characters",
        "Assets/_Project/Art/Tilesets",
        "Assets/_Project/Art/UI",
        "Assets/_Project/Art/VFX",
        "Assets/_Project/Audio",
        "Assets/_Project/Audio/Music",
        "Assets/_Project/Audio/SFX",
        "Assets/_Project/Audio/Ambience",
        "Assets/_Project/Data",
        "Assets/_Project/Data/Crops",
        "Assets/_Project/Data/Items",
        "Assets/_Project/Data/NPCs",
        "Assets/_Project/Data/Enemies",
        "Assets/_Project/Data/Quests",
        "Assets/_Project/Data/Weather",
        "Assets/_Project/Data/Spirits",
        "Assets/_Project/Prefabs",
        "Assets/_Project/Prefabs/Player",
        "Assets/_Project/Prefabs/NPCs",
        "Assets/_Project/Prefabs/Enemies",
        "Assets/_Project/Prefabs/Items",
        "Assets/_Project/Prefabs/UI",
        "Assets/_Project/Prefabs/Systems",
        "Assets/_Project/Scenes",
        "Assets/_Project/Scenes/Boot",
        "Assets/_Project/Scenes/Farm",
        "Assets/_Project/Scenes/Towns",
        "Assets/_Project/Scenes/Forests",
        "Assets/_Project/Scenes/Caves",
        "Assets/_Project/Scenes/Coast",
        "Assets/_Project/Scenes/Diving",
        "Assets/_Project/Scenes/Desert",
        "Assets/_Project/Scenes/Ruins",
        "Assets/_Project/Scenes/Tests",
        "Assets/_Project/Scripts",
        "Assets/_Project/Scripts/Core",
        "Assets/_Project/Scripts/Save",
        "Assets/_Project/Scripts/Input",
        "Assets/_Project/Scripts/Player",
        "Assets/_Project/Scripts/Camera",
        "Assets/_Project/Scripts/Farming",
        "Assets/_Project/Scripts/Inventory",
        "Assets/_Project/Scripts/Items",
        "Assets/_Project/Scripts/Dialogue",
        "Assets/_Project/Scripts/NPC",
        "Assets/_Project/Scripts/Quest",
        "Assets/_Project/Scripts/Weather",
        "Assets/_Project/Scripts/Combat",
        "Assets/_Project/Scripts/Spirits",
        "Assets/_Project/Scripts/World",
        "Assets/_Project/Scripts/UI",
        "Assets/_Project/Scripts/Tools",
        "Assets/_Project/Scripts/Editor",
        "Assets/_Project/Settings",
        "Assets/_Project/Tests",
        "Assets/_Project/Tests/EditMode",
        "Assets/_Project/Tests/PlayMode"
    };

    [MenuItem("Tools/Project Foundation/Run Full Setup")]
    public static void RunFullSetup()
    {
        CreateFolders();
        CreateBaseScenes();
        CreateGitFiles();
        CreateAIWorkflowFiles();
        AssetDatabase.Refresh();
        Debug.Log("Project foundation setup complete.");
    }

    [MenuItem("Tools/Project Foundation/1 Create Folder Structure")]
    public static void CreateFolders()
    {
        foreach (var folder in ProjectFolders)
        {
            CreateFolderRecursive(folder);
        }

        AssetDatabase.Refresh();
        Debug.Log("Project folder structure created or verified.");
    }

    [MenuItem("Tools/Project Foundation/2 Create Base Scenes")]
    public static void CreateBaseScenes()
    {
        CreateFolders();

        const string bootScenePath = "Assets/_Project/Scenes/Boot/Boot.unity";
        const string farmScenePath = "Assets/_Project/Scenes/Farm/Farm_Test.unity";
        const string systemTestScenePath = "Assets/_Project/Scenes/Tests/SystemTest.unity";

        CreateBootScene(bootScenePath);
        CreateFarmTestScene(farmScenePath);
        CreateSystemTestScene(systemTestScenePath);

        EditorBuildSettings.scenes = new[]
        {
            new EditorBuildSettingsScene(bootScenePath, true),
            new EditorBuildSettingsScene(farmScenePath, true),
            new EditorBuildSettingsScene(systemTestScenePath, true)
        };

        AssetDatabase.Refresh();
        Debug.Log("Base scenes created or verified, and Build Settings updated.");
    }

    [MenuItem("Tools/Project Foundation/3 Create Git Files")]
    public static void CreateGitFiles()
    {
        WriteRootFileIfMissing(".gitignore", GitignoreContent);
        WriteRootFileIfMissing(".gitattributes", GitattributesContent);
        AssetDatabase.Refresh();
        Debug.Log("Git helper files created if missing.");
    }

    [MenuItem("Tools/Project Foundation/4 Create AI Workflow Files")]
    public static void CreateAIWorkflowFiles()
    {
        WriteRootFileIfMissing("AGENTS.md", AgentsContent);
        WriteRootFileIfMissing("AI_TASK_TEMPLATE.md", TaskTemplateContent);
        AssetDatabase.Refresh();
        Debug.Log("AI workflow files created if missing.");
    }

    private static void CreateFolderRecursive(string folderPath)
    {
        var parts = folderPath.Split('/');
        if (parts.Length == 0 || parts[0] != "Assets")
        {
            throw new ArgumentException("Folder path must start with Assets: " + folderPath);
        }

        var current = "Assets";
        for (var i = 1; i < parts.Length; i++)
        {
            var next = current + "/" + parts[i];
            if (!AssetDatabase.IsValidFolder(next))
            {
                AssetDatabase.CreateFolder(current, parts[i]);
            }
            current = next;
        }
    }

    private static void CreateBootScene(string path)
    {
        if (SceneFileExists(path))
        {
            Debug.Log("Skipped existing scene: " + path);
            return;
        }

        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "Boot";

        var systemsRoot = new GameObject("_BootSystems");
        systemsRoot.transform.position = Vector3.zero;

        var bootstrap = new GameObject("GameBootstrap");
        bootstrap.transform.SetParent(systemsRoot.transform);

        CreateCamera("Main Camera", new Vector3(0f, 0f, -10f), 5f);

        EditorSceneManager.SaveScene(scene, path);
    }

    private static void CreateFarmTestScene(string path)
    {
        if (SceneFileExists(path))
        {
            Debug.Log("Skipped existing scene: " + path);
            return;
        }

        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "Farm_Test";

        var systemsRoot = new GameObject("_SceneSystems");
        systemsRoot.transform.position = Vector3.zero;

        var spawnPoint = new GameObject("Player_Spawn_Point");
        spawnPoint.transform.position = Vector3.zero;

        var gridGo = new GameObject("Grid");
        var grid = gridGo.AddComponent<Grid>();
        grid.cellSize = new Vector3(1f, 1f, 0f);
        grid.cellLayout = GridLayout.CellLayout.Rectangle;

        CreateTilemapChild(gridGo.transform, "Ground_Tilemap", 0);
        CreateTilemapChild(gridGo.transform, "Collision_Tilemap", 10);
        CreateTilemapChild(gridGo.transform, "Decoration_Tilemap", 20);

        CreateCamera("Main Camera", new Vector3(0f, 0f, -10f), 5f);

        EditorSceneManager.SaveScene(scene, path);
    }

    private static void CreateSystemTestScene(string path)
    {
        if (SceneFileExists(path))
        {
            Debug.Log("Skipped existing scene: " + path);
            return;
        }

        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "SystemTest";

        var testRoot = new GameObject("_SystemTestRoot");
        testRoot.transform.position = Vector3.zero;

        CreateCamera("Main Camera", new Vector3(0f, 0f, -10f), 5f);

        EditorSceneManager.SaveScene(scene, path);
    }

    private static void CreateTilemapChild(Transform parent, string name, int sortingOrder)
    {
        var tilemapGo = new GameObject(name);
        tilemapGo.transform.SetParent(parent);
        tilemapGo.transform.localPosition = Vector3.zero;

        tilemapGo.AddComponent<Tilemap>();
        var renderer = tilemapGo.AddComponent<TilemapRenderer>();
        renderer.sortingOrder = sortingOrder;
    }

    private static Camera CreateCamera(string name, Vector3 position, float orthographicSize)
    {
        var cameraGo = new GameObject(name);
        cameraGo.tag = "MainCamera";
        cameraGo.transform.position = position;

        var camera = cameraGo.AddComponent<Camera>();
        camera.orthographic = true;
        camera.orthographicSize = orthographicSize;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = Color.black;
        camera.nearClipPlane = 0.1f;
        camera.farClipPlane = 100f;
        return camera;
    }

    private static bool SceneFileExists(string assetPath)
    {
        return File.Exists(ToAbsolutePath(assetPath));
    }

    private static string ToAbsolutePath(string assetPath)
    {
        var projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        return Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar));
    }

    private static void WriteRootFileIfMissing(string fileName, string content)
    {
        var projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        var path = Path.Combine(projectRoot, fileName);

        if (File.Exists(path))
        {
            Debug.Log("Skipped existing root file: " + fileName);
            return;
        }

        File.WriteAllText(path, content.Replace("\n", Environment.NewLine));
        Debug.Log("Created root file: " + fileName);
    }

    private const string GitignoreContent = @"# Unity generated folders
/[Ll]ibrary/
/[Tt]emp/
/[Oo]bj/
/[Bb]uild/
/[Bb]uilds/
/[Ll]ogs/
/[Uu]ser[Ss]ettings/
/[Mm]emoryCaptures/
/[Rr]ecordings/

# IDE and OS files
.vs/
.vscode/
.idea/
*.csproj
*.sln
*.user
*.userprefs
*.tmp
*.pidb
*.booproj
*.svd
*.pdb
*.mdb
*.opendb
*.VC.db
.DS_Store
Thumbs.db

# Unity crash and generated files
sysinfo.txt
mono_crash.*

# Build outputs
*.apk
*.aab
*.app
*.exe
*.dmg
*.unitypackage
*.unitypackage.meta

# Addressables generated data
/ServerData
/[Aa]ssets/StreamingAssets/aa*
/[Aa]ssets/AddressableAssetsData/link.xml*
/[Aa]ssets/Addressables_Temp*
/[Aa]ssets/AddressableAssetsData/*/*.bin*

# Rider plugin
/[Aa]ssets/Plugins/Editor/JetBrains*
*.DotSettings.user
";

    private const string GitattributesContent = @"# Images and pixel art sources
*.png filter=lfs diff=lfs merge=lfs -text
*.jpg filter=lfs diff=lfs merge=lfs -text
*.jpeg filter=lfs diff=lfs merge=lfs -text
*.psd filter=lfs diff=lfs merge=lfs -text
*.aseprite filter=lfs diff=lfs merge=lfs -text

# Audio
*.wav filter=lfs diff=lfs merge=lfs -text
*.ogg filter=lfs diff=lfs merge=lfs -text
*.mp3 filter=lfs diff=lfs merge=lfs -text
*.flac filter=lfs diff=lfs merge=lfs -text

# Video
*.mp4 filter=lfs diff=lfs merge=lfs -text
*.mov filter=lfs diff=lfs merge=lfs -text
*.webm filter=lfs diff=lfs merge=lfs -text

# Fonts
*.ttf filter=lfs diff=lfs merge=lfs -text
*.otf filter=lfs diff=lfs merge=lfs -text

# Unity and DCC binary assets
*.fbx filter=lfs diff=lfs merge=lfs -text
*.blend filter=lfs diff=lfs merge=lfs -text
*.anim filter=lfs diff=lfs merge=lfs -text
*.controller filter=lfs diff=lfs merge=lfs -text

# Archives
*.zip filter=lfs diff=lfs merge=lfs -text
*.rar filter=lfs diff=lfs merge=lfs -text
*.7z filter=lfs diff=lfs merge=lfs -text
";

    private const string AgentsContent = @"# Project Agent Instructions

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
";

    private const string TaskTemplateContent = @"# AI Task Template

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
";
}
#endif
