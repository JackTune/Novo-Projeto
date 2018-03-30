using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    //Vida do Inimigo
    public float startingHealth = 100;
    public float currentHealth;
    float tempoDano;

    //Velocidade no qual o inimigo desaparecerá
    public float sinkSpeed = 2.5f;

    //Pontos que o inimigo dá
    public int scoreValue = 10;

    //Animações e Audios
    // public AudioClip deathClip;
    //Animator anim;
    // AudioSource enemyAudio;


    //Levar dano
    public Image damageImage;
    public GameObject damageGO;

    //Particulas e Colliders
    ParticleSystem hitParticle;
    CapsuleCollider capsuleCollider;

    //Booleanos
    bool isDead;
    bool isSinking;
    bool enemyTakeDamage;

    // Use this for initialization
     void Awake()
    {
       // anim = GetComponent<Animator>();
       // enemyAudio = GetComponent<AudioSource>();
        hitParticle = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        damageGO.SetActive(false);
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update () { 

        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }


        //Enemy esconde a life bar
        if (enemyTakeDamage)
        {
            tempoDano += Time.deltaTime;
        }

        if(tempoDano > 2f)
        {
            tempoDano = 0;
            enemyTakeDamage = false;
            damageGO.SetActive(false);
        }

    }


    //Inimigo leva dano
    public void TakeDamege(int amount, Vector3 hitPoint)
    {
        enemyTakeDamage = true;
        damageGO.SetActive(true);

        if (isDead)
        {
            return;
        }

        
        currentHealth -= amount;
        damageImage.fillAmount = currentHealth / startingHealth;

        // enemyAudio.Play();

        //Animação da particula colidindo
        hitParticle.transform.position = hitPoint;
        hitParticle.Play();

        if(currentHealth <= 0)
        {
            Death();
            StartSinking();
        }
    }

    //Morte do inimigo
    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        //WavesDetails
        WavesDetails.countEnemies--;
        WavesDetails.isDeadEnemy = true;

        //anim.SetTrigger ("Dead");

       // enemyAudio.clip = deathClip;
        //enemyAudio.Play();
    }


    //Começar a afundar
    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}
