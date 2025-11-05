using OfFogAndDust.Utils;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class OpenSceneFromMenu
{
    private const string tutorialScenePath = "Assets/Scenes/Map.unity";

    [MenuItem("Tools/Launch Tutorial %#m")] // Ctrl+Shift+M
    private static void OpenScene()
    {
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            return;

        EditorSceneManager.OpenScene(tutorialScenePath);

        EditorApplication.isPlaying = true;

        GameObject watcher = new GameObject("RuntimeWatcher");
        watcher.AddComponent<RuntimeWatcher>();
    }

}
