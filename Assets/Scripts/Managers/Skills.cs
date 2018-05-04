using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour {

    //variáveis
    
    
    
    //float startTimeSkillFrezze;

    //Variáveis da Skill Freeze
    public ParticleSystem particleIceFreeze;
    public SphereCollider IceExplosionColider;
    public Image imageSkillFreeze;
    float auxTimeForActiveSkillFreeze;
    public float timeForActiveSkillFreeze;
    bool canUseFreeze;
    public int damageFreeze;
    
   

    //Variáveis da Skill StormIce
    public ParticleSystem particleStormIce;
    public SphereCollider StormIceColider;
    public Image imageSkillStormIce;
    public float timeForActiveSkillStormIce;
    public int damageStormIce;
    float auxTimeForActiveSkillStormIce;
    bool canUseStormIce;
    

    
    public ParticleSystem particleBlackHole;
    public SphereCollider blackHoleColider;
    public Image imageSkillBlackHole;
    public float timeForActiveBlackHole;
    public int damageBlackHole;
    public Animator animBlackHole;
    float auxTimeForActiveSkillBlackHole;
    bool canUseBlackHole;

    EnemyHealth enemyHealth;
    

    
    //Variáveis de pegar a posição do mouse
    Ray camRay;
    RaycastHit floorHit;
    Vector3 positionMouse;
    int floorMask;
    float canRayLenght = 100f;

    // Use this for initialization
    void Start () {
        floorMask = LayerMask.GetMask("Floor");
        canUseFreeze = true;
        canUseStormIce = true;
        canUseBlackHole = true;
        IceExplosionColider.enabled = false;
        StormIceColider.enabled = false;
        blackHoleColider.enabled = false;
        auxTimeForActiveSkillFreeze = 0;
        auxTimeForActiveSkillStormIce = 0;
        auxTimeForActiveSkillBlackHole = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

       

        // condição de poder usar o congelar e se usar as coisas que acontecem
        if (Input.GetKeyDown(KeyCode.Alpha1) && canUseFreeze == true)
        {

            auxTimeForActiveSkillFreeze = 0;
            imageSkillFreeze.fillAmount = 0;
            canUseFreeze = false;
            particleIceFreeze.Play();
            IceExplosionColider.enabled = true;
            StartCoroutine(DesactiveCollidersFreeze());
            StartCoroutine(StartSkillFreeze());

        }
        else if (canUseFreeze == false) // Carregaremnto da skill
        {
            auxTimeForActiveSkillFreeze += Time.deltaTime;
            print((int)auxTimeForActiveSkillFreeze);
            imageSkillFreeze.fillAmount = auxTimeForActiveSkillFreeze / timeForActiveSkillFreeze;
            
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && canUseStormIce == true)
        {
            auxTimeForActiveSkillStormIce = 0;
            imageSkillStormIce.fillAmount = 0;
            canUseStormIce = false;

            camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay, out floorHit, canRayLenght, floorMask))
            {
               positionMouse = floorHit.point;
            }
            positionMouse.y = 10f;
            particleStormIce.transform.position = positionMouse;
   

            particleStormIce.Play();
            StormIceColider.enabled = true;
                
            StartCoroutine(StartSkillStormIce());
            StartCoroutine(DesativarColiderStormIce());
        }
        else if (canUseStormIce == false) // Carregaremnto da skill
        {
            auxTimeForActiveSkillStormIce += Time.deltaTime;
            //print((int)auxTimeForActiveSkillStormIce);
            imageSkillStormIce.fillAmount = auxTimeForActiveSkillStormIce / timeForActiveSkillStormIce;

        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && canUseBlackHole == true)
        {
            auxTimeForActiveSkillBlackHole = 0;
            imageSkillBlackHole.fillAmount = 0;
            animBlackHole.SetBool("ActiveBlackHole", true);
            canUseBlackHole = false;

            camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay, out floorHit, canRayLenght, floorMask))
            {
                positionMouse = floorHit.point;
            }
            positionMouse.y = 1f;
            particleBlackHole.transform.position = positionMouse;

            particleBlackHole.Play();
            blackHoleColider.enabled = true;

            

            StartCoroutine(StartSkillBlackHole());
            StartCoroutine(DesativarColiderBlackHole());
        }
        else if (canUseBlackHole == false) // Carregaremnto da skill
        {
            
            auxTimeForActiveSkillBlackHole += Time.deltaTime;
            //print((int)auxTimeForActiveSkillStormIce);
            imageSkillBlackHole.fillAmount = auxTimeForActiveSkillBlackHole / timeForActiveBlackHole;

        }

    }

    

    // Tempo para usar as skills
    IEnumerator StartSkillFreeze()
    {
        
        yield return new WaitForSeconds(timeForActiveSkillFreeze);
        canUseFreeze = true;
    }

    IEnumerator StartSkillStormIce()
    {
        yield return new WaitForSeconds(timeForActiveSkillStormIce);
        canUseStormIce = true;
    }

    IEnumerator StartSkillBlackHole()
    {
        yield return new WaitForSeconds(timeForActiveBlackHole);
        canUseBlackHole = true;
    }

    // Tempo para desativar os coliders
    IEnumerator DesactiveCollidersFreeze()
    {
        yield return new WaitForSeconds(0.1f);
        IceExplosionColider.enabled = false;
        particleIceFreeze.Stop();
    }

    IEnumerator DesativarColiderStormIce()
    {
        yield return new WaitForSeconds(3.1f);
        StormIceColider.enabled = false;
        particleStormIce.Stop();
       
    }

    IEnumerator DesativarColiderBlackHole()
    {
        yield return new WaitForSeconds(5f);
        animBlackHole.SetBool("ActiveBlackHole", false);
        blackHoleColider.enabled = false;
        particleBlackHole.Stop();
    }

   


}
