using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected float minWaitTime;
    [SerializeField] protected float maxWaitTime;
    [SerializeField] protected float walkRadius;
    [SerializeField] protected float playerProximity;

    private float waitTime = 0;
    private bool isMoving = false;
    private MonsterState currentState;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Monster monster = gameObject.GetComponent<Monster>();
        currentState = monster.currentState;

        if (waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            isMoving = false;
        }

        if (!isMoving)
        {
            switch (currentState)
            {
                case MonsterState.Angry:
                case MonsterState.Loving:
                    agent.SetDestination(MoveTowardsPlayer());
                    break;
                case MonsterState.Passive:
                default:
                    agent.SetDestination(RandomNavmeshLocation());
                    break;
            }
        }
    }

    private void MakePlayerYeet()
    {
        Monster monster = gameObject.GetComponent<Monster>();
        GameObject player = GameObject.Find("Player");

        player.GetComponent<PlayerController>().RequestYeet();
        monster.SetState(MonsterState.Passive);
    }

    private Vector3 MoveTowardsPlayer()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 proximityToPlayer = player.transform.position.normalized - transform.position.normalized;

        if ((Mathf.Abs(proximityToPlayer.x) + Mathf.Abs(proximityToPlayer.z)) <= playerProximity && currentState == MonsterState.Angry)
        {
            MakePlayerYeet();
        }

        return player.transform.position;
    }

    private Vector3 RandomNavmeshLocation()
    {
        isMoving = true;
        waitTime = Random.Range(minWaitTime, maxWaitTime);

        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
