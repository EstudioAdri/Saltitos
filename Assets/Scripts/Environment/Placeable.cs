using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public PlaceableType placeableType;

    public enum PlaceableType
    {
        Spawn,
        Obstacle
    }
}
