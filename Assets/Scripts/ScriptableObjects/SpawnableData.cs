using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableData : ScriptableObject
{
    [Header("Common")]
    public Spawnable.SpawnableType sType;
    public GameObject associatedPrefab;
    public GameObject alternatePrefab;

    [Header("Enemy actions and stats")]
    public float speed;

    [Header("Enemies")]
    public ThinkingSpawnable.AttackType attackType = ThinkingSpawnable.AttackType.Melee;
    public Spawnable.SpawnableTarget targetType = Spawnable.SpawnableTarget.Both;
    public float attackRatio = 1f; //time between attacks
    public float damagePerAttack = 2f; //damage each attack deals
    public float attackRange = 1f;
    public float hitPoints = 10f; //when units or buildings suffer damage, they lose hitpoints
    public AudioClip attackClip, dieClip;


}
