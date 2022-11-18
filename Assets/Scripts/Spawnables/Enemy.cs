using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : ThinkingSpawnable

{
    private float speed;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        sType = Spawnable.SpawnableType.Enemy;

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>(); //will be disabled until Activate is called
        audioSource = GetComponent<AudioSource>();
    }

    public void Activate(Faction sFaction, SpawnableData sData)
    {
        faction = sFaction;
        hitPoints = sData.hitPoints;
        targetType = sData.targetType;
        attackRange = sData.attackRange;
        attackRatio = sData.attackRatio;
        speed = sData.speed;
        damage = sData.damagePerAttack;
        attackAudioClip = sData.attackClip;
        dieAudioClip = sData.dieClip;
        //TODO: add more as necessary

        navMeshAgent.speed = speed;
        animator.SetFloat("MoveSpeed", speed); //will act as multiplier to the speed of the run animation clip

        state = States.Idle;
        navMeshAgent.enabled = true;
    }

    public override void SetTarget(ThinkingSpawnable t)
    {
        base.SetTarget(t);
    }

    public override void Seek()
    {
        if (target == null)
            return;

        base.Seek();

        navMeshAgent.SetDestination(target.transform.position);
        navMeshAgent.isStopped = false;
        animator.SetBool("IsMoving", true);
    }

    public override void StartAttack()
    {
        base.StartAttack();

        navMeshAgent.isStopped = true;
        animator.SetBool("IsMoving", false);
    }

    public override void DealBlow()
    {
        base.DealBlow();

        animator.SetTrigger("Attack");
        transform.forward = (target.transform.position - transform.position).normalized; //turn towards the target
    }

    public override void Stop()
    {
        base.Stop();

        navMeshAgent.isStopped = true;
        animator.SetBool("IsMoving", false);
    }

    protected override void Die()
    {
        base.Die();

        navMeshAgent.enabled = false;
        animator.SetTrigger("IsDead");
    }

}
