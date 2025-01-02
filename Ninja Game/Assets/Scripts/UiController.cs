using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public GameObject UiManager;
    // Start is called before the first frame update
    
    public void Playgame()
    {
        SceneManager.LoadScene("MainScenes");
        Time.timeScale = 1.0f;
    }
    public void Pausegame()
    {
        UiManager.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resumegame()
    {
        UiManager.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Home()
    {
        SceneManager.LoadScene("UI");
        Time.timeScale = 1.0f;
    }
}
