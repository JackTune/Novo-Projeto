using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    //Variables Gun
    public int damagePerShot = 20;
    public float TimeBetweenBullets = 2f;

    PlayerHealth playerHealth;

    float timer;
    ParticleSystem gunParticle;

    //AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    private void Awake()
    {
        gunParticle = GetComponent<ParticleSystem>();
       // gunLine = GetComponent<LineRenderer>();
      //  gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Damage"))
        {
            damagePerShot = PlayerPrefs.GetInt("Damage");
        }
    }


    private void FixedUpdate()
    {
        timer += Time.deltaTime;


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
                print("Tiro Pegou");
                enemyHealth.TakeDamage(damagePerShot);
            }
        }
    }


}
