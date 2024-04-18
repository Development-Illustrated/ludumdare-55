using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Summonager : MonoBehaviour
{

    [SerializeField] protected AudioClip successSound;
    [SerializeField] protected AudioClip failureSound;
    [SerializeField] protected AudioClip putdownSound;

    [SerializeField]
    protected MonsterSO[] monsterObjects;

    [SerializeField]
    protected GameObject monsterPrefab;

    [SerializeField]
    protected GameObject spawnPoint;

    [SerializeField] protected DepositPoint[] depositPoints;

    Dictionary<Color, MonsterSO> monsterMap = new Dictionary<Color, MonsterSO>();

    AudioSource audioSource;

    protected enum State
    {
        Empty,
        Head,
        Torso
    }

    protected State currentState = State.Empty;

    protected Monster currentMonster;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Ingredient[] ingredients = FindObjectsOfType<Ingredient>();

        //Color[] colors = ingredients.Select(ingredient => ingredient.color).ToArray();
        Color[] colors = {
            Color.red,
            Color.magenta,
            Color.blue,
            Color.yellow,
            Color.black,
            Color.white
        };

        // Fill the dictionary with the monsters and their respective colors
        /*
        foreach (MonsterSO monsterObject in monsterObjects)
        {
            Color randomColor = colors[UnityEngine.Random.Range(0, colors.Length)];
            Debug.Log("Assigning " + monsterObject.name + " to Color " + randomColor.ToString());

            while (monsterMap.ContainsKey(randomColor))
            {
                randomColor = colors[UnityEngine.Random.Range(0, colors.Length)];
            }

            monsterMap.Add(randomColor, monsterObject);
        }
        */
        for (int i = 0; i < colors.Count(); i++){
            monsterMap.Add(colors[i], monsterObjects[i]);
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
        audioSource.PlayOneShot(putdownSound);

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
                    monsterObject.SpawnTorso(currentMonster);
                    currentMonster.transform.position = new Vector3(
                        currentMonster.transform.position.x,
                        currentMonster.transform.position.y + currentMonster.torso.GetComponent<BoxCollider>().size.y / 2, 
                        currentMonster.transform.position.z
                    );
                    break;
                case State.Torso:
                    monsterObject.SpawnLegs(currentMonster);
                    currentMonster.transform.position = new Vector3(
                        currentMonster.transform.position.x,
                        currentMonster.transform.position.y + currentMonster.legs.GetComponent<BoxCollider>().size.y / 2, 
                        currentMonster.transform.position.z
                    );
                  
                    if (currentMonster.Activate()){
                        audioSource.PlayOneShot(successSound);
                    } else {
                        audioSource.PlayOneShot(failureSound);
                    }
                    foreach (DepositPoint point in depositPoints)
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
            currentState = State.Empty;
            currentMonster = null;
        }
    }
}
