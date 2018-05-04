using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {


    public static bool gameIsPaused;

    public GameObject pauseMenuUI, activeOptions;
    public Button buttonReturn, buttonOptions, buttonMainMenu, buttonExit;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Return();
            }
            else
            {
                Pause();
            }
        }
	}


    public void Return()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MenuPrincipal");

    }

    public void Options()
    {
        ButtonsHiddenOrShow(false);
        activeOptions.SetActive(true);
    }

    public void Back()
    {
        activeOptions.SetActive(false);
        ButtonsHiddenOrShow(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void ButtonsHiddenOrShow(bool boolean)
    {
        buttonReturn.gameObject.SetActive(boolean);
        buttonOptions.gameObject.SetActive(boolean);
        buttonMainMenu.gameObject.SetActive(boolean);
        buttonExit.gameObject.SetActive(boolean);
    }

   
}
