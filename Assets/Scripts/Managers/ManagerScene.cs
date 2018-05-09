using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerScene : MonoBehaviour {

    //Código usado para troca de fases, no caso para passar de fase e dar restart

    Save salvar;

    [System.NonSerialized]
    public string gameMode;

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
    
    //Quando clicar no botão, espera meio segundo e depois vai para a cena indicada.

    private IEnumerator AbrirCena(){

		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene (nomeCena);

	}

    //Deletar todos os playerPrefs e começar um novo jogo
    public void NovoGame(string cena)
    {
        nomeCena = cena;
        StartCoroutine(AbrirCena());
        DelleteAll.DeleteAll();
    }

    //Obter qual a sena jogada no momento e verificar seu modo de jogo.
    public void GetScenesModeGame()
    {
        if (SceneManager.GetSceneByName("Survival").IsValid())
        {
            gameMode = "Survival";
        }
        else
        {
            gameMode = "Arcade";
        }
    }

}

