using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesDetails : MonoBehaviour {

    public Text qntWaves;
    public Text timeProxWave;
    public Text enemiesAlive;
    public CreateWaves wave;
    int waveAnterior = 0;
    int time;
    public Animator animTextWaveComplete;
    public static bool waveComplete = true;
    public static int countEnemies;
    public static bool isDeadEnemy;


    // Use this for initialization
    void Start () {
        
        qntWaves.text = "Waves: "+ (wave.numberWave + 1) + "/" + wave.waves.Length;
        timeProxWave.text ="Prox Wave: " + wave.timeBetweenWaves;
        animTextWaveComplete.SetBool("WaveComplete", true);
        countEnemies = wave.waves[wave.numberWave].count;
        enemiesAlive.text = "Inimigos Vivos: " + wave.waves[wave.numberWave].count + "/" + wave.waves[wave.numberWave].count;
        
    }
	
	// Update is called once per frame
	void Update () {

        if (isDeadEnemy)
        {
            enemiesAlive.text = "Inimigos Vivos: " + countEnemies + "/" + wave.waves[wave.numberWave].count;
        }


        //Texto para "wavesAtual/QntInteiradeWaves"
        if (wave.numberWave > waveAnterior)
        {
            countEnemies = wave.waves[wave.numberWave].count;
            qntWaves.text = "Waves: " + (wave.numberWave + 1) + "/" + wave.waves.Length;
            waveAnterior++;
        }

        if (waveComplete)
        {
            animTextWaveComplete.SetBool("WaveComplete", true);
           
        }
        else
        {
            animTextWaveComplete.SetBool("WaveComplete", false);
        }

        if (wave.finish)
        {
            qntWaves.text = "Level Completado";
            animTextWaveComplete.SetBool("WaveComplete", true);
        }



        //Texto do Tempo para Próxima Wave
        if (wave.countTempo >= 0 && wave.finish != true)
        {
            timeProxWave.text = "Prox Wave: " + wave.countTempo;
        }
        else if (wave.countTempo < 0)
        {
            timeProxWave.text = "Prox Wave: ---";
        }


	}
}
