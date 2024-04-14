using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Yeet : MonoBehaviour
{
    [SerializeField] bool debugMode;
    public void YeetObject(GameObject obj, Vector3 direction, float force)
    {
        if (debugMode)
        {
            Debug.Log("Yeeting object " + obj.name + " with force " + force + " in direction " + direction);
        }
        
        obj.GetComponent<Ingredient>().LetsGetPhysical();   
        obj.transform.parent = null;
        obj.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
    }
}
