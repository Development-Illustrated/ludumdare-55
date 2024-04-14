using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    public static RequestManager instance;

    [SerializeField]
    protected GameObject[] spawnPointPrefabs;

    [SerializeField]
    protected GameObject requestMonsterPrefab;

    [SerializeField]
    protected MonsterSO[] monsterScriptableObjects;

    [SerializeField]
    protected int maxRequests = 5;

    [SerializeField]
    protected int requestDelayInSeconds = 10;

    [SerializeField]
    protected int requestDurationInSeconds = 25;

    protected List<RequestedMonster> activeRequests = new List<RequestedMonster>();

    protected enum State
    {
        Paused,
        Active
    }

    protected Dictionary<GameObject, RequestedMonster> spawnPoints = new Dictionary<GameObject, RequestedMonster>();

    protected State currentState = State.Paused;

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        foreach (GameObject spawnPoint in spawnPointPrefabs)
        {
            spawnPoints.Add(spawnPoint, null);
        }
    }

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

    private void Update()
    {
        if (activeRequests.Count < maxRequests)
        {
            if (currentState == State.Paused)
            {
                currentState = State.Active;
                StartCoroutine(GenerateRequestDelay());
            }
        }
    }

    private IEnumerator GenerateRequestDelay()
    {
        yield return new WaitForSeconds(requestDelayInSeconds);
        DispatchRequest();
    }


    private void DispatchRequest()
    {
        RequestedMonster request = GenerateRequest();
        activeRequests.Add(request);
        currentState = State.Paused;
    }

    // Separate the timeout and complete request methods
    // So we can handle adding or removing from a score
    public void TimeoutRequest(RequestedMonster request)
    {
        RemoveRequest(request);
    }

    // Separate the timeout and complete request methods
    // So we can handle adding or removing from a score
    public void CompleteRequest(RequestedMonster request)
    {
        GameManager.instance.IncrementScore(request.durationInSeconds);
        RemoveRequest(request);
    }

    public bool CheckRequest(Monster monster)
    {
        foreach (RequestedMonster request in activeRequests)
        {
            if (request.head.name == monster.head.name && request.torso.name == monster.torso.name && request.legs.name == monster.legs.name)
            {
                CompleteRequest(request);
                return true;
            }
        }

        // Monster does not match any states in the active requests
        return false;
    }

    private void RemoveRequest(RequestedMonster request)
    {
        activeRequests.Remove(request);
        GameObject keyToRemove = spawnPoints.FirstOrDefault(kv => kv.Value == request).Key;
        spawnPoints[keyToRemove] = null;
        Destroy(request.gameObject);
    }

    private RequestedMonster GenerateRequest()
    {
        GameObject randomHead = monsterScriptableObjects[UnityEngine.Random.Range(0, monsterScriptableObjects.Length)].headPrefab;
        GameObject randomTorso = monsterScriptableObjects[UnityEngine.Random.Range(0, monsterScriptableObjects.Length)].torsoPrefab;
        GameObject randomLegs = monsterScriptableObjects[UnityEngine.Random.Range(0, monsterScriptableObjects.Length)].legsPrefab;


        KeyValuePair<GameObject, RequestedMonster> spawnPoint = spawnPoints.FirstOrDefault(kv => kv.Value == null);
        Debug.Log(spawnPoint.Key);
        Debug.Log(spawnPoint.Value);
        GameObject newRequest = Instantiate(requestMonsterPrefab, spawnPoint.Key.transform.position, Quaternion.identity);
        RequestedMonster requestScript = newRequest.GetComponent<RequestedMonster>();
        spawnPoints[spawnPoint.Key] = requestScript;

        requestScript.AddHead(randomHead);
        requestScript.AddTorso(randomTorso);
        requestScript.AddLegs(randomLegs);

        requestScript.durationInSeconds = requestDurationInSeconds;

        // Assuming we want to activate a request straight away
        requestScript.Activate();

        return requestScript;
    }
}
