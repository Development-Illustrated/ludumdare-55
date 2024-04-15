using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected float minChangeBehaviourCount;
    [SerializeField] protected float maxChangeBehaviourCount;
    [SerializeField] protected float walkRadius;
    [SerializeField] protected float playerProximity;

    private float changeBehaviourCount = 0;
    private bool isMoving = false;
    private MonsterState currentState;
    private Monster monster;
    private GameObject player;
    private PlayerController playerController;

    void Start()
    {
        monster = gameObject.GetComponent<Monster>();
        player = GameObject.Find("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState = monster.currentState;
        if (currentState != MonsterState.Building)
        {
            if (changeBehaviourCount <= 0)
            {
                RandomiseBehaviour();
            }
            else
            {
                changeBehaviourCount -= Time.deltaTime;
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
    }

    private void RandomiseBehaviour()
    {
        isMoving = false;
        changeBehaviourCount = UnityEngine.Random.Range(minChangeBehaviourCount, maxChangeBehaviourCount);

        int randomState;
        do
        {
            randomState = (int)Mathf.Round(UnityEngine.Random.Range(1f, 3f));
        } while (currentState == (MonsterState)randomState && currentState != MonsterState.Angry);

        Debug.Log("Changing state from " + currentState.ToString() + " to " + ((MonsterState)randomState).ToString());

        monster.SetState((MonsterState)randomState);
    }

    private void MakePlayerYeet()
    {
        playerController.RequestYeet();
        monster.SetState(MonsterState.Passive);
    }

    private Vector3 MoveTowardsPlayer()
    {
        Vector3 proximityToPlayer = player.transform.position.normalized - transform.position.normalized;

        if (Mathf.Abs(proximityToPlayer.x) <= playerProximity && Mathf.Abs(proximityToPlayer.z) <= playerProximity && currentState == MonsterState.Angry)
        {
            MakePlayerYeet();
        }

        return player.transform.position;
    }

    private Vector3 RandomNavmeshLocation()
    {
        isMoving = true;

        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * walkRadius;

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
