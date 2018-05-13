using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesDetails : MonoBehaviour {

    public Text qntWavesText;
    public Text numberWaveText;
    bool entrouNaWave;
    public Text enemiesAliveText;
    public CreateWaves wave;
    int waveAnterior = 0;
    int time;
    public Animator animTextWaveComplete;
    public static int countEnemies;
    public static bool isDeadEnemy;
    LevelCompleteManager levelComplete;

    // Use this for initialization
    void Start () {
        levelComplete = GetComponent<LevelCompleteManager>();

        qntWavesText.text = "Waves: "+ (wave.numberWave + 1) + "/" + wave.waves.Length;
        animTextWaveComplete.SetBool("WaveComplete", true);
        StartCoroutine(NumberWave());
        countEnemies = wave.waves[wave.numberWave].count;
        enemiesAliveText.text = "" + wave.waves[wave.numberWave].count;
        
        
    }

    // Update is called once per frame
    void Update() {

        //Quantidade de Inimigos Vivos, e mostrando no canvas
        if (isDeadEnemy)
        {
            enemiesAliveText.text = "" + countEnemies;
        }


        //Texto para "wavesAtual/QntInteiradeWaves"
        if (wave.numberWave > waveAnterior)
        {
            countEnemies = wave.waves[wave.numberWave].count;
            qntWavesText.text = "Waves: " + (wave.numberWave + 1) + "/" + wave.waves.Length;
            waveAnterior++;
        }

        if (wave.waveComplete)
        {

            animTextWaveComplete.SetBool("WaveComplete", true);
            if (entrouNaWave)
            {
                StartCoroutine(NumberWave());
            }
            
            entrouNaWave = false;
        }
        else
        {
            entrouNaWave = true;
            animTextWaveComplete.SetBool("WaveComplete", false);
        }

        //Se a wave foi completada
        if (wave.finish)
        {
            levelComplete.WinLevel();
            qntWavesText.text = "Level Completado";
            animTextWaveComplete.SetBool("WaveComplete", true);

        }           


	}

    IEnumerator NumberWave()
    {
        numberWaveText.gameObject.SetActive(false);
        yield return new WaitForSeconds(wave.timeBetweenWaves);
        numberWaveText.text = qntWavesText.text;
        numberWaveText.gameObject.SetActive(true);
    }
    
}
