using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Entity : ThinkingSpawnable

{
    private float speed;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private GameManager gameManager;
    Vector3 lastPosition;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        // animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>(); //will be disabled until Activate is called
        audioSource = GetComponent<AudioSource>();
        spawnableType = Spawnable.SpawnableType.Entity;
        navMeshAgent.updateRotation = false;
    }

    private void Start()
    {
        lastPosition = transform.position;
        navMeshAgent.stoppingDistance = attackRange;
    }
     
    private void Update()
    {
        ThinkingSpawnable targetToPass;

        Vector3 headingTo = transform.position + (transform.position - lastPosition); // Calculates where the gameObject will be in the next frame
        LookTowards(new Vector3(headingTo.x, transform.position.y, headingTo.z));

        switch (state)
        {
            case ThinkingSpawnable.States.Idle:
                bool targetFound = gameManager.FindClosestInList(transform.position, gameManager.GetAttackList(faction, targetType),
                    targetType, out targetToPass);

                if (!targetFound)
                    Debug.LogError("No targets found");

                SetTarget(targetToPass);
                Seek();

                break;

            case ThinkingSpawnable.States.Seeking:
                if (navMeshAgent.isStopped)
                {
                    Stop();
                    if (target != null)
                        LookTowards(target.transform.position);
                }
                else
                {
                    Seek();
                }
                break;
        }

        lastPosition = transform.position;
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
        // animator.SetFloat("MoveSpeed", speed); //will act as multiplier to the speed of the run animation clip

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

        if (Vector3.Distance(target.transform.position, transform.position) > attackRange)
        {
            base.Seek();
            navMeshAgent.SetDestination(target.transform.position);
            navMeshAgent.isStopped = false;
        }

        // animator.SetBool("IsMoving", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedWith = other.gameObject;       
        switch (collidedWith.tag)
        {
            case ("Core"):
                if (this.state == States.Seeking)// Se chequea el estado seeking porque no hay forma de diferenciar a una torreta de un enemigo, ambos son entity. Arreglar ésto
                {
                    Destroy(this.gameObject);
                    //TODO damage to core
                }              
                    break;
        }
    }

    public override void StartAttack()
    {
        base.StartAttack();

        navMeshAgent.isStopped = true;
        // animator.SetBool("IsMoving", false);
    }

    public override void DealBlow()
    {
        base.DealBlow();

        // animator.SetTrigger("Attack");
        transform.forward = (target.transform.position - transform.position).normalized; //turn towards the target
    }

    protected override void Die()
    {
        base.Die();

        navMeshAgent.enabled = false;
        // animator.SetTrigger("IsDead");
    }

}
