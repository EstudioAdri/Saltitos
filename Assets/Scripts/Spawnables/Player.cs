using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ThinkingSpawnable
{
    [SerializeField] float speedMultiplier;
    [SerializeField] Rigidbody PlayerRB;
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            int targetRotation = 0;
            if (Input.GetKey(KeyCode.A))
            {
                PlayerRB.AddForce(new Vector3(-1, 0, 1) * speedMultiplier);
                targetRotation = -135;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                PlayerRB.AddForce(new Vector3(1, 0, -1) * speedMultiplier);
                targetRotation = 45;
            }
            if (Input.GetKey(KeyCode.W))
            {
                PlayerRB.AddForce(new Vector3(1, 0, 1) * speedMultiplier);
                targetRotation = -45;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                PlayerRB.AddForce(new Vector3(-1, 0, -1) * speedMultiplier);
                targetRotation = 135;
            }

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                targetRotation = 0;
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                targetRotation = -90;
            }

            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                targetRotation = 90;

            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                targetRotation = 180;
            }
             
            transform.rotation = Quaternion.Euler(new Vector3(0, targetRotation, 0));            
        }
    }
}