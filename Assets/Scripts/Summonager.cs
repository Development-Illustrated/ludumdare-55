using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summonager : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] orangePrefabs;
    [SerializeField]
    protected GameObject[] redPrefabs;
    [SerializeField]
    protected GameObject[] bluePrefabs;
    Dictionary<string, List<GameObject>> ingredients;

    // Start is called before the first frame update
    void Start()
    {
        ingredients.Add("orange", new List<GameObject> {
            orangePrefabs[0],
            orangePrefabs[1],
            orangePrefabs[2],
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
