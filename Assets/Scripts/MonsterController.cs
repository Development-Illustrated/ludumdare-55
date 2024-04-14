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
        Monster monster = gameObject.GetComponent<Monster>()
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
        if (currentState == MonsterState.Angry)
        {
            // Cause yeet on contact
            // After yeet move away -> return RandomNavmeshLocation()
        }
    }

    private Vector3 MoveTowardsPlayer()
    {
        GameObject player = GameObject.Find("Player");
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
