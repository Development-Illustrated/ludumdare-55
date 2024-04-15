using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DaveInput : MonoBehaviour
{
    [SerializeField] bool debugMode;
    [SerializeField] protected AudioClip throwSound;

    AudioSource audioSource;

    PlayerController playerController;
    
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }

    public void OnMove(InputValue value)
    {
        if(debugMode){Debug.Log("OnMove called with " + value.Get<Vector2>());}
        this.gameObject.SendMessage("RequestMove", value.Get<Vector2>());
    }

    public void OnInteract(InputValue value)
    {
        if(debugMode){Debug.Log("OnInteract called");}
        this.gameObject.SendMessage("RequestInteract");
    }

    public void OnYeet(InputValue value)
    {
        audioSource.PlayOneShot(throwSound);
        if(debugMode){Debug.Log("OnYeet called");}
        this.gameObject.SendMessage("RequestYeet");
    }
}
