using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWaves : MonoBehaviour {

    //Estágios da Wave: Spawnando, Esperando, ou Contando
    public enum SpawnState { SPAWNING, WAITING, COUNTING, FINISH };


    //Classe Wave
    [System.Serializable]
    public class Wave
    {
        public string nome;
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
    public PlayerHealth playerHealth;

    //Tempo de Jogo
    public Text timerText;
    public float startTimer;
    [System.NonSerialized]
    public float auxTimer;
    string timerString;
    int seconds, minutes;
    bool comecarContar;

    //Auxiliares do WavesDetails
    [System.NonSerialized]
    public bool finish;
    [System.NonSerialized]
    public int numberWave;
    [System.NonSerialized]
    public int countTempo;
    [System.NonSerialized]
    public bool waveComplete = true;

    //Animations
    

    

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCountDown = timeBetweenWaves;
        numberWave = nextWave;
        countTempo = (int)waveCountDown;

        //Setando o tempo
        auxTimer = startTimer;
        seconds = (int)(startTimer % 60);
        minutes = (int)(startTimer / 60) % 60;
        timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;

    }

    private void Update()
    {

        //Setando o tempo
        if (comecarContar)
        {
            auxTimer -= Time.deltaTime;
            seconds = (int)(auxTimer % 60);
            minutes = (int)(auxTimer / 60) % 60;

            timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timerString;
            
            if (auxTimer <= 0)
            {
                print("Entrou no if de time");
                GameOverManager.timeGameOver = true;
                comecarContar = false;
            }
        }

        

        if (state == SpawnState.WAITING)
        {
            //Check if os inimigos ainda estão vivos
            if (!EnemiesIsAlive())
            {
                //Começa um novo round
                WaveComplete();

                return;
            }
            else
            {
                waveComplete = false;
                return;
            }
        }


        if(waveCountDown <= 0)
        {
            if(state != SpawnState.SPAWNING && state != SpawnState.FINISH)
            {
                //Começar spawnando a wave
                comecarContar = true;
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
            waveComplete = false;
            comecarContar = false;
            print("Todas waves completadas");
        }
        else
        {
            comecarContar = false;
            auxTimer = startTimer;
            waveComplete = true;
            nextWave++;
            numberWave = nextWave;
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
            //Inimigo Randomizado
            SpawnEnemy(wave.enemies[spawnRandomIndex]);
            yield return new WaitForSeconds(1f * wave.rate);
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
        if (playerHealth.currentHealth > 0)
        {
            Instantiate(enemy, spawnPoints[spawnPointsIndex].position, spawnPoints[spawnPointsIndex].rotation);
        }
        
    }
}
