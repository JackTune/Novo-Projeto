using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {


    //Vida do player
    public int startingHealth = 100;
    public int currentHealth;
    public Slider sliderHealth;

    //Imagem do Damage
    public Image damageImage;
    // public AudioClip deathClip;
    public float flashSepeed;
    public Color flashColour;

    //Animações e Audios
    // Animator anim;
    // AudioSource playerAudio;
    PlayerController player;
    PlayerShooting playerShooting;


    //Booleanos
    bool isDead;
    bool dameged;

	// Use this for initialization
	void Awake () {

    //    anim = GetComponent<Animator>();
    //    playerAudio = GetComponent<AudioSource>();
        player = GetComponent<PlayerController>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (dameged)
        {
                damageImage.color = flashColour;
            
        }
        else
        {
           damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSepeed * Time.deltaTime);
        }
        dameged = false;
	}


    //Player leva dano
    public void TakeDamege(int amount)
    {
        dameged = true;

        currentHealth -= amount;

        sliderHealth.value = currentHealth;

        //  playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();

        }
    }


    //Player Morre
    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();
        //anim.SetTrigger("Die");

       // playerAudio.clip = deathClip;
        //playerAudio.Play();

        player.enabled = false;
    }
}
