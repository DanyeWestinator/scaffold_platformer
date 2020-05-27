using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public string level = "jay_level_1";
    public void playGame()
    {
        SceneManager.LoadScene(level);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}