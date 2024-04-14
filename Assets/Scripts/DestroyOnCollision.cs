using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Destroying " + gameObject.name + " on collision with " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
