using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private const string MAIN_MENU = "MainMenu";
    private const string OPTIONS_MENU = "OptionsMenu";
    private const string LEVEL1_NAME = "Level1";

    public void LoadMainMenu()
    {
        ResetTimeScale();
        SetPointCounterActive(true);
        SceneManager.LoadScene(MAIN_MENU);
    }

    public void LoadNextScene()
    {
        ResetTimeScale();
        SetPointCounterActive(true);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void StartGame()
    {
        if (PlayerSelector.Instance.IsSelectedPlayerUnlocked() == false) { return; }

        ResetTimeScale();
        SetPointCounterActive(true);
        SceneManager.LoadScene(LEVEL1_NAME);
    }

    public void LoadOptionsMenu()
    {
        ResetTimeScale();
        SetPointCounterActive(false);
        SceneManager.LoadScene(OPTIONS_MENU);
    }

    public void ReloadCurrentScene()
    {
        ResetTimeScale();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void SetPointCounterActive(bool active)
    {
        if (PointCounter.Instance != null) { PointCounter.Instance.gameObject.SetActive(active); }
    }

    private void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
