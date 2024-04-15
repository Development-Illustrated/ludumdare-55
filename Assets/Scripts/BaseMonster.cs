using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonster : MonoBehaviour
{
    [HideInInspector] public GameObject head;
    [HideInInspector] public GameObject torso;
    [HideInInspector] public GameObject legs;
 

    public MonsterState currentState = MonsterState.Building;

    private void Start()
    {
       
    }

    public void SetState(MonsterState state)
    {
        currentState = state;
    }

    public void AddHead(GameObject headPrefab)
    {
        if (!head)
        {
            head = Instantiate(headPrefab, transform.position, transform.rotation, gameObject.transform);
        }
        else
        {
            Debug.LogError("Head already exists");
        }
    }

    public void AddTorso(GameObject torsoPrefab)
    {
        if (head && !torso)
        {
            torso = Instantiate(torsoPrefab, transform.position, transform.rotation, gameObject.transform);


            Transform torsoAnchor = head.transform.Find("TorsoAnchor");
            Transform headAnchor = torso.transform.Find("HeadAnchor");

            Vector3 torsoPosition = torsoAnchor.position - (headAnchor.position - torso.transform.position);

            torso.transform.position = torsoPosition;
        }
        else
        {
            Debug.LogError("Torso already exists or head doesn't exist");
        }
    }

    public void AddLegs(GameObject legsPrefab)
    {
        if (head && torso && !legs)
        {
            legs = Instantiate(legsPrefab, transform.position, transform.rotation, gameObject.transform);

            Transform legsAnchor = torso.transform.Find("LegsAnchor");
            Transform torsoAnchor = legs.transform.Find("TorsoAnchor");

            Vector3 legsPosition = legsAnchor.position - (torsoAnchor.position - legs.transform.position);

            legs.transform.position = legsPosition;
        }
        else
        {
            Debug.LogError("Legs already exist or head/torso doesn't exist");
        }
    }

    public abstract bool Activate();
}
