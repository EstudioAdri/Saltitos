using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawnable : MonoBehaviour
{
    public SpawnableType sType;

    [HideInInspector] public Faction faction;
    [HideInInspector] public SpawnableTarget targetType; //TODO: move to ThinkingPlaceable?
    [HideInInspector] public AudioClip dieAudioClip;

    public UnityAction<Spawnable> OnDie;
    public enum SpawnableType
    {
        Player,
        Enemy,
        Item,
    }

    public enum SpawnableTarget
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
