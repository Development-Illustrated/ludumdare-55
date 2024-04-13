using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed = 6.0f;

    CharacterController characterController;
    Camera cam;
    Vector3 moveInput;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.enableOverlapRecovery = true;
        cam = Camera.main;     
    }

    private void Update()
    {
        simpleMove(moveInput);
    }

    public void requestMove(Vector2 input)
    {
        moveInput = new Vector3(input.x, 0f, input.y);
    }

    public void requestInteract()
    {
        Debug.Log("Interact");
    }

    Vector3 getTargetDirection(float inputx, float inputy)
    {
        Vector3 targetDirection = new Vector3(inputx, 0f, inputy);
        targetDirection = cam.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;
        return targetDirection;
    }

    void move(Vector3 moveInput)
    {
        Vector3 targetDirection = getTargetDirection(moveInput.x, moveInput.z);
        targetDirection *= speed;
        characterController.Move(targetDirection * Time.deltaTime);
    }

    void simpleMove(Vector3 moveInput)
    {
        Vector3 targetDirection = getTargetDirection(moveInput.x, moveInput.z);
        targetDirection.Normalize(); // Normalize the vector to prevent faster diagonal movement
        targetDirection *= speed;
        characterController.SimpleMove(targetDirection);
    }
}
