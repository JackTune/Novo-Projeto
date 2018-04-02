using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
	public PlayerHealth playerHealth;
	public float restartDelay = 5f;
    public GameObject desativarTimeProxWaves;
    public GameObject desativarCountEnemies;
    public Button restart;
    public static bool timeGamveOver;


	Animator anim;
	float restartTime;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
        restart.interactable = false;
        timeGamveOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth.currentHealth <= 0 || timeGamveOver == true) {
			anim.SetTrigger ("GameOver");
            desativarTimeProxWaves.SetActive(false);
            desativarCountEnemies.SetActive(false);
            restartTime += Time.deltaTime;
            restart.interactable = true;
		}
        
	}
}
