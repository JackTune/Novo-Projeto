using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWaves : MonoBehaviour {
    //Enemies
    public GameObject enemies;
    public GameObject auxEnemies;
    public EnemyAttack attackEnemy;
    public EnemyHealth enemyHealth;

    int waveAtual;
    int qntEnemiesWave;
    int qntEnemiesGerados;
    int qntEnemiesVivos;

    public float tempoRespawn;
    bool newWave;

    public Transform[] spawnPoints;


	// Use this for initialization
	void Start () {
        tempoRespawn = 0;
        qntEnemiesWave = 10;
        waveAtual = 0;
        qntEnemiesVivos = 0;
        qntEnemiesGerados = 0;
        newWave = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (newWave)
        {
            if(qntEnemiesWave > qntEnemiesGerados)
            {
                //Randomiza os pontos
                int spawnPointsIndex = Random.Range(0, spawnPoints.Length);

                //Instancia os inimigos
                Instantiate(enemies, spawnPoints[spawnPointsIndex].position, spawnPoints[spawnPointsIndex].rotation);
                qntEnemiesGerados++;
            }
        }
	}
}
