using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summonager : MonoBehaviour
{
    public static Summonager instance;

    [SerializeField]
    protected MonsterSO[] monsterObjects;

    [SerializeField]
    protected GameObject monsterPrefab;

    [SerializeField]
    protected GameObject spawnPoint;

    Dictionary<OrbColor, MonsterSO> monsterMap = new Dictionary<OrbColor, MonsterSO>();

    protected enum State
    {
        Empty,
        Head,
        Torso,
        Legs
    }

    protected State currentState = State.Empty;

    protected Monster currentMonster;

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
        CreateSingleton();

        // Fill the dictionary with the monsters and their respective colors
        foreach (MonsterSO monsterObject in monsterObjects)
        {
            OrbColor randomColor = (OrbColor)Random.Range(0, System.Enum.GetValues(typeof(OrbColor)).Length);
            while (monsterMap.ContainsKey(randomColor))
            {
                randomColor = (OrbColor)Random.Range(0, System.Enum.GetValues(typeof(OrbColor)).Length);
            }

            summonagerMap.Add(randomColor, monsterObject);
        }
    }

    public void Deposit(Ingredient depositedIngredient)
    {
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
                    break;
            }


            UpdateState();
        }
        else
        {
            Debug.LogError("No monster found for color " + depositedIngredient.color);
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
