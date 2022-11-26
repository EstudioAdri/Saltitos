using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Placeable;

public class Spawn : LocatedPlaceable
{
    public void Awake()
    {
        placeableType = PlaceableType.Spawn;
    }
    public void Activate(PlaceableData placeableDataRef)
    {
        placeableType = placeableDataRef.placeableType;
    }

    private void Start()
    {
        transform.parent = GameObject.Find("Spawn Points").transform;
    }
}
