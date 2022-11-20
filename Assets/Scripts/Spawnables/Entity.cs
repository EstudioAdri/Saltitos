using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : ThinkingSpawnable

{
    private float speed;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        spawnableType = Spawnable.SpawnableType.Entity;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>(); //will be disabled until Activate is called
        audioSource = GetComponent<AudioSource>();
    }

    public void Activate(SpawnableData spawnableDataRef)
    {
        faction = spawnableDataRef.Faction;
        hitPoints = spawnableDataRef.hitPoints;
        targetType = spawnableDataRef.targetType;
        attackRange = spawnableDataRef.attackRange;
        attackRatio = spawnableDataRef.attackRatio;
        speed = spawnableDataRef.speed;
        damage = spawnableDataRef.damagePerAttack;
        attackAudioClip = spawnableDataRef.attackClip;
        dieAudioClip = spawnableDataRef.dieClip;

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
