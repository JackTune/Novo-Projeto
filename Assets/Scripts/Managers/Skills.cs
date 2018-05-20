using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour {

    [System.Serializable]
    public class ClassSkills
    {
        public string nome;
        public ParticleSystem particleSkill;
        public SphereCollider colliderSkill;
        public Image imageSkill;
        public Text timeForActiveSkillText;
        public float timeForActiveSkill;
        public int damageSkill;
        [System.NonSerialized]
        public float auxTimeForActiveSkill;
        [System.NonSerialized]
        public bool canUseSkill;
        public float costManaSkill;
    }

    public ClassSkills skillFreeze = new ClassSkills();
    public ClassSkills skillStormIce = new ClassSkills();
    public ClassSkills skillBlackHole = new ClassSkills();
    public Animator animBlackHole;
    public ClassSkills skillLaserIce = new ClassSkills();
    public BoxCollider colliderLaserIce;
    float timeLostManaLaserIce;
    [System.NonSerialized]
    public bool ativouSkillLaserIce = false;
    float timer;

    //variáveis
    Color color;
    GameObject player;
    PlayerShooting playerShooting;
    PlayerController playerMovement;
    float auxSpeed;
    EnemyHealth enemyHealth;
    Mana mana;
    GameObject gm;
    PotionsScript playerPotions;

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
    
    
    //Variáveis de pegar a posição do mouse
    Ray camRay;
    RaycastHit floorHit;
    Vector3 positionMouse;
    int floorMask;
    float canRayLenght = 100f;


    private void Awake()
    {
        playerPotions = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PotionsScript>();

        //Obtém o Player e seus componentes
        player = GameObject.FindGameObjectWithTag("Player");
        mana = player.GetComponent<Mana>();
        playerShooting = player.GetComponentInChildren<PlayerShooting>();
        playerMovement = player.GetComponent<PlayerController>();
        
    }



    // Use this for initialization
    void Start () {
        auxSpeed = playerMovement.speed;

        //Utilitários da Fury
        consumeFury = timeOfFury;
        currentForActiveFury = 0;

        //Uma layer, na qual serve para identificar a posição do mouse
        floorMask = LayerMask.GetMask("Floor");
        //Autorizado a usar as skills
        skillFreeze.canUseSkill = true;
        skillStormIce.canUseSkill = true;
        skillBlackHole.canUseSkill = true;
        skillLaserIce.canUseSkill = true;
        //Desabilitar os colliders das skills
        skillFreeze.colliderSkill.enabled = false;
        skillStormIce.colliderSkill.enabled = false;
        skillBlackHole.colliderSkill.enabled = false;
        colliderLaserIce.enabled = false;

        skillFreeze.auxTimeForActiveSkill = skillFreeze.timeForActiveSkill;
        DesativeOrActiveTimeSkills(skillFreeze, false);
        skillStormIce.auxTimeForActiveSkill = skillStormIce.timeForActiveSkill;
        DesativeOrActiveTimeSkills(skillStormIce, false);
        skillBlackHole.auxTimeForActiveSkill = skillBlackHole.timeForActiveSkill;
        DesativeOrActiveTimeSkills(skillBlackHole, false);
        skillLaserIce.auxTimeForActiveSkill = skillLaserIce.timeForActiveSkill;
        DesativeOrActiveTimeSkills(skillLaserIce, false);
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
        
        yield return new WaitForSeconds(skillFreeze.timeForActiveSkill);
        skillFreeze.canUseSkill = true;
        skillFreeze.auxTimeForActiveSkill = skillFreeze.timeForActiveSkill;
        DesativeOrActiveTimeSkills(skillFreeze, false);
    }

    IEnumerator StartSkillStormIce()
    {
        yield return new WaitForSeconds(skillStormIce.timeForActiveSkill);
        skillStormIce.canUseSkill = true;
        skillStormIce.auxTimeForActiveSkill = skillStormIce.timeForActiveSkill;
        DesativeOrActiveTimeSkills(skillStormIce, false);
    }

    IEnumerator StartSkillBlackHole()
    {
        yield return new WaitForSeconds(skillBlackHole.timeForActiveSkill);
        skillBlackHole.canUseSkill = true;
        skillBlackHole.auxTimeForActiveSkill = skillBlackHole.timeForActiveSkill;
        DesativeOrActiveTimeSkills(skillBlackHole, false);
    }

    IEnumerator StartSkillLaserIce()
    {
        yield return new WaitForSeconds(skillLaserIce.timeForActiveSkill);
        print("Entrou na coroutine");
        skillLaserIce.canUseSkill = true;
        ativouSkillLaserIce = false;
        skillLaserIce.auxTimeForActiveSkill = skillLaserIce.timeForActiveSkill;
        DesativeOrActiveTimeSkills(skillLaserIce, false);

    }

    // Tempo para desativar os coliders
    IEnumerator DesativeCollidersFreeze()
    {
        yield return new WaitForSeconds(0.1f);
        skillFreeze.colliderSkill.enabled = false;
        skillFreeze.particleSkill.Stop();
    }

    IEnumerator DesativeColiderStormIce()
    {
        yield return new WaitForSeconds(3.1f);
        skillStormIce.colliderSkill.enabled = false;
        skillStormIce.particleSkill.Stop();
       
    }

    IEnumerator DesativeColiderBlackHole()
    {
        yield return new WaitForSeconds(5f);
        animBlackHole.SetBool("ActiveBlackHole", false);
        skillBlackHole.colliderSkill.enabled = false;
        skillBlackHole.particleSkill.Stop();
    }

    void DesativeOrActiveTimeSkills(ClassSkills skill, bool setGameObject)
    {
        skill.timeForActiveSkillText.gameObject.SetActive(setGameObject);
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
        if (mana.currentMana < skillFreeze.costManaSkill)
        {
            AlertMana(skillFreeze.imageSkill, 0);
        }
        else
        {
            AlertMana(skillFreeze.imageSkill, 255);
        }

        if (mana.currentMana < skillStormIce.costManaSkill)
        {
            AlertMana(skillStormIce.imageSkill, 0);
        }
        else
        {
            AlertMana(skillStormIce.imageSkill, 255);
        }

        if (mana.currentMana < skillBlackHole.costManaSkill)
        {
            AlertMana(skillBlackHole.imageSkill, 0);
        }
        else
        {
            AlertMana(skillBlackHole.imageSkill, 255);
        }

        if(mana.currentMana < skillLaserIce.costManaSkill)
        {
            AlertMana(skillLaserIce.imageSkill, 0);
        }
        else
        {
            AlertMana(skillLaserIce.imageSkill, 255);
        }
    }

    /*Método das Skills*/
    void UseSkills()
    {


        // condição de poder usar o congelar e se usar as coisas que acontecem
        if (Input.GetKeyDown(KeyCode.Space) && skillFreeze.canUseSkill == true && mana.currentMana >= skillFreeze.costManaSkill)
        {

            mana.GastarMana(skillFreeze.costManaSkill);
            skillFreeze.canUseSkill = false;
            skillFreeze.particleSkill.Play();
            skillFreeze.colliderSkill.enabled = true;
            StartCoroutine(DesativeCollidersFreeze());
            StartCoroutine(StartSkillFreeze());

        }
        else if (skillFreeze.canUseSkill == false) // Carregaremnto da skill
        {
            if (ativouSkillLaserIce == false)
            {
                skillFreeze.auxTimeForActiveSkill -= Time.deltaTime;
                skillFreeze.timeForActiveSkillText.text = ((int)skillFreeze.auxTimeForActiveSkill).ToString();
                DesativeOrActiveTimeSkills(skillFreeze, true);
            }
            // skillFreeze.imageSkill.fillAmount = skillFreeze.auxTimeForActiveSkill / skillFreeze.timeForActiveSkill;

        }

        // condição de poder usar a tempestade e se usar as coisas que acontecem
        if (Input.GetKeyDown(KeyCode.E) && skillStormIce.canUseSkill == true && mana.currentMana >= skillStormIce.costManaSkill)
        {
            mana.GastarMana(skillStormIce.costManaSkill);
            skillStormIce.canUseSkill = false;

            camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Captura a posição do mouse
            if (Physics.Raycast(camRay, out floorHit, canRayLenght, floorMask))
            {
                positionMouse = floorHit.point;
            }
            positionMouse.y = 10f;
            //Seta a posição da particula, na posição do mouse
            skillStormIce.particleSkill.transform.position = positionMouse;

            //Inicia a particula e seu collider
            skillStormIce.particleSkill.Play();
            skillStormIce.colliderSkill.enabled = true;

            StartCoroutine(StartSkillStormIce());
            StartCoroutine(DesativeColiderStormIce());
        }
        else if (skillStormIce.canUseSkill == false) // Carregaremnto da skill
        {
            if (ativouSkillLaserIce == false)
            {
                skillStormIce.auxTimeForActiveSkill -= Time.deltaTime;
                skillStormIce.timeForActiveSkillText.text = ((int)skillStormIce.auxTimeForActiveSkill).ToString();
                DesativeOrActiveTimeSkills(skillStormIce, true);
            }
        }

        // condição de poder usar o buraco negro e se usar as coisas que acontecem
        if (Input.GetKeyDown(KeyCode.R) && skillBlackHole.canUseSkill == true && mana.currentMana >= skillBlackHole.costManaSkill)
        {
           
            mana.GastarMana(skillBlackHole.costManaSkill);
            animBlackHole.SetBool("ActiveBlackHole", true);
            skillBlackHole.canUseSkill = false;

            camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Captura a posição do mouse
            if (Physics.Raycast(camRay, out floorHit, canRayLenght, floorMask))
            {
                positionMouse = floorHit.point;
            }
            positionMouse.y = 1f;
            //Seta a posição da particula, na posição do mouse
            skillBlackHole.particleSkill.transform.position = positionMouse;

            //Inicia a particula e seu collider
            skillBlackHole.particleSkill.Play();
            skillBlackHole.colliderSkill.enabled = true;



            StartCoroutine(StartSkillBlackHole());
            StartCoroutine(DesativeColiderBlackHole());
        }
        else if (skillBlackHole.canUseSkill == false) // Carregaremnto da skill
        {
            if (ativouSkillLaserIce == false)
            {
                skillBlackHole.auxTimeForActiveSkill -= Time.deltaTime;
                skillBlackHole.timeForActiveSkillText.text = ((int)skillBlackHole.auxTimeForActiveSkill).ToString();
                DesativeOrActiveTimeSkills(skillBlackHole, true);
            }
        }


        timeLostManaLaserIce += Time.deltaTime;
        if (Input.GetKey(KeyCode.F) && skillLaserIce.canUseSkill == true && mana.currentMana >= skillLaserIce.costManaSkill)
        {
            
            playerMovement.speed = 0;
            skillFreeze.canUseSkill = false;
            skillBlackHole.canUseSkill = false;
            skillStormIce.canUseSkill = false;
            playerShooting.canFire = false;

            if(timeLostManaLaserIce >= 1f)
            {
                mana.GastarMana(skillLaserIce.costManaSkill);
                timeLostManaLaserIce = 0;
            }
            //
           // skillLaserIce.canUseSkill = false;
           if(ativouSkillLaserIce == false)
            {
                skillLaserIce.particleSkill.Play();
                ativouSkillLaserIce = true;
            }
            colliderLaserIce.enabled = true;

        }
        else if(ativouSkillLaserIce)
        {
            playerMovement.speed = auxSpeed;

            skillFreeze.canUseSkill = true;
            skillBlackHole.canUseSkill = true;
            skillStormIce.canUseSkill = true;
            playerShooting.canFire = true;
            skillLaserIce.canUseSkill = false;

            colliderLaserIce.enabled = false;
            skillLaserIce.particleSkill.Stop();
            timer += Time.deltaTime;
            skillLaserIce.auxTimeForActiveSkill -= Time.deltaTime;
            skillLaserIce.timeForActiveSkillText.text = ((int)skillLaserIce.auxTimeForActiveSkill).ToString();
            DesativeOrActiveTimeSkills(skillLaserIce, true);
            ContarLaserIce();
        }
    }

    void ContarLaserIce()
    {
        if (timer >= skillLaserIce.timeForActiveSkill)
        {
            skillLaserIce.canUseSkill = true;
            ativouSkillLaserIce = false;
            skillLaserIce.auxTimeForActiveSkill = skillLaserIce.timeForActiveSkill;
            DesativeOrActiveTimeSkills(skillLaserIce, false);
            timer = 0;
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
        auxBuffForFreeze = skillFreeze.timeForActiveSkill;
        auxBuffForStormIce = skillStormIce.timeForActiveSkill;
        auxBuffForBlackHole = skillBlackHole.timeForActiveSkill;
        auxMana = mana.manaGainForSecond;

        playerShooting.damagePerShot += playerShooting.damagePerShot * addBuff;
        playerShooting.TimeBetweenBullets -= playerShooting.TimeBetweenBullets * addBuff;
        skillFreeze.timeForActiveSkill -= skillFreeze.timeForActiveSkill * addBuff;
        skillStormIce.timeForActiveSkill -= skillStormIce.timeForActiveSkill * addBuff;
        skillBlackHole.timeForActiveSkill -= skillBlackHole.timeForActiveSkill * addBuff;
        mana.manaGainForSecond *= 2;

    }

    //Desativando a fúria
    void DesativeFury()
    {

        particleFury.Stop();

        playerShooting.damagePerShot = auxDamagePlayer;
        playerShooting.TimeBetweenBullets = auxTimeBetweenBullets;
        skillFreeze.timeForActiveSkill = auxBuffForFreeze;
        skillStormIce.timeForActiveSkill = auxBuffForStormIce;
        skillBlackHole.timeForActiveSkill = auxBuffForBlackHole;
        mana.manaGainForSecond = auxMana;
        consumeFury = timeOfFury;
    }

    //Usar a fúria
    void UseFury()
    {
        if (playerPotions.playerCanUsePower)
        {
            AddFury();
            playerPotions.playerCanUsePower = false;
        }

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
