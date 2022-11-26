using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlaceable", menuName = "Saltitos/Placeable Data")]
public class PlaceableData : ScriptableObject
{
    [Header("Common")]
    public Placeable.PlaceableType PlaceableType;
    public GameObject associatedPrefab;
}
