using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public enum AtackType
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

    [HideInInspector] public float timeToActNext = 0f;

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
