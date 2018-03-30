using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
	public PlayerHealth playerHealth;
	public float restartDelay = 5f;
    public GameObject desativarTimeProxWaves;
    public Button restart;


	Animator anim;
	float restartTime;
	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();

        
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth.currentHealth <= 0) {
			anim.SetTrigger ("GameOver");
            desativarTimeProxWaves.SetActive(false);
            restartTime += Time.deltaTime;
            restart.interactable = true;
		}
        else
        {
            restart.interactable = false;
        }
	}
}
