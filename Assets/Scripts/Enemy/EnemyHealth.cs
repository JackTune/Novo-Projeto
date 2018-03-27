using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    //Vida do Inimigo
    public float startingHealth = 100;
    public float currentHealth;

    //Velocidade no qual o inimigo desaparecerá
    public float sinkSpeed = 2.5f;

    //Pontos que o inimigo dá
    public int scoreValue = 10;

    //Animações e Audios
    // public AudioClip deathClip;
    //Animator anim;
    // AudioSource enemyAudio;


    //Imagem do Damage
    public Image damageImage;

    //Particulas e Colliders
    ParticleSystem hitParticle;
    CapsuleCollider capsuleCollider;

    //Booleanos
    bool isDead;
    bool isSinking;

    // Use this for initialization
     void Awake()
    {
       // anim = GetComponent<Animator>();
       // enemyAudio = GetComponent<AudioSource>();
        hitParticle = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update () { 

        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }

	}


    //Inimigo leva dano
    public void TakeDamege(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }

       // enemyAudio.Play();

        currentHealth -= amount;

        damageImage.fillAmount = currentHealth / startingHealth;

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
