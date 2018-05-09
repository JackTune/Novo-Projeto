using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesDetailsSurvival : MonoBehaviour {

    //SurvivalMode
    [System.NonSerialized]
    public int qntKillsEnemy, qntGold;
    string time;
    int seconds, minutes, hours;
    float timer;
    public Text KillsGameOverText, TimerGameOverText, GoldGameOverText, KillsText, TimerText, GoldText;
    public GameObject showGameOverInformations;
    public PlayerHealth playerHealth;
    bool canGetGameOver;
    

    // Use this for initialization
    void Start()
    {
        canGetGameOver = true;
    }

	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        seconds = (int)(timer % 60);
        minutes = (int)(timer / 60) % 60;
        //hours = (int)(timer / 3600) % 24;
        //Setando o formato da string
        time = string.Format("{0:00}:{1:00}",minutes,seconds);

        //Apresenta na tela, as quantidades de mortes, dinheiro e tempo
        KillsText.text = qntKillsEnemy.ToString();
        GoldText.text = qntGold.ToString();
        TimerText.text = time;

        if (playerHealth.currentHealth <= 0 && canGetGameOver)
        {
            canGetGameOver = false;
            StartCoroutine(Esperar());
        }
        
	}

    //Modo Survival

    //Informações do Game Over
    void DetailsSurvival()
    {
        KillsGameOverText.text += qntKillsEnemy.ToString();
        TimerGameOverText.text += time;
        GoldGameOverText.text += qntGold.ToString();
        showGameOverInformations.SetActive(true);
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(3f);
        DetailsSurvival();
    }

}
