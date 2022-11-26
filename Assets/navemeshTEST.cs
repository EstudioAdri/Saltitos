using System.Collections;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine;

public class navemeshTEST : MonoBehaviour
{
    Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        if (transform.position != lastPosition)
        {
            NavMeshBuilder.ClearAllNavMeshes();
            NavMeshBuilder.BuildNavMesh();
            lastPosition = transform.position;
        }
    }
}
