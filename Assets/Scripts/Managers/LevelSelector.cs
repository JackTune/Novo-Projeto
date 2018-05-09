using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    string nameCena;


    public void Select(string cena)
    {
        nameCena = cena;

        StartCoroutine(AbrirCena());
    }



    private IEnumerator AbrirCena()
    {

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nameCena);

    }
}
