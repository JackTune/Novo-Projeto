using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteManager : MonoBehaviour {

    public WavesDetails waveDetals;
    public float restartDelay = 5f;
    public GameObject desativarTimeProxWaves;
    public GameObject desativarCountEnemies;
    public Button proxFase;

    Animator anim;
    float restartTime;
    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        proxFase.interactable = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (waveDetals.wave.finish)
        {
            StartCoroutine(Espera());
            desativarTimeProxWaves.SetActive(false);
            desativarCountEnemies.SetActive(false);
            restartTime += Time.deltaTime;
            proxFase.interactable = true;
        }
        
	}

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("LevelComplete");
    }
}
