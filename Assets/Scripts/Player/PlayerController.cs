using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Rigidbody
    Rigidbody rb;

    //Movement
    Vector3 movement;
    float horizontal;
    float vertical;

    //Speeds
    public float speed;

    //RayCast Point(Camera)
    int floorMask;
    float canRayLenght = 100f;

    
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Move(horizontal, vertical);

        Turning();
       
    }


    //Movimentar o personagem
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }


    //Rotacionar a frente do player para a posição do mouse.
    void Turning()
    {

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, canRayLenght, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            
            rb.MoveRotation(newRotation);
        }
        

    }
}
