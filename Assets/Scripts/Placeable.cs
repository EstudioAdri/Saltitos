using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{    public enum PlaceableType
    {
        Player,
        Enemy,
        Item,
    }

    public enum PlaceableTarget
    {
        Player,
        Enemy,
        Both,
    }

    public enum Faction
    {
        Player,
        Enemy,
        None,
    }
}
