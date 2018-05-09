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

        //Verifica se o Esc foi clicado
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Verifica se está pausado
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

    //Retorna ao game, propriamente dito.
    public void Return()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    //Pausa o game, mostrando suas funções de pause
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    //OnClick para voltar ao MenuPrincipal
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MenuPrincipal");

    }

    //Mostrar as configurações
    public void Options()
    {
        ButtonsHiddenOrShow(false);
        activeOptions.SetActive(true);
    }

    //OnClick para voltar para a tela "Pause"
    public void Back()
    {
        activeOptions.SetActive(false);
        ButtonsHiddenOrShow(true);
    }

    //OnClick para sair do jogo
    public void ExitGame()
    {
        Application.Quit();
    }


    //Metódo feito para ativar ou desativar os utiliários do canvas.
    void ButtonsHiddenOrShow(bool boolean)
    {
        buttonReturn.gameObject.SetActive(boolean);
        buttonOptions.gameObject.SetActive(boolean);
        buttonMainMenu.gameObject.SetActive(boolean);
        buttonExit.gameObject.SetActive(boolean);
    }

   
}
