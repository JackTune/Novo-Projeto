using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    //Posição do player
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    EnemyAttack enemyAttack;

    //GM
    GameObject GM;
    Skills skills;
    //Congelar
    public int timeOfFreeze;

    //IA
    NavMeshAgent nav;
    float startSpeed;
    bool entrouNoColliderStormIce = true;
    bool entrouNoColliderBlackHole = true;
    

    // Use this for initialization
    private void Awake()
    {
        //Encontrar o Objeto do Player e seus componentes
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        startSpeed = nav.speed;

        //Pegando o objeto da classe skills
        GM = GameObject.FindGameObjectWithTag("GameController");
        skills = GM.GetComponent<Skills>();
    }

    // Update is called once per frame
    void Update () {

        //Verifica se o player e o inimigo estão vivos, para assim o inimigo seguir o Player
        if (playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
	}

    //Colisões
    private void OnTriggerEnter(Collider other)
    {
        //Caso o inimigo colida com a skill "StormIce"
        if(other.CompareTag("StormIce") && entrouNoColliderStormIce)
        {
            
            StartCoroutine(StormIce());
            entrouNoColliderStormIce = false;
        }

        //Caso o inimigo colida com a skill "BlackHole"
        if (other.CompareTag("BlackHole") && entrouNoColliderBlackHole)
        {
            StartCoroutine(BlackHole());
            entrouNoColliderBlackHole = false;
        }

        //Caso o inimigo colida com a skill "Freeze"
        if (other.CompareTag("Congelar"))
        {
            StartCoroutine(Congelar(timeOfFreeze));
        }

    }

  
    /*IEnumerators*/

    //Faz mudanças no ataque e movimentação do inimigo
    IEnumerator Congelar(int time)
    {
        enemyAttack.attackDamage = 0;
        enemyHealth.TakeDamage(skills.damageFreeze);
        nav.speed = 0;
        yield return new WaitForSeconds(time);
        enemyAttack.attackDamage = enemyAttack.startDamage;
        nav.speed = startSpeed;
    }

    //Deixa o inimigo lento
    IEnumerator StormIce()
    {

        nav.speed = startSpeed * 0.5f;
        for (int i = 0; i < 3; i++)
        {

            enemyHealth.TakeDamage(skills.damageStormIce);
            yield return new WaitForSeconds(1.1f);
        }
        yield return new WaitForSeconds(1f);
        nav.speed = startSpeed;
        entrouNoColliderStormIce = true;

    }

    //Deixa o inimigo lento
    IEnumerator BlackHole()
    {

        nav.speed = startSpeed * 0.2f;
        for (int i = 0; i < 5; i++)
        {

            enemyHealth.TakeDamage(skills.damageBlackHole);
            yield return new WaitForSeconds(1.1f);
        }
        yield return new WaitForSeconds(1f);
        nav.speed = startSpeed;
        entrouNoColliderBlackHole = true;

    }
}
