using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWaves : MonoBehaviour {

    //Estágios da Wave: Spawnando, Esperando, ou Contando
    public enum SpawnState { SPAWNING, WAITING, COUNTING, FINISH };


    //Classe Wave
    [System.Serializable]
    public class Wave
    {
        public string nome;
        //public Transform enemy;
        public Transform[] enemies;
        public int count;
        public float rate;
    }

    //Controle de Waves
    public Wave[] waves;
    private int nextWave = 0;
    public float timeBetweenWaves = 10f;
    private float waveCountDown;
    public Transform[] spawnPoints;

    //Auxiliares do WavesDetails
    [System.NonSerialized]
    public bool finish;
    [System.NonSerialized]
    public int numberWave;
    [System.NonSerialized]
    public int countTempo;

    

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCountDown = timeBetweenWaves;
        numberWave = nextWave;
        countTempo = (int)waveCountDown;

    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            //Check if os inimigos ainda estão vivos
            if (!EnemiesIsAlive())
            {
                //Começa um novo round
                WaveComplete();
                numberWave = nextWave;
                return;
            }
            else
            {
                return;
            }
        }


        if(waveCountDown <= 0)
        {
            if(state != SpawnState.SPAWNING && state != SpawnState.FINISH)
            {
                //Começar spawnando a wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
            
        }
        else
        {
            waveCountDown -= Time.deltaTime;
            countTempo = (int)waveCountDown;
        }
    }


    //Final de toda Wave
    void WaveComplete()
    {
        Debug.Log("Wave Completada");

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            //TODAS WAVES COMPLETAS / Estágio Completado
            nextWave = 0;
            finish = true;
            state = SpawnState.FINISH;
            print("Todas waves completadas");
        }
        else
        {
            nextWave++;
        }

    }


    //Pergunta se os inimigos ainda estão vivos
    bool EnemiesIsAlive()
    {

        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }


    //Corotina para Spawnar a wave
    IEnumerator SpawnWave(Wave wave)
    {

        //Spawnando Wave

        state = SpawnState.SPAWNING;

        //Spawn
        for (int i = 0; i < wave.count; i++)
        {
            int spawnRandomIndex = Random.Range(0, wave.enemies.Length);
            SpawnEnemy(wave.enemies[spawnRandomIndex]);
            yield return new WaitForSeconds(1f / wave.rate);
        }


        state = SpawnState.WAITING;


        yield break;
    }

    //Método que spawna o inimigo
    void SpawnEnemy(Transform enemy)
    {
        //Spawn Enemy
        Debug.Log("Spawning Enemy: " + enemy.name);

        int spawnPointsIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointsIndex].position, spawnPoints[spawnPointsIndex].rotation);
        
    }
}
