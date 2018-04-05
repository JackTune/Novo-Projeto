using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerScene : MonoBehaviour {

    GameObject player;
    PlayerHealth playerHealth;
    PlayerExperience playerExperience;

	string nomeCena;

	public void ChangeScene (string cena){
		nomeCena = cena;
        Save();
		StartCoroutine (AbrirCena());
	}

     void Awake()
    {
         if (GameObject.FindGameObjectWithTag("Player"))
            {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            playerExperience = player.GetComponent<PlayerExperience>();
           
        }
    }
    
    private IEnumerator AbrirCena(){

		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene (nomeCena);

	}

    void Save()
    {
        PlayerPrefs.SetInt("Score", ScoreManager.score);
        PlayerPrefs.SetInt("Level", playerHealth.playerLevel);
        PlayerPrefs.SetFloat("Experience", playerExperience.currentExperience);
        PlayerPrefs.SetFloat("MaxExperience", playerExperience.maxExperience);
    }



}

