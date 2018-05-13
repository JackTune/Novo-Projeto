using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionsScript : MonoBehaviour {

    [System.Serializable]
    public class Potions{
        public Button buyPotionButton;
        public Button usePotionButton;
        public Text qntPotionsText;
        public int valuePotion;
        public int qntPotions;
        public float porcentagemDeAumento;
    }

    public GameObject potionsGameObject;
    GameOverManager gameOver;
    LevelCompleteManager levelComplete;

    //Canvas
    public Potions potionHP = new Potions();
    [System.NonSerialized]
    public bool playerCanCure;
    [System.NonSerialized]
    public bool playerCanGainMana;
    public Potions potionMana = new Potions();

    public Potions power = new Potions();
    [System.NonSerialized]
    public bool playerCanUsePower;

    //Player 
    GameObject player;
    PlayerHealth playerHealth;
    Mana playerMana;
    GameObject gm;
    Skills skills;

    //GM
    ManagerScene managerScene;

    private void Awake()
    {
        managerScene = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManagerScene>();
        managerScene.GetScenesModeGame();
        gameOver = GetComponent<GameOverManager>();
        levelComplete = GetComponent<LevelCompleteManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMana = player.GetComponent<Mana>();
        gm = GameObject.FindGameObjectWithTag("GameController");
        skills = gm.GetComponent<Skills>();


    }


    // Use this for initialization
    void Start () {

        //Colocar um playerprefs que verifica se o dinheiro atual é igual ou maior que o valor das poções, se sim, ele é interagivel.
        if (managerScene.gameMode != "Survival")
        {
            VerificationsPlayerPrefs();
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (gameOver.gameOver)
        {
            potionsGameObject.SetActive(false);
        }

        if (levelComplete.levelComplete)
        {
            potionsGameObject.SetActive(false);
        }

        VerificationInteractableButtons();

    }

    public void UsePotionHP()
    {
        //Se o Personagem clicar na porção. Curará % de vida
        if(potionHP.qntPotions != 0)
        {
            potionHP.qntPotions--;
            potionHP.qntPotionsText.text = potionHP.qntPotions.ToString();
            playerCanCure = true;
            playerHealth.currentHealth += playerHealth.maxHealth * potionHP.porcentagemDeAumento;
            if(playerHealth.currentHealth > playerHealth.maxHealth)
            {
                playerHealth.currentHealth = playerHealth.maxHealth;
            }
        }
        else
        {
            potionHP.usePotionButton.interactable = false;
        }
    }

    public void UsePotionMana()
    {
        //Se o Personagem clicar na porção. Preencherá % de mana
        if (potionMana.qntPotions != 0)
        {
            potionMana.qntPotions--;
            potionMana.qntPotionsText.text = potionMana.qntPotions.ToString();
            playerCanGainMana = true;
            playerMana.currentMana += playerMana.startMana * potionMana.porcentagemDeAumento;
            if (playerMana.currentMana > playerMana.startMana)
            {
                playerMana.currentMana = playerMana.startMana;
            }
        }
        else
        {
            potionMana.usePotionButton.interactable = false;
        }
    }

    public void UsePower()
    {
        // se o person clocar na power, Preencherá % de power
        if(power.qntPotions != 0)
        {
            power.qntPotions--;
            power.qntPotionsText.text = power.qntPotions.ToString();
            playerCanUsePower = true;
            skills.currentForActiveFury += skills.qntForActiveFury * power.porcentagemDeAumento;
            if(skills.currentForActiveFury > skills.qntForActiveFury)
            {
                skills.currentForActiveFury = skills.qntForActiveFury;
            }
            else
            {
                power.usePotionButton.interactable = false;
            }
        }
    }

    public void BuyPotion(Button button)
    {
        //Quando o personagem comprar uma porção.

        if(button.gameObject.name == "BuyPotionHP")
        {
            //Comprou poção de HP
            potionHP.qntPotions++;
            potionHP.qntPotionsText.text = potionHP.qntPotions.ToString();
            ScoreManager.gold -= potionHP.valuePotion;
        }
        else if (button.gameObject.name == "BuyPotionMana")
        {
            //Comprou poção de Mana
            potionMana.qntPotions++;
            potionMana.qntPotionsText.text = potionMana.qntPotions.ToString();
            ScoreManager.gold -= potionMana.valuePotion;
        }
        else
        {
            // Comprou power
            power.qntPotions++;
            power.qntPotionsText.text = power.qntPotions.ToString();
            ScoreManager.gold -= power.valuePotion;
        }
    }


    void VerificationsPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("QntPotionHP"))
        {
            print("Existe");
            potionHP.qntPotions = PlayerPrefs.GetInt("QntPotionHP");
            if (potionHP.qntPotions != 0)
            {
                potionHP.qntPotionsText.text = potionHP.qntPotions.ToString();
                potionHP.usePotionButton.interactable = true;
            }
            else
            {
                potionHP.usePotionButton.interactable = false;
            }
        }
        else
        {
            potionHP.usePotionButton.interactable = false;
        }

        if (PlayerPrefs.HasKey("QntPotionMana"))
        {
            potionMana.qntPotions = PlayerPrefs.GetInt("QntPotionMana");
            if (potionMana.qntPotions != 0)
            {
                potionMana.qntPotionsText.text = potionMana.qntPotions.ToString();
                potionMana.usePotionButton.interactable = true;
            }
            else
            {
                potionMana.usePotionButton.interactable = false;
            }
        }
        else
        {
            potionMana.usePotionButton.interactable = false;
        }

        if (PlayerPrefs.HasKey("QntPower"))
        {
            power.qntPotions = PlayerPrefs.GetInt("QntPower");
            if (power.qntPotions != 0)
            {
                power.qntPotionsText.text = power.qntPotions.ToString();
                power.usePotionButton.interactable = true;
            }
            else
            {
                power.usePotionButton.interactable = false;
            }
        }
        else
        {
            power.usePotionButton.interactable = false;
        }


        if (ScoreManager.gold < potionHP.valuePotion)
        {
            potionHP.buyPotionButton.interactable = false;
        }
        else
        {
            potionHP.buyPotionButton.interactable = true;
        }

        if (ScoreManager.gold < potionMana.valuePotion)
        {
            potionMana.buyPotionButton.interactable = false;
        }
        else
        {
            potionMana.buyPotionButton.interactable = true;
        }

        if(ScoreManager.gold < power.valuePotion)
        {
            power.buyPotionButton.interactable = true;
        }
        else
        {
            power.buyPotionButton.interactable = false;
        }
    }

    void VerificationInteractableButtons()
    {
        if (ScoreManager.gold >= potionHP.valuePotion)
        {
            potionHP.buyPotionButton.interactable = true;
        }
        else
        {
            potionHP.buyPotionButton.interactable = false;
        }

        if (potionHP.qntPotions > 0)
        {
            potionHP.usePotionButton.interactable = true;
        }
        else
        {
            potionHP.usePotionButton.interactable = false;
        }

        if (ScoreManager.gold >= potionMana.valuePotion)
        {
            potionMana.buyPotionButton.interactable = true;
        }
        else
        {
            potionMana.buyPotionButton.interactable = false;
        }

        if (potionMana.qntPotions > 0)
        {
            potionMana.usePotionButton.interactable = true;
        }
        else
        {
            potionMana.usePotionButton.interactable = false;
        }

        if (ScoreManager.gold >= power.valuePotion)
        {
            power.buyPotionButton.interactable = true;
        }
        else
        {
            power.buyPotionButton.interactable = false;
        }

        if (power.qntPotions > 0)
        {
            power.usePotionButton.interactable = true;
        }
        else
        {
            power.usePotionButton.interactable = false;
        }
    }
}
