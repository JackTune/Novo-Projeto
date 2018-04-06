using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLvL : MonoBehaviour {
    //Player
    PlayerExperience playerExperience;
    PlayerHealth HP;
    PlayerShooting attack;


    //UI
    public Button botaoVida;
    public Button botaoAtaque;
    public GameObject imageUp;

    private void Awake()
    {
        playerExperience = GetComponent<PlayerExperience>();
        HP = GetComponent<PlayerHealth>();
        attack = GetComponentInChildren<PlayerShooting>(); 
    }
    // Use this for initialization
    void Start()
    {

        ButtonsInteractable(false);


        if (playerExperience.levelUp)
        {
            imageUp.SetActive(true);
            ButtonsInteractable(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerExperience.levelUp)
        {
            imageUp.SetActive(true);
            botaoAtaque.interactable = true;
            botaoVida.interactable = true;
        }

    }

    public void UpgradeVida()
    {
        HP.maxHealth += HP.maxHealth * 0.01f;
        imageUp.SetActive(false);
        ButtonsInteractable(false);
    }
    public void UpgradeAtaque()
    {
        attack.damagePerShot += 2;
        imageUp.SetActive(false);
        ButtonsInteractable(false);
    }


    void ButtonsInteractable(bool ativoOuNao)
    {
        botaoAtaque.interactable = ativoOuNao;
        botaoVida.interactable = ativoOuNao;
    }
}
