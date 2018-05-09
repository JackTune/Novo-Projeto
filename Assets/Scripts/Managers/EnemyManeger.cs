using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManeger : MonoBehaviour {

    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTimeMin = 1f;
    public float spawnTimeMax = 3f;
    public Transform[] spawnPoints;

    private void Start()
    {
        float spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
        //Repete a esta linha de código
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if(playerHealth.currentHealth <= 0)
        {
            return;
        }
        //Randomiza os pontos de spawnar
        int spawnPointsIndex = Random.Range(0, spawnPoints.Length);
        //Instancia os inimigos
        Instantiate(enemy, spawnPoints[spawnPointsIndex].position, spawnPoints[spawnPointsIndex].rotation);
    }
}
