using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    //Dano do inimigo
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    [System.NonSerialized]
    public int startDamage;
    //Animator anim;

    //Classes do player
    GameObject player;
    PlayerHealth playerHealth;

    //Inimigo
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        //anim = GetComponent<Animator>();
        startDamage = attackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        //Morte do player
        if (playerHealth.currentHealth <= 0)
        {
            //animation.SetTrigger("PlayerDead");
        }
    }


    //Player tocou no inimigo
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            
            playerInRange = true;
        }
   
    }


    //Player saiu da colisão do inimigo
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }



    //Ataque do inimigo
    void Attack()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamege(attackDamage);
        }
    }
}
