using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float speed = 3.5f;
    public float pauseTime = 2f;

    public bool isTalking = false;
    public bool wasWalking = false;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform currentTarget;
    public float pauseTimer;
    private bool isWaiting;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.speed = speed;

        currentTarget = pointB;
        agent.SetDestination(currentTarget.position);

        pauseTimer = pauseTime;
        isWaiting = false;
    }

    void Update()
    {
        HandleAnimation();

        if (animator != null)
        {
            float currentSpeed = agent.velocity.magnitude;
            animator.SetFloat("Speed", currentSpeed);

            animator.SetBool("isWalking", currentSpeed > 0.1f);
        }        

        if (isTalking)
        {
            agent.isStopped = true;
            return;
        }
        else
        {
            agent.isStopped = false;
        }

        if (isWaiting)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                isWaiting = false;
                SwitchTarget();
            }
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            isWaiting = true;
            pauseTimer = pauseTime;
        }

    }

    void SwitchTarget()
    {
        if (currentTarget == pointA)
        {
            currentTarget = pointB;
        }
        else
            currentTarget = pointA;

        agent.SetDestination(currentTarget.position);
    }

    void HandleAnimation()
    {
        if (isTalking != wasWalking)
        {
            wasWalking = isTalking;
        }

        float currentSpeed = agent.velocity.magnitude;

        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("isWalking", currentSpeed > 0.1f && !isTalking);
        animator.SetBool("isTalking", isTalking);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTalking = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTalking = false;
        }   
    }
}
