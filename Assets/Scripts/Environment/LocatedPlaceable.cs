using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocatedPlaceable : Placeable
{
    [Header("Prefab")]
    public GameObject placeablePrefab;

    public void Activate(PlaceableData placeableDataRef)
    {
        placeablePrefab = placeableDataRef.associatedPrefab;
        placeableType = placeableDataRef.PlaceableType;
    }

}
