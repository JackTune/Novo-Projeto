using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerScene : MonoBehaviour {

	string nomeCena;

	public void ChangeScene (string cena){
		nomeCena = cena;
        SaveScore();
		StartCoroutine (AbrirCena());
	}
      
	private IEnumerator AbrirCena(){

		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene (nomeCena);

	}

    void SaveScore()
    {
        PlayerPrefs.SetInt("Score", ScoreManager.score);
    }



}

