using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {


    //Vida do player
    public float startingHealth;
    public float maxHealth = 100f;
    public float currentHealth;
    public Image imageHealth;

    //Imagem do Damage
    public Image damageImage;
    // public AudioClip deathClip;
    public float flashSepeed;
    public Color flashColour;

    //Faz parte da experiencia do player
    
    public int playerLevel = 1;
    

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

    }

    private void Start()
    {
        /*if (PlayerPrefs.HasKey("Level"))
        {
            
            playerLevel = PlayerPrefs.GetInt("Level");
        }*/
        if (PlayerPrefs.HasKey("MaxHealth"))
        {
            maxHealth = PlayerPrefs.GetFloat("MaxHealth");
        }

        startingHealth = maxHealth;
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

        imageHealth.fillAmount = currentHealth / maxHealth;
        
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
