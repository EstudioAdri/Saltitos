using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : LocatedPlaceable
{
    [SerializeField] ObstacleType obstacleType;

    public enum ObstacleType
    {
        PalmTree,
        PineTre,
        Rock1,
        Rock2,
        Rock3
    }
}
