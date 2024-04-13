using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SummonagerObject", menuName = "ScriptableObjects/SummonagerScriptableObject", order = 1)]
public class SummonagerSO : ScriptableObject
{
    [SerializeField]
    protected GameObject headPrefab;
    [SerializeField]
    protected GameObject torsoPrefab;
    [SerializeField]
    protected GameObject legsPrefab;

    public void SpawnHead(Vector3 position, Quaternion rotation)
    {
        if(headPrefab){
            Instantiate(headPrefab, position, rotation);
        } else {
            Debug.LogError("Head prefab not set");
        }
    }
    public void SpwawnTorso(Vector3 position, Quaternion rotation)
    {
        if(torsoPrefab){
            Instantiate(torsoPrefab, position, rotation);
        } else {
            Debug.LogError("Torso prefab not set");
        }

    }
    public void SpawnLegs(Vector3 position, Quaternion rotation) 
    {
        if(legsPrefab){
            Instantiate(legsPrefab, position, rotation);
        } else {
            Debug.LogError("Legs prefab not set");
        }
    }
}
