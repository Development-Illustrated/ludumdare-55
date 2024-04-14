using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/Monster Object", order = 1)]
public class MonsterSO : ScriptableObject
{
    [SerializeField]
    public GameObject headPrefab;
    [SerializeField]
    public GameObject torsoPrefab;
    [SerializeField]
    public GameObject legsPrefab;

    public void SpawnHead(Monster parentMonster)
    {
        if (headPrefab)
        {
            parentMonster.AddHead(headPrefab);
        }
        else
        {
            Debug.LogError("Head prefab not set");
        }
    }
    public void SpwawnTorso(Monster parentMonster)
    {
        if (torsoPrefab)
        {
            parentMonster.AddTorso(torsoPrefab);
        }
        else
        {
            Debug.LogError("Torso prefab not set");
        }

    }
    public void SpawnLegs(Monster parentMonster)
    {
        if (legsPrefab)
        {
            parentMonster.AddLegs(legsPrefab);
        }
        else
        {
            Debug.LogError("Legs prefab not set");
        }
    }
}
