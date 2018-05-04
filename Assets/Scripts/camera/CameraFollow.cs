using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smooth = 5f;

    Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
	}
    // Câmera segue o player 
    void FixedUpdate()
    {
        Vector3 targetCamPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPosition, smooth * Time.deltaTime);
    }


}
