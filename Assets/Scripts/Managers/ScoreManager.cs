using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour {

	public static int gold;

	Text text;

	// Use this for initialization
	void Awake () {
		text = GetComponent<Text> ();
		gold = 0;


    }
    // Se existir algum dinheiro quando iniciar a partida que tinha arrecadado na fase passada ou quando saiu do jogo, é recuperado
    private void Start()
    {

        if (PlayerPrefs.HasKey("Score"))
        {
            gold = PlayerPrefs.GetInt("Score");
        }
    }
    // Update is called once per frame
    void Update () {
		text.text = "" + gold;
	}


}
