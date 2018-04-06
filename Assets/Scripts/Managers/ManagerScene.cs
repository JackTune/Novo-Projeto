using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerScene : MonoBehaviour {

    Save salvar;

	string nomeCena;

    private void Awake()
    {
        salvar = GetComponent<Save>();
    }
    public void ChangeScene (string cena){
		nomeCena = cena;
        salvar.Saves();
		StartCoroutine (AbrirCena());
	}
    
    private IEnumerator AbrirCena(){

		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene (nomeCena);

	}


}

