using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshObstacle))]

public class LocatedPlaceable : Placeable
{
    private void Awake()
    {
        GetComponent<NavMeshObstacle>().carving = true;
    }
}
