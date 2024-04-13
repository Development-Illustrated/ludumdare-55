using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DaveInput : MonoBehaviour
{
    [SerializeField] bool debugMode;

    PlayerController playerController;
    
    private void Awake() {
        playerController = GetComponent<PlayerController>();
    }

    public void OnMove(InputValue value)
    {
        if(debugMode){Debug.Log("OnMove called");}
        this.gameObject.SendMessage("requestMove", value.Get<Vector2>());
    }

    public void OnInteract(InputValue value)
    {
        if(debugMode){Debug.Log("OnInteract called");}
        this.gameObject.SendMessage("requestInteract");
    }
}
