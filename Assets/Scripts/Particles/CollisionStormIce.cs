using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionStormIce : MonoBehaviour {

    GameObject GM;
    Skills skills;

    // Use this for initialization
    void Start () {
        GM = GameObject.FindGameObjectWithTag("GameController");
        skills = GM.GetComponent<Skills>();
    }
	

    private void OnParticleCollision(GameObject other)
    {
        //Verifica se colidiu com o inimigo
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();


            //Caso Exista um enemyHealth naquele objeto
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(skills.damageStormIce);
            }
        }
    }
}
