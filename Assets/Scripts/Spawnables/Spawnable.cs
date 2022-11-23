using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawnable : MonoBehaviour
{
    public SpawnableType spawnableType;

    [HideInInspector] public Faction faction;
    [HideInInspector] public SpawnableType targetType;
    [HideInInspector] public AudioClip dieAudioClip;

    public UnityAction<Spawnable> OnDie;
    public enum SpawnableType
    {
        Player,
        Building,
        Castle,
        Entity
    }

    public enum Faction
    {
        Player,
        Enemy,
        None,
    }

    public enum EnemyType
    {
        NotEnemy,
        Alien,
        SeatIbiza
    }
}
