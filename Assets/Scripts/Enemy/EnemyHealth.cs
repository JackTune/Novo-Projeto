using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour {

    //Menu
    MENU menu;

    //Vida do Inimigo
    public float startingHealth = 100;
    public float currentHealth;
    float tempoDano;

    //Velocidade no qual o inimigo desaparecerá
    public float sinkSpeed = 2.5f;

    //Pontos que o inimigo dá
    public int scoreValue = 10;

    //Váriaveis do Player para Upar
    public float experienceValue = 10;
    GameObject player;
    PlayerExperience playerExperience;
    WavesDetailsSurvival wavesDetailsSurvival;

    //Modificações


    //Animações e Audios
    // public AudioClip deathClip;
    //Animator anim;
    // AudioSource enemyAudio;

    //GM
    GameObject GM;
    Skills skills;
    ManagerScene gmManagerScene;

    //Levar dano
    public Image damageImage;
    public GameObject damageGO;

    //Particulas e Colliders
    CapsuleCollider capsuleCollider;

    //Booleanos
    bool isDead;
    bool isSinking;
    bool enemyTakeDamage;

    // Use this for initialization
    void Awake()
    {
        //Pegar os objetos do GM, e setar as skills e o ManagerScene
        GM = GameObject.FindGameObjectWithTag("GameController");
        skills = GM.GetComponent<Skills>();
        gmManagerScene = GM.GetComponent<ManagerScene>();
        gmManagerScene.GetScenesModeGame();

        //Verificar se o modo de jogo é survival
        if (gmManagerScene.gameMode == "Survival")
        {
            print("Entrou no if");
            wavesDetailsSurvival = GameObject.FindGameObjectWithTag("Canvas").GetComponent<WavesDetailsSurvival>();
        }

        // anim = GetComponent<Animator>();
        // enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        damageGO.SetActive(false);
        currentHealth = startingHealth;

        //Achar o PlayerExperience
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerExperience = player.GetComponent<PlayerExperience>();
        }
    }

    // Update is called once per frame
    void Update () {
        //Se Ele estiver morto, descendo no chão.
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
    public void TakeDamage(float amount)
    {
        enemyTakeDamage = true;
        damageGO.SetActive(true);

        if (isDead)
        {
            return;
        }

        //Mostrar na lifebar do inimigo
        currentHealth -= amount;
        damageImage.fillAmount = currentHealth / startingHealth;

        // enemyAudio.Play();

        if (currentHealth <= 0)
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

        //Mortes para ativar a furia
        if (skills.isUsingFury == false)
        {
            skills.currentForActiveFury++;
            skills.AddFury();
        }

        //Modos de Jogo
        if (gmManagerScene.gameMode == "Survival")
        {
            print("Entrou");
            //Modo Survival
            wavesDetailsSurvival.qntKillsEnemy++;
            wavesDetailsSurvival.qntGold += scoreValue;
        }
        else
        {
            //Modo Arcade
            //WavesDetails
            WavesDetails.countEnemies--;
            WavesDetails.isDeadEnemy = true;
            playerExperience.AddExperience(experienceValue);

        }



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
        ScoreManager.gold += scoreValue;
        Destroy(gameObject, 2f);
    }


   


}
