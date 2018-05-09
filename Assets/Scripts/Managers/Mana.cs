using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour {

    public float startMana = 100f;
   
    public float currentMana;
    public Image manaImage;
    public float manaGainForSecond = 1f;
    float countRealTime;

    // Use this for initialization
    void Start () {
        currentMana = startMana;
	}
	
	// Update is called once per frame
	void Update () {

        countRealTime += Time.deltaTime;
        //Verifica a cada segundo
        if(currentMana < startMana && countRealTime >= 1f)
        {
            countRealTime = 0;
            SomarMana();
        }

	}

    //Função utilizada quando o player gastar sua mana
    public void GastarMana(float amount)
    {
        currentMana -= amount;

        manaImage.fillAmount = currentMana / startMana;
    }

    //Mana obtida a cada segundo
    void SomarMana()
    {
        currentMana += manaGainForSecond;
        manaImage.fillAmount = currentMana / startMana;
    }
}
