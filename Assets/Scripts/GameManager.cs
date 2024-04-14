using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private Ingredient[] Ingredients;

    [HideInInspector]
    private List<Color> OrbColors = new List<Color>();

    private void CreateSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        OrbColors.Add(Color.red);
        OrbColors.Add(Color.blue);
        OrbColors.Add(Color.green);
        OrbColors.Add(Color.yellow);
        OrbColors.Add(Color.black);
        OrbColors.Add(Color.magenta);

        CreateSingleton();
    }

    private void OnEnable()
    {
        Ingredient[] ingredientsInScene = FindObjectsOfType<Ingredient>();

        foreach (Ingredient ingredient in ingredientsInScene)
        {
            Color newColor = OrbColors[UnityEngine.Random.Range(0, OrbColors.Count)];

            ingredient.color = newColor;
            Renderer renderer = ingredient.GetComponent<Renderer>();

            renderer.material.color = newColor;
            renderer.material.SetColor("_EmissionColor", newColor);

            OrbColors.Remove(newColor);
        }
    }
}
