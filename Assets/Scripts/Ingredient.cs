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

        // Dont trigger if held by player
        if(this.transform.parent != null)
        {
            return;
        }

        if(debugMode){Debug.Log("PlayerController: OnTriggerEnter called with " + other.tag);}

        if (other.tag == "Summonager")
        {
            if(debugMode){Debug.Log("Ingredient: OnTriggerEnter called with " + other.tag);}
            Summonager hitSummonager = other.GetComponent<Summonager>();
            bool gr8Success = hitSummonager.Deposit(this);
            if (gr8Success)
            {
                LetsGetMagical();
            }
        }
    }

    public void LetsGetPhysical()
    {
        if(debugMode){Debug.Log("Ingredient: LetsGetPhysical called");}
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<Collider>().enabled = true;
    }

    public void LetsGetMagical()
    {
        if(debugMode){Debug.Log("Ingredient: LetsGetMagical called");}
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<Collider>().enabled = false;
    }
}
