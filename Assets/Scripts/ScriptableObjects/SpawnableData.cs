using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpawnable", menuName = "Saltitos/Spawnable Data")]
public class SpawnableData : ScriptableObject
{
    [Header("Common")]
    public Spawnable.Faction Faction;
    public Spawnable.SpawnableType sType;
    public GameObject associatedPrefab;
    public GameObject alternatePrefab;

    [Header("Enemy actions and stats")]
    public float speed;

    [Header("Stats")]
    public ThinkingSpawnable.AttackType attackType = ThinkingSpawnable.AttackType.Melee;
    public Spawnable.SpawnableType targetType = Spawnable.SpawnableType.Entity;
    public float attackRatio = 1f; //time between attacks
    public float damagePerAttack = 2f; //damage each attack deals
    public float attackRange = 1f;
    public float hitPoints = 10f; //when units or buildings suffer damage, they lose hitpoints
    public AudioClip attackClip, dieClip;
}
