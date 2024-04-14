using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DepositPoint : MonoBehaviour
{
    [SerializeField] Transform ingredientHolder;
    Ingredient currentIngredient;

    public Ingredient CurrentIngredient { get => currentIngredient; }

    public void DepositIngredient(Ingredient ingredient)
    {
        if (!currentIngredient)
        {
            Debug.Log("Deposited ingredient: " + ingredient);
            currentIngredient = ingredient;
            ingredient.transform.parent = ingredientHolder;
        }
        else
        {
            Debug.LogError("Deposit point " + this.gameObject.name + " already has an ingredient");
        }
    }

    public void RemoveIngredient()
    {
        if (currentIngredient)
        {
            GameObject.Destroy(currentIngredient.gameObject);
            currentIngredient = null;
        }
    }

}
