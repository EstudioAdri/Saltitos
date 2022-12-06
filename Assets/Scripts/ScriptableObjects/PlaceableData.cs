using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlaceable", menuName = "Saltitos/Placeable Data")]
public class PlaceableData : ScriptableObject
{
    [Header("Common")]
    public Placeable.PlaceableType placeableType;
    public GameObject associatedPrefab;
    public Transform spawnPoint;
    public string objectID;
}
