using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour {

    public Image experienceImage;
    
    public Text textCountPoints;

    public float currentExperience;

    public float startingExperience = 0;

    public float maxExperience;

    PlayerHealth playerHealth;

    public int countLvlUp = 0;

    public Text levelText;

    [System.NonSerialized]
    public bool levelUp = false;

	// Use this for initialization
	void Awake () {
        playerHealth = GetComponent<PlayerHealth>();

    }

    

    void Start()
    {
        // Pegar valor das variaáveis em fases passadas ou quando incia o jogo
        if (PlayerPrefs.HasKey("MaxExperience"))
        {
            maxExperience = PlayerPrefs.GetFloat("MaxExperience");
        }

        if (PlayerPrefs.HasKey("Experience"))
        {

            currentExperience = PlayerPrefs.GetFloat("Experience");
            experienceImage.fillAmount = currentExperience / maxExperience;

        }

        if (PlayerPrefs.HasKey("Level"))
        {
            playerHealth.playerLevel = PlayerPrefs.GetInt("Level");
            levelText.text = "" + playerHealth.playerLevel;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Se a experiencia chegar ao máximo, upa 
        if (currentExperience >= maxExperience)
        {
            experienceImage.fillAmount = 0;
            LevelUp();
            levelText.text = "" + playerHealth.playerLevel;

        }
        else
        {
            levelUp = false;
        }
        
    }
    // Adicionar ao player
    public void AddExperience(float experience)
    {
        currentExperience += experience;

        experienceImage.fillAmount = currentExperience / maxExperience;

    }

    // Upar
    void LevelUp()
    {
        levelUp = true;
        countLvlUp++;
        textCountPoints.text = "Points: " + countLvlUp;
        currentExperience = 0;
        maxExperience += maxExperience * 0.2f;
        playerHealth.playerLevel += 1;

        

        //experienceValue = experienceValue + experienceValue*levelPlayer*0.1f
       
    }


}
