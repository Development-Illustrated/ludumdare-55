using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    public static RequestManager instance;

    [SerializeField]
    protected GameObject spawnPoint;

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

    protected List<Request> activeRequests = new List<Request>();

    protected enum State
    {
        Paused,
        Active
    }

    protected State currentState = State.Paused;

    private void Awake()
    {
        CreateSingleton();
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
        Request request = GenerateRequest();
        activeRequests.Add(request);
        currentState = State.Paused;
    }

    // Separate the timeout and complete request methods
    // So we can handle adding or removing from a score
    public void TimeoutRequest(Request request)
    {
        RemoveRequest(request);
    }

    // Separate the timeout and complete request methods
    // So we can handle adding or removing from a score
    public void CompleteRequest(Request request)
    {
        RemoveRequest(request);
    }

    public bool CheckRequest(Monster monster)
    {
        foreach (Request request in activeRequests)
        {
            if (request.head == monster.head && request.torso == monster.torso && request.legs == monster.legs)
            {
                CompleteRequest(request);
                return true;
            }
        }

        // Monster does not match any states in the active requests
        return false;
    }

    private void RemoveRequest(Request request)
    {
        activeRequests.Remove(request);
        Destroy(request);
    }

    private Request GenerateRequest()
    {
        GameObject randomHead = monsterScriptableObjects[UnityEngine.Random.Range(0, monsterScriptableObjects.Length)].headPrefab;
        GameObject randomTorso = monsterScriptableObjects[UnityEngine.Random.Range(0, monsterScriptableObjects.Length)].torsoPrefab;
        GameObject randomLegs = monsterScriptableObjects[UnityEngine.Random.Range(0, monsterScriptableObjects.Length)].legsPrefab;

        GameObject newRequest = Instantiate(requestMonsterPrefab, spawnPoint.transform.position, Quaternion.identity);
        Request requestScript = newRequest.GetComponent<Request>();

        requestScript.AddHead(randomHead);
        requestScript.AddTorso(randomTorso);
        requestScript.AddLegs(randomLegs);

        requestScript.durationInSeconds = requestDurationInSeconds;

        // Assuming we want to activate a request straight away
        requestScript.Activate();

        return requestScript;
    }
}