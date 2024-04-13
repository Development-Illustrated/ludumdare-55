using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summonager : MonoBehaviour
{
    [SerializeField]
    protected MonsterSO[] monsterObjects;

    [SerializeField]
    protected GameObject monsterPrefab;

    [SerializeField]
    protected GameObject spawnPoint;

    Dictionary<OrbColor, MonsterSO> summonagerMap = new Dictionary<OrbColor, MonsterSO>();

    protected enum State
    {
        Empty,
        Head,
        Torso,
        Legs
    }

    protected State currentState = State.Empty;

    // protected GameObject currentMonster;
    protected Monster currentMonster;

    private void Awake()
    {
        // Fill the dictionary with the summonagers and their respective colors
        foreach (MonsterSO monsterObject in monsterObjects)
        {
            OrbColor randomColor = (OrbColor)Random.Range(0, System.Enum.GetValues(typeof(OrbColor)).Length);
            while (summonagerMap.ContainsKey(randomColor))
            {
                randomColor = (OrbColor)Random.Range(0, System.Enum.GetValues(typeof(OrbColor)).Length);
            }

            summonagerMap.Add(randomColor, monsterObject);
        }
    }

    public void Deposit(Ingredient depositedIngredient)
    {
        // Grab the summonagerObject that corresponds to the color of the deposited ingredient
        if (summonagerMap.ContainsKey(depositedIngredient.color))
        {
            MonsterSO monsterObject = summonagerMap[depositedIngredient.color];

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
                    break;
            }


            UpdateState();
        }
        else
        {
            Debug.LogError("No summonager found for color " + depositedIngredient.color);
        }
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
