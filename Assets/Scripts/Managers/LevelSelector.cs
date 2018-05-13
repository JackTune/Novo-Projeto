using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    string nameCena;
    public Button[] levelButtons;

    public int levelToUnlock;

    private void Start()
    {
        int levelReacher = PlayerPrefs.GetInt(("levelReacher"), 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > levelReacher)
                levelButtons[i].interactable = false;
        }
    }


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
