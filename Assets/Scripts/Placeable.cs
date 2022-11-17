using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Placeable : MonoBehaviour
{
    public PlaceableType pType;

    [HideInInspector] public Faction faction;
    [HideInInspector] public PlaceableTarget targetType; //TODO: move to ThinkingPlaceable?
    [HideInInspector] public AudioClip dieAudioClip;

    public UnityAction<Placeable> OnDie;
    public enum PlaceableType
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
