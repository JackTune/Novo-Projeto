using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    //Geral
    ManagerScene managerScene;
	public PlayerHealth playerHealth;
	public float restartDelay = 5f;
    public Button restart;
    public Button backMenu;
    public static bool timeGameOver;

    
	Animator anim;
	float restartTime;

    private void Awake()
    {
        managerScene = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManagerScene>();
        managerScene.GetScenesModeGame();
    }

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
        restart.interactable = false;
        if(managerScene.gameMode == "Survival")
        {
            backMenu.interactable = false;
        }
        timeGameOver = false;
	}
	
	// Update is called once per frame
	void Update () {

        //Quando o player morrer
		if (playerHealth.currentHealth <= 0 || timeGameOver == true) {
			anim.SetTrigger ("GameOver");
            restartTime += Time.deltaTime;
            if (managerScene.gameMode == "Survival")
            {
                backMenu.interactable = true;
            }
            restart.interactable = true;
		}
        
	}

}
