﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

    GameObject player;
    PlayerHealth playerHealth;
    PlayerExperience playerExperience;
    PlayerShooting playerShooting;

    // Colocar todo valor que foi passado de uma fase para outra nas variáveis

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            playerExperience = player.GetComponent<PlayerExperience>();
            playerShooting = player.GetComponentInChildren<PlayerShooting>();

        }
    }
    // salvar todas as informações que precisamos para a troca de fases 
    public void Saves()
    {
        PlayerPrefs.SetInt("Score", ScoreManager.gold);
        PlayerPrefs.SetInt("Level", playerHealth.playerLevel);
        PlayerPrefs.SetFloat("Experience", playerExperience.currentExperience);
        PlayerPrefs.SetFloat("MaxExperience", playerExperience.maxExperience);
        PlayerPrefs.SetFloat("Damage", playerShooting.damagePerShot);
        PlayerPrefs.SetFloat("MaxHealth", playerHealth.maxHealth);
    }
}
