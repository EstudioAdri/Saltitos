using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThinkingPlaceable : Placeable
{
    [HideInInspector]public States state;
    public enum States
    {
        Idle,
        Seeking,
        Attacking,
        Dead
    }    
    public enum AttackType
    {
        Melee,
        Ranged
    }

    [HideInInspector] public ThinkingPlaceable target;

    [HideInInspector] public float hitPoints;
    [HideInInspector] public float attackRange;
    [HideInInspector] public float attackRatio;
    [HideInInspector] public float lastBlowTime = -1000f;
    [HideInInspector] public float damage;
    [HideInInspector] public AudioClip attackAudioClip;
    protected AudioSource audioSource;

    [HideInInspector] public float timeToActNext = 0f;

    [Header("Projectile for Ranged")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    public UnityAction<ThinkingPlaceable> OnDealDamage, OnProjectileFired;

    public virtual void SetTarget(ThinkingPlaceable t)
    {
        target = t;
        t.OnDie += TargetIsDead;
    }

    public virtual void StartAttack()
    {
        state = States.Attacking;
    }

    public virtual void DealBlow()
    {
        lastBlowTime = Time.time;
    }
    public virtual void Seek()
    {
        state = States.Seeking;
    }
    protected void TargetIsDead(Placeable p)
    {
        //Debug.Log("My target " + p.name + " is dead", gameObject);
        state = States.Idle;

        target.OnDie -= TargetIsDead;

        timeToActNext = lastBlowTime + attackRatio;
    }
    public bool IsTargetInRange()
    {
        return (transform.position - target.transform.position).sqrMagnitude <= attackRange * attackRange;
    }
    public virtual void Stop()
    {
        state = States.Idle;
    }
    protected virtual void Die()
    {
        state = States.Dead;

        if (OnDie != null)
            OnDie(this);

    }

    public float SufferDamage(float amount)
    {
        hitPoints -= amount;       
        if (state != States.Dead
            && hitPoints <= 0f)
        {
            Die();
        }

        return hitPoints;
    }
}
