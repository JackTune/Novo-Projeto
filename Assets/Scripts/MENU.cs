using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MENU : MonoBehaviour
{
    public Button buttonStart, buttonOptions, buttonQuit;
    public GameObject options;



    public void Options()
    {
        ButtonsActive(false);
        StartCoroutine(AbrirOpcoes());
        options.SetActive(true);
    }

    public void Back()
    {
        options.SetActive(false);
        ButtonsActive(true);
       
    }

    // espera um tempo para abrir opções 
    private IEnumerator AbrirOpcoes()
    {
        yield return new WaitForSeconds(0.5f);

    }

    void ButtonsActive(bool boolean)
    {
        buttonStart.gameObject.SetActive(boolean);
        buttonOptions.gameObject.SetActive(boolean);
        buttonQuit.gameObject.SetActive(boolean);
    }

    public void Quit()
    {
        Application.Quit();
    }

}