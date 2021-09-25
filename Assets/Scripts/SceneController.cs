using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static int status = 0;
    public static int grade = 0;

    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    public static void start()
    {
        SceneManager.UnloadSceneAsync("Menu");
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
    }

    public static void lose()
    {
        Debug.Log("You lose.");
        SceneController.status = -1;
        SceneManager.UnloadSceneAsync("Gameplay");
        SceneManager.LoadScene("End", LoadSceneMode.Additive);
    }

    public static void win()
    {
        Debug.Log("You win.");
        SceneController.status = 1;
        SceneController.grade *= 2;
        SceneManager.UnloadSceneAsync("Gameplay");
        SceneManager.LoadScene("End", LoadSceneMode.Additive);
    }

    public static void restart()
    {
        SceneManager.UnloadSceneAsync("End");
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
}
