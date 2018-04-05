﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour {

    public Image experienceImage;

    public float currentExperience;

    public float startingExperience = 0;

    public float maxExperience;

    PlayerHealth playerHealth;

    public Text levelText;

	// Use this for initialization
	void Awake () {
        playerHealth = GetComponent<PlayerHealth>();

    }

    

    void Start()
    {

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
        if (currentExperience >= maxExperience)
        {
            experienceImage.fillAmount = 0;
            LevelUp();
            levelText.text = "" + playerHealth.playerLevel;

        }
        
            
        
    }

    public void AddExperience(float experience)
    {
        currentExperience += experience;

        experienceImage.fillAmount = currentExperience / maxExperience;

    }

    void LevelUp()
    {

        currentExperience = 0;
        maxExperience += maxExperience * 0.2f;
        playerHealth.playerLevel += 1;

        

        //experienceValue = experienceValue + experienceValue*levelPlayer*0.1f
       
    }


}
