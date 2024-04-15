using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PedistalOrb : MonoBehaviour
{
    [SerializeField] protected float minRespawnTime;
    [SerializeField] protected float maxRespawnTime;
    [SerializeField] public Ingredient ingredient;
    [SerializeField] protected AudioClip pickupSound;

    public bool isRespawning;
    private float respawnCounter;
    AudioSource audioSource;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (respawnCounter >= 0)
        {
            respawnCounter -= Time.deltaTime;
        }
        else
        {
            if (isRespawning)
            {
                ingredient.gameObject.SetActive(true);
                isRespawning = false;
            }
        }
    }

    public bool PickUpOrb()
    {
        if (isRespawning)
        {
            return false;
        }

        audioSource.PlayOneShot(pickupSound);
        Debug.Log("Picked up ingredient: " + ingredient);
        ingredient.gameObject.SetActive(false);
        respawnCounter = Random.Range(minRespawnTime, maxRespawnTime);
        isRespawning = true;
        return true;
    }
}
