using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float movementSpeed = 6.0f;
    [SerializeField] float rotationSpeed = 10.0f;

    CharacterController characterController;
    Camera cam;
    Vector3 moveInput;
    Vector3 targetDirection;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.enableOverlapRecovery = true;
        cam = Camera.main;     
    }

    private void Update()
    {

        targetDirection = getTargetDirection(moveInput.x, moveInput.z);
        targetDirection.Normalize();
        simpleMove(targetDirection);
        if (moveInput != Vector3.zero)
        {
            rotate(targetDirection);
        }
    }

    public void requestMove(Vector2 input)
    {
        moveInput = new Vector3(input.x, 0f, input.y);
    }

    public void requestInteract()
    {
        Debug.Log("Interact");
    }

    public void rotate(Vector3 inputDirection)
    {
        // Rotate the player to face the direction using rotationSpeed
        Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
        targetDirection *= movementSpeed;
        characterController.Move(targetDirection * Time.deltaTime);
    }

    void simpleMove(Vector3 inputDirection)
    {
        inputDirection *= movementSpeed;
        characterController.SimpleMove(inputDirection);
    }
}
