using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Summonager : MonoBehaviour
{

    [SerializeField]
    protected MonsterSO[] monsterObjects;

    [SerializeField]
    protected GameObject monsterPrefab;

    [SerializeField]
    protected GameObject spawnPoint;

    [SerializeField] protected DepositPoint[] depositPoints;

    Dictionary<Color, MonsterSO> monsterMap = new Dictionary<Color, MonsterSO>();

    protected enum State
    {
        Empty,
        Head,
        Torso,
        Legs
    }

    protected State currentState = State.Empty;

    protected Monster currentMonster;

    private void Start()
    {
        Ingredient[] ingredients = FindObjectsOfType<Ingredient>();
        Color[] colors = ingredients.Select(ingredient => ingredient.color).ToArray();

        // Fill the dictionary with the monsters and their respective colors
        foreach (MonsterSO monsterObject in monsterObjects)
        {
            Color randomColor = colors[UnityEngine.Random.Range(0, colors.Length)];

            while (monsterMap.ContainsKey(randomColor))
            {
                randomColor = colors[UnityEngine.Random.Range(0, colors.Length)];
            }

            monsterMap.Add(randomColor, monsterObject);
        }
    }

    public bool Deposit(Ingredient depositedIngredient)
    {

        // Physically move the ingredient to the deposit point
        bool deposited = false;
        for (int i = 0; i < depositPoints.Length; i++)
        {
            if (depositPoints[i].CurrentIngredient == null)
            {
                depositPoints[i].DepositIngredient(depositedIngredient);
                deposited = true;
                break;
            }
        }

        if (!deposited)
        {
            Debug.LogError("No deposit point available");
            return false;
        }

        // Grab the monsterObject that corresponds to the color of the deposited ingredient
        if (monsterMap.ContainsKey(depositedIngredient.color))
        {
            MonsterSO monsterObject = monsterMap[depositedIngredient.color];

            switch (currentState)
            {
                case State.Empty:
                    GameObject monster = Instantiate(monsterPrefab, spawnPoint.transform.position, Quaternion.identity);
                    currentMonster = monster.GetComponent<Monster>();

                    monsterObject.SpawnHead(currentMonster);
                    break;
                case State.Head:
                    monsterObject.SpwawnTorso(currentMonster);
                    break;
                case State.Torso:
                    monsterObject.SpawnLegs(currentMonster);
                    currentMonster.Activate();
                    foreach(DepositPoint point in depositPoints)
                    {
                        point.RemoveIngredient();
                    }
                    break;
            }


            UpdateState();
        }
        else
        {
            Debug.LogError("No monster found for color " + depositedIngredient.color);
        }

        return true;
    }

    protected void UpdateState()
    {
        if (currentState == State.Empty)
        {
            currentState = State.Head;
        }
        else if (currentState == State.Head)
        {
            currentState = State.Torso;
        }
        else if (currentState == State.Torso)
        {
            currentState = State.Legs;
        }
        else if (currentState == State.Legs)
        {
            currentState = State.Empty;

            currentMonster = null;
        }
    }
}
