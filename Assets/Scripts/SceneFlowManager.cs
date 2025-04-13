using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneFlowManager
{
    public static SceneFlowManager Instance { get; private set; }

    private List<string> sceneOrder = new List<string>
    {
        "Level1",
        "Level2",
        "Level3",
        "ToBeContinued"
    };

    static SceneFlowManager()
    {
        Instance = new SceneFlowManager();
    }

    private SceneFlowManager() { }

    public string GetNextScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int index = sceneOrder.IndexOf(currentScene);

        if (index >= 0 && index < sceneOrder.Count - 1)
        {
            return sceneOrder[index + 1];
        }

        return null;
    }

    public void LoadNextScene()
    {
        string nextScene = GetNextScene();
        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Следующая сцена не найдена или это последняя сцена.");
        }
    }
}
