using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Turret : ThinkingSpawnable
{
    private Animator animator;

    private void Awake()
    {
        sType = Spawnable.SpawnableType.Turret;

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Activate(Faction sFaction, SpawnableData sData)
    {
        faction = sFaction;
        hitPoints = sData.hitPoints;
        targetType = sData.targetType;
        attackRange = sData.attackRange;
        attackRatio = sData.attackRatio;
        damage = sData.damagePerAttack;
        attackAudioClip = sData.attackClip;
        dieAudioClip = sData.dieClip;

        state = States.Idle;        
    }
   

    public override void DealBlow()
    {
        base.DealBlow();
        animator.SetTrigger("Attack");
    }    

    protected override void Die()
    {
        base.Die();
        animator.SetTrigger("IsDead");
    }
}
