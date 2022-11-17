using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableData : ScriptableObject
{
    [Header("Common")]
    public Placeable.PlaceableType pType;
    public GameObject associatedPrefab;
    public GameObject alternatePrefab;

    [Header("Enemy actions and stats")]
    public float speed;
    

    
    
}
