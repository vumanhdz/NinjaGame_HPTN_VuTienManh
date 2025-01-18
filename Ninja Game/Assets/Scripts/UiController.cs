using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public GameObject UiPause;
    public GameObject UiAbout;
    public GameObject UiHscore;

    public void Playgame()
    {
        SceneManager.LoadScene("MainScenes");
        Time.timeScale = 1.0f;
    }
    public void Pausegame()
    {
        UiPause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resumegame()
    {
        UiPause.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Home()
    {
        SceneManager.LoadScene("UI");
        Time.timeScale = 1.0f;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
    public void Hscore()
    {
        UiHscore.SetActive(true);
    }
    public void About()
    {
        UiAbout.SetActive(true);
    }
    public void Xab()
    {
        UiAbout.SetActive(false);
    }
    public void Xhs()
    {
        UiHscore.SetActive(false);
    }
}
