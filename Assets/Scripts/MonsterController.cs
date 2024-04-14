using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime >= 0) {
            waitTime -= Time.deltaTime;
        }
        else
        {
            isMoving = false;
        }

        if (!isMoving) {
            agent.SetDestination(RandomNavmeshLocation());
        }
    }

    public Vector3 RandomNavmeshLocation() {
        isMoving = true;
        waitTime = Random.Range(minWaitTime, maxWaitTime);

        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }
}
