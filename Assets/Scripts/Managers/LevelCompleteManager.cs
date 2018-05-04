using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteManager : MonoBehaviour {

    public WavesDetails waveDetals;
    public float restartDelay = 5f;
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
            restartTime += Time.deltaTime;
            proxFase.interactable = true;
        }
        
	}

    // Quando matar todos os inimigos espera 5 segundos e inicia a animação de fase completa
    IEnumerator Espera()
    {
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("LevelComplete");
    }
}
