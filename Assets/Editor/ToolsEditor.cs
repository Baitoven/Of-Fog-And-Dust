using OfFogAndDust.Save;
using UnityEditor;
using UnityEditor.SceneManagement;

public class OpenSceneFromMenu
{
    private const string tutorialScenePath = "Assets/Scenes/Map.unity";

    [MenuItem("Tools/Launch Tutorial %#m")] // (Ctrl+Shift+M)
    private static void OpenScene()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(tutorialScenePath);
            EditorApplication.isPlaying = true;

            SaveManager.Instance.LoadTutorial();
        }
    }
}
