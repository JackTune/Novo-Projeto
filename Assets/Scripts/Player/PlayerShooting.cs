using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    //Menu
    ManagerScene managerScene;

    //Variables Gun
    public float damagePerShot = 20;
    public float TimeBetweenBullets = 2f;

    PlayerHealth playerHealth;

    float timer;
    ParticleSystem gunParticle;

    //AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    private void Awake()
    {
        //Obtém o managerScene para poder obter os modos de jogo
        managerScene = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManagerScene>();
        managerScene.GetScenesModeGame();


        gunParticle = GetComponent<ParticleSystem>();
       // gunLine = GetComponent<LineRenderer>();
      //  gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    private void Start()
    {
        //Verifica o modo de jogo
        if (managerScene.gameMode != "Survival")
        {
            if (PlayerPrefs.HasKey("Damage"))
            {
                damagePerShot = PlayerPrefs.GetFloat("Damage");
            }
        }
        
    }


    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        //Verifica se pode atirar
        if(Input.GetButton ("Fire1") && timer >= TimeBetweenBullets && playerHealth.currentHealth > 0)
        {
            Shoot();
        }

        if(timer >= TimeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLight.enabled = false;
    }

    //Método atirar
    void Shoot()
    {
        timer = 0f;

       // gunAudio.Play();

        gunLight.enabled = true;

        gunParticle.Stop();
        gunParticle.Play();

    }


    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Procura se o objeto colidido tem o script EnemyHealth
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();


            //Caso Exista um enemyHealth naquele objeto
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot);
            }
        }
    }


}
