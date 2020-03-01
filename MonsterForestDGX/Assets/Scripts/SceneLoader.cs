using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string battleScene = "Battle";
    public string mainMenuScene = "MainMenu";

    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadMainMenu()
    {
        LoadScene(mainMenuScene);
    }

    public void LoadBattle()
    {
        LoadScene(battleScene);
    }
}
