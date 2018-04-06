using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

    GameObject player;
    PlayerHealth playerHealth;
    PlayerExperience playerExperience;
    PlayerShooting playerShooting;

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
    // Use this for initialization
    public void Saves()
    {
        PlayerPrefs.SetInt("Score", ScoreManager.score);
        PlayerPrefs.SetInt("Level", playerHealth.playerLevel);
        PlayerPrefs.SetFloat("Experience", playerExperience.currentExperience);
        PlayerPrefs.SetFloat("MaxExperience", playerExperience.maxExperience);
        PlayerPrefs.SetInt("Damage", playerShooting.damagePerShot);
        PlayerPrefs.SetFloat("MaxHealth", playerHealth.maxHealth);
    }
}
