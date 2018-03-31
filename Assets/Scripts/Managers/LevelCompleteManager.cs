using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteManager : MonoBehaviour {

    public WavesDetails wavesDetails;
    public Button proximaFase;
    Animator anim;
    public float restartDelay = 5f;
    public GameObject desativarTimeProxWaves;
    float restartTime;



    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (wavesDetails.wave.finish)
        {
            StartCoroutine(Espera());
            desativarTimeProxWaves.SetActive(false);
            restartTime += Time.deltaTime;
            proximaFase.interactable = true;
        }
        else
        {
            proximaFase.interactable = false;
        }



    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("LevelComplete");
    }

}
