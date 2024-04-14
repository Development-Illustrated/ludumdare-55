using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ingredient : MonoBehaviour
{

    public Color color;
    [SerializeField] public bool debugMode;

    private void OnTriggerEnter(Collider other)
    {
        if(debugMode){Debug.Log("PlayerController: OnTriggerEnter called with " + other.tag);}

        if (other.tag == "Summonager")
        {
            if(debugMode){Debug.Log("Ingredient: OnTriggerEnter called with " + other.tag);}
            Summonager hitSummonager = other.GetComponent<Summonager>();
            bool gr8Success = hitSummonager.Deposit(this);
            if (gr8Success)
            {
                this.GetComponent<Rigidbody>().isKinematic = false;
                this.GetComponent<Animator>().enabled = true;
            }
        }
    }
}


