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
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();


            //Caso Exista um enemyHealth naquele objeto
            if (enemyHealth != null)
            {
                print("Tiro Pegou");
                enemyHealth.TakeDamage(skills.damageStormIce);
            }
        }
    }
}
