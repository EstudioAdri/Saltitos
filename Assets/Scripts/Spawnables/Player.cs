using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : ThinkingSpawnable
{
    [SerializeField] float speedMultiplier;
    [SerializeField] Rigidbody PlayerRB;
    NavMeshAgent playerAgent;
    Vector3 lastPosition;

    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
        playerAgent = GetComponent<NavMeshAgent>();
        playerAgent.enabled = true;
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 headingTo = transform.position + (transform.position - lastPosition); // Calculates where the gameObject will be in the next frame
        LookTowards(new Vector3(headingTo.x, transform.position.y, headingTo.z));
        
        if (Input.GetMouseButton(1))
        {
            MovePlayer();
        }

        lastPosition = transform.position; // must be last
    }


    void MovePlayer()
    {
        Ray moveRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit moveInfo;
        if (Physics.Raycast(moveRay, out moveInfo, Mathf.Infinity))        {
            
            playerAgent.stoppingDistance = 0f;
            playerAgent.SetDestination(moveInfo.point);
        }
    }
}