using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour {

    //variáveis
    Color color;
    GameObject player;
    EnemyHealth enemyHealth;
    Mana mana;
    GameObject gm;

    //Fury
    [System.NonSerialized]
    public float currentForActiveFury;
    [System.NonSerialized]
    public bool isUsingFury;

    [Space(5)]
    [Header("Furia")]
    [Space(5)]
    public int qntAddBuff;
    public int qntForActiveFury;
    public int timeOfFury;
    float countRealTime;
    float consumeFury;
    bool canUseFury;
    public Image furyImage;
    public ParticleSystem particleFury;
    PlayerShooting playerShooting;

    [Space(10)]

    [Header("FreezeSkill")]
    [Space(5)]
    //Variáveis da Skill Freeze
    public ParticleSystem particleIceExplosion;
    public SphereCollider IceExplosionColider;
    public Image imageSkillFreeze;
    float auxTimeForActiveSkillFreeze;
    public float timeForActiveSkillFreeze;
    bool canUseFreeze = false;
    public int damageFreeze;
    public float costManaFreeze;

    [Space(10)]

    [Header("StormIceSkill")]
    [Space(5)]

    //Variáveis da Skill StormIce
    public ParticleSystem particleStormIce;
    public SphereCollider StormIceColider;
    public Image imageSkillStormIce;
    public float timeForActiveSkillStormIce;
    public int damageStormIce;
    float auxTimeForActiveSkillStormIce;
    bool canUseStormIce;
    public float costManaStormIce;

    [Space(10)]

    [Header("BlackHoleSkill")]
    [Space(5)]

    //Variáveis da Skill BlackHole
    public ParticleSystem particleBlackHole;
    public SphereCollider blackHoleColider;
    public Image imageSkillBlackHole;
    public float timeForActiveBlackHole;
    public int damageBlackHole;
    public Animator animBlackHole;
    float auxTimeForActiveSkillBlackHole;
    bool canUseBlackHole;
    public float costManaBlackHole;

    
    //Variáveis de pegar a posição do mouse
    Ray camRay;
    RaycastHit floorHit;
    Vector3 positionMouse;
    int floorMask;
    float canRayLenght = 100f;


    private void Awake()
    {
        //Obtém o Player e seus componentes
        player = GameObject.FindGameObjectWithTag("Player");
        mana = player.GetComponent<Mana>();
        playerShooting = player.GetComponentInChildren<PlayerShooting>();
       
    }


    // Use this for initialization
    void Start () {
        //Utilitários da Fury
        consumeFury = timeOfFury;
        currentForActiveFury = 0;

        //Uma layer, na qual serve para identificar a posição do mouse
        floorMask = LayerMask.GetMask("Floor");
        //Autorizado a usar as skills
        canUseFreeze = true;
        canUseStormIce = true;
        canUseBlackHole = true;
        //Desabilitar os colliders das skills
        IceExplosionColider.enabled = false;
        StormIceColider.enabled = false;
        blackHoleColider.enabled = false;
        auxTimeForActiveSkillFreeze = 0;
        auxTimeForActiveSkillStormIce = 0;
        auxTimeForActiveSkillBlackHole = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        SetImagesSkills();
        UseSkills();
        UseFury();

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
    IEnumerator DesativeCollidersFreeze()
    {
        yield return new WaitForSeconds(0.1f);
        IceExplosionColider.enabled = false;
        particleIceExplosion.Stop();
    }

    IEnumerator DesativeColiderStormIce()
    {
        yield return new WaitForSeconds(3.1f);
        StormIceColider.enabled = false;
        particleStormIce.Stop();
       
    }

    IEnumerator DesativeColiderBlackHole()
    {
        yield return new WaitForSeconds(5f);
        animBlackHole.SetBool("ActiveBlackHole", false);
        blackHoleColider.enabled = false;
        particleBlackHole.Stop();
    }


    /*Métodos da Mana*/

    //Quando o player não tiver mana suficiente para usar a skill
    void AlertMana(Image image, float alfa)
    {
        color = image.color;
        color.a = alfa;
        image.color = color;
    }

    //Seta as imagens dependendo de qual skill o player não puder usar
    void SetImagesSkills()
    {
        if (mana.currentMana < costManaFreeze)
        {
            AlertMana(imageSkillFreeze, 0);
        }
        else
        {
            AlertMana(imageSkillFreeze, 255);
        }

        if (mana.currentMana < costManaStormIce)
        {
            AlertMana(imageSkillStormIce, 0);
        }
        else
        {
            AlertMana(imageSkillStormIce, 255);
        }

        if (mana.currentMana < costManaBlackHole)
        {
            AlertMana(imageSkillBlackHole, 0);
        }
        else
        {
            AlertMana(imageSkillBlackHole, 255);
        }
    }

    /*Método das Skills*/
    void UseSkills()
    {
        // condição de poder usar o congelar e se usar as coisas que acontecem
        if (Input.GetKeyDown(KeyCode.Space) && canUseFreeze == true && mana.currentMana >= costManaFreeze)
        {

            auxTimeForActiveSkillFreeze = 0;
            imageSkillFreeze.fillAmount = 0;
            mana.GastarMana(costManaFreeze);
            canUseFreeze = false;
            particleIceExplosion.Play();
            IceExplosionColider.enabled = true;
            StartCoroutine(DesativeCollidersFreeze());
            StartCoroutine(StartSkillFreeze());

        }
        else if (canUseFreeze == false) // Carregaremnto da skill
        {
            auxTimeForActiveSkillFreeze += Time.deltaTime;
            imageSkillFreeze.fillAmount = auxTimeForActiveSkillFreeze / timeForActiveSkillFreeze;

        }

        // condição de poder usar a tempestade e se usar as coisas que acontecem
        if (Input.GetKeyDown(KeyCode.E) && canUseStormIce == true && mana.currentMana >= costManaStormIce)
        {
            auxTimeForActiveSkillStormIce = 0;
            imageSkillStormIce.fillAmount = 0;
            mana.GastarMana(costManaStormIce);
            canUseStormIce = false;

            camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Captura a posição do mouse
            if (Physics.Raycast(camRay, out floorHit, canRayLenght, floorMask))
            {
                positionMouse = floorHit.point;
            }
            positionMouse.y = 10f;
            //Seta a posição da particula, na posição do mouse
            particleStormIce.transform.position = positionMouse;

            //Inicia a particula e seu collider
            particleStormIce.Play();
            StormIceColider.enabled = true;

            StartCoroutine(StartSkillStormIce());
            StartCoroutine(DesativeColiderStormIce());
        }
        else if (canUseStormIce == false) // Carregaremnto da skill
        {
            auxTimeForActiveSkillStormIce += Time.deltaTime;
            imageSkillStormIce.fillAmount = auxTimeForActiveSkillStormIce / timeForActiveSkillStormIce;

        }

        // condição de poder usar o buraco negro e se usar as coisas que acontecem
        if (Input.GetKeyDown(KeyCode.R) && canUseBlackHole == true && mana.currentMana >= costManaBlackHole)
        {
            auxTimeForActiveSkillBlackHole = 0;
            imageSkillBlackHole.fillAmount = 0;
            mana.GastarMana(costManaBlackHole);
            animBlackHole.SetBool("ActiveBlackHole", true);
            canUseBlackHole = false;

            camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Captura a posição do mouse
            if (Physics.Raycast(camRay, out floorHit, canRayLenght, floorMask))
            {
                positionMouse = floorHit.point;
            }
            positionMouse.y = 1f;
            //Seta a posição da particula, na posição do mouse
            particleBlackHole.transform.position = positionMouse;

            //Inicia a particula e seu collider
            particleBlackHole.Play();
            blackHoleColider.enabled = true;



            StartCoroutine(StartSkillBlackHole());
            StartCoroutine(DesativeColiderBlackHole());
        }
        else if (canUseBlackHole == false) // Carregaremnto da skill
        {

            auxTimeForActiveSkillBlackHole += Time.deltaTime;
            imageSkillBlackHole.fillAmount = auxTimeForActiveSkillBlackHole / timeForActiveBlackHole;

        }
    }

    /* Metódos da Fury*/

    //Adicionar Fúria
    public void AddFury()
    {
        furyImage.fillAmount = currentForActiveFury / qntForActiveFury;
    }

    //Mostra no canvas a perda de fúria
    void LoseFury()
    {
        consumeFury--;
        furyImage.fillAmount = consumeFury / timeOfFury;

    }

    //Variáveis auxiliares
    float auxDamagePlayer, auxTimeBetweenBullets, auxBuffForFreeze, auxBuffForStormIce, auxBuffForBlackHole, auxMana;

    //Ativando a fúria, e adicionando os buffs
    public void ActiveFury(float addBuff)
    {
       

        addBuff /= 100;

        particleFury.Play();

        auxDamagePlayer = playerShooting.damagePerShot;
        auxTimeBetweenBullets = playerShooting.TimeBetweenBullets;
        auxBuffForFreeze = timeForActiveSkillFreeze;
        auxBuffForStormIce = timeForActiveSkillStormIce;
        auxBuffForBlackHole = timeForActiveBlackHole;
        auxMana = mana.manaGainForSecond;

        playerShooting.damagePerShot += playerShooting.damagePerShot * addBuff;
        playerShooting.TimeBetweenBullets -= playerShooting.TimeBetweenBullets * addBuff;
        timeForActiveSkillFreeze -= timeForActiveSkillFreeze * addBuff;
        timeForActiveSkillStormIce -= timeForActiveSkillStormIce * addBuff;
        timeForActiveBlackHole -= timeForActiveBlackHole * addBuff;
        mana.manaGainForSecond *= 2;

    }

    //Desativando a fúria
    void DesativeFury()
    {

        particleFury.Stop();

        playerShooting.damagePerShot = auxDamagePlayer;
        playerShooting.TimeBetweenBullets = auxTimeBetweenBullets;
        timeForActiveSkillFreeze = auxBuffForFreeze;
        timeForActiveSkillStormIce = auxBuffForStormIce;
        timeForActiveBlackHole = auxBuffForBlackHole;
        mana.manaGainForSecond = auxMana;
        
    }

    //Usar a fúria
    void UseFury()
    {
        //Verifica se a barra de fúria está cheia
        if (currentForActiveFury >= qntForActiveFury)
        {
            canUseFury = true;
        }

        //Verifica se o foi clicado e se pode usar
        if (Input.GetKeyDown(KeyCode.Q) && canUseFury)
        {
            ActiveFury(qntAddBuff);
            isUsingFury = true;
            canUseFury = false;
            countRealTime = 1;
            currentForActiveFury = 0;
            
            consumeFury--;
            furyImage.fillAmount = consumeFury / timeOfFury;
            
        }
        else if(canUseFury == false && isUsingFury && countRealTime >=1)
        {
            countRealTime = 0;
            LoseFury();
            if(consumeFury == 0) {
                isUsingFury = false;
                DesativeFury();
            }
        }
        else
        {
            countRealTime += Time.deltaTime;
        }


    }

}
