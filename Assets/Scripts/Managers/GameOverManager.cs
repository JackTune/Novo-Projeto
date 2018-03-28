using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {
	public PlayerHealth playerHealth;
	public float restartDelay = 5f;
    public GameObject desativarQntWaves;
    public GameObject desativarTimeProxWaves;

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
            desativarQntWaves.SetActive(false);
            desativarTimeProxWaves.SetActive(false);
            restartTime += Time.deltaTime;
		}
	}
}
