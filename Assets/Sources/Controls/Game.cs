using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game _instance;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private static IEnumerator RestartGameAction(bool showStartScreen)
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        while(!sceneLoad.isDone)
        {
            yield return null;
        }

        if (!showStartScreen)
        {
            FindObjectOfType<StartScreen>().StartGame();
        }
    }

    public static void RestartGame(bool showStartScreen)
    {
        _instance.StartCoroutine(RestartGameAction(showStartScreen));
    }
}