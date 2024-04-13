using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PedistalOrb : MonoBehaviour
{
    [SerializeField] protected float minRespawnTime;
    [SerializeField] protected float maxRespawnTime;
    [SerializeField] public Ingredient ingredient;

    public bool isRespawning;
    private float respawnCounter;

    // Start is called before the first frame update
    void Start()
    {
        // ingredient = ingredients[Random.Range(0, ingredients.length)];
        // orbRenderer.material.color = ingredient.color;
    }

    // Update is called once per frame
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

                // ingredient = ingredients[Random.Range(0, ingredients.length)];
                // orbRenderer.material.color = ingredient.color;
            }
        }
    }

    public void PickUpOrb()
    {
        Debug.Log("Picked up ingredient: " + ingredient);
        ingredient.gameObject.SetActive(false);
        respawnCounter = Random.Range(minRespawnTime, maxRespawnTime);
        isRespawning = true;
    }
}
