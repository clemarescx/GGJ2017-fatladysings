using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool Paused = false;
    public Canvas PauseCanvas;
    public string MainMenuSceneName;

    void Start()
    {
        PauseCanvas.gameObject.SetActive(false);
    }


    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (Paused == false)
        {
            PauseGame();
        } else {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        PauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        Paused = false;
    }

    public void PauseGame()
    {
        PauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
        Paused = true;
    }

    public void ChangeScene() {
        ResumeGame();
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}