using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStatus status;

    private Animator animator;
    private readonly int moveHash = Animator.StringToHash("Move");
    private readonly int deathHash = Animator.StringToHash("Death");

    private NavMeshAgent agent;
    private Coroutine coUpdatePath;

    [SerializeField] private Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        status.deathEvnet.AddListener(OnDead);
    }
    private void Start()
    {
        agent.speed = status.GetStatus(StatusInfo.Speed);
        coUpdatePath = StartCoroutine(CoUpdatePath());
    }
    private void Update()
    {
        animator.SetFloat(moveHash, agent.velocity.magnitude / agent.speed);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnDead()
    {
        StopCoroutine(coUpdatePath);
        agent.isStopped = true;

        animator.SetTrigger(deathHash);
        StartCoroutine(DestroyTime());
    }

    private IEnumerator CoUpdatePath()
    {
        while (!status.IsDead)
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);

            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }
}
