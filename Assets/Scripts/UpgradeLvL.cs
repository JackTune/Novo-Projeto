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
    public GameObject imageCountPoints;

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
            imageCountPoints.SetActive(true);
            ButtonsInteractable(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerExperience.levelUp)
        {
            imageUp.SetActive(true);
            imageCountPoints.SetActive(true);
            ButtonsInteractable(true);
        }

    }

    public void UpgradeVida()
    {
        HP.maxHealth += HP.maxHealth * 0.01f;
        playerExperience.countLvlUp--;
        Upgrade(playerExperience.countLvlUp);
    }
    public void UpgradeAtaque()
    {
        attack.damagePerShot += 2;
        playerExperience.countLvlUp--;
        Upgrade(playerExperience.countLvlUp);
    }


    void Upgrade(int count)
    {

        if (count <= 0)
        {
            imageUp.SetActive(false);
            ButtonsInteractable(false);
            playerExperience.countLvlUp = 0;
        }
        playerExperience.textCountPoints.text = "Points: " + playerExperience.countLvlUp;
    }

        void ButtonsInteractable(bool ativoOuNao)
    {
        botaoAtaque.interactable = ativoOuNao;
        botaoVida.interactable = ativoOuNao;
    }
}
