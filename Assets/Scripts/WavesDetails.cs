using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesDetails : MonoBehaviour {

    public Text qntWaves;
    public Text timeProxWave;
    public CreateWaves wave;
    int waveAnterior = 0;
    int time;
    public Animator animTextWaveComplete;
    public static bool waveComplete = true;


    // Use this for initialization
    void Start () {
        
        qntWaves.text = "Waves: "+ (wave.numberWave + 1) + "/" + wave.waves.Length;
        timeProxWave.text ="Prox Wave: " + wave.timeBetweenWaves;
        animTextWaveComplete.SetBool("WaveComplete", true);
        
        
    }
	
	// Update is called once per frame
	void Update () {
       
        //Texto para "wavesAtual/QntInteiradeWaves"
		if(wave.numberWave > waveAnterior)
        {
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
