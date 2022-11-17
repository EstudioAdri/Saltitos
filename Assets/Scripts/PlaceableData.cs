using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableData : ScriptableObject
{
    [Header("Common")]
    public Placeable.PlaceableType pType;
    public GameObject associatedPrefab;
    public GameObject alternatePrefab;

    [Header("Enemy actions and stats")]
    public float speed;

    [Header("Enemies")]
    public ThinkingPlaceable.AttackType attackType = ThinkingPlaceable.AttackType.Melee;
    public Placeable.PlaceableTarget targetType = Placeable.PlaceableTarget.Both;
    public float attackRatio = 1f; //time between attacks
    public float damagePerAttack = 2f; //damage each attack deals
    public float attackRange = 1f;
    public float hitPoints = 10f; //when units or buildings suffer damage, they lose hitpoints
    public AudioClip attackClip, dieClip;


}
