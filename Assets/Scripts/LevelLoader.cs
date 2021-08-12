using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        ResetTimeScale();
        if (PointCounter.Instance != null) { PointCounter.Instance.gameObject.SetActive(true); }
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextScene()
    {
        ResetTimeScale();
        if (PointCounter.Instance != null) { PointCounter.Instance.gameObject.SetActive(true); }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadOptionsMenu()
    {
        ResetTimeScale();
        if (PointCounter.Instance != null) { PointCounter.Instance.gameObject.SetActive(false); }
        SceneManager.LoadScene("OptionsMenu");
    }

    public void ReloadCurrentScene()
    {
        ResetTimeScale();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
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
