using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float movementSpeed = 6.0f;
    [SerializeField] float rotationSpeed = 10.0f;
    [SerializeField] Transform orbHolder;
    [SerializeField] bool debugMode = false;

    [SerializeField] float yeetForce = 100f;

    Ingredient currentIngredient;
    PedistalOrb occupiedPedistal;

    Summonager occupiedSummonager;

    CharacterController characterController;
    Animator animator;

    Yeet yeet;

    Camera cam;
    Vector3 moveInput;
    Vector3 targetDirection;
    
    void Start()
    {   
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        yeet = GetComponent<Yeet>();
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
            Rotate(targetDirection);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    public bool PickUpOrb(Ingredient ingredient)
    {
        if (!currentIngredient)
        {
            Debug.Log("Picked up ingredient: " + ingredient);
            currentIngredient = Instantiate(ingredient, orbHolder); // Duplicate ingredient game object to "pick it up"    
            return true;
        }
        return false;
    }

    public void RequestMove(Vector2 input)
    {
        moveInput = new Vector3(input.x, 0f, input.y);
    }

    public void RequestYeet()
    {
        if (currentIngredient)
        {
            yeet.YeetObject(currentIngredient.gameObject, transform.forward, yeetForce, false);
            currentIngredient = null;
        }
    }

    public void RequestInteract()
    {
        Debug.Log("Interact");
        if(occupiedPedistal && !currentIngredient)
        {
            if (PickUpOrb(occupiedPedistal.ingredient))
            {
                occupiedPedistal.PickUpOrb();
            }
        }

        if(occupiedSummonager && currentIngredient)
        {
            bool gr8Success = occupiedSummonager.Deposit(currentIngredient);
            if (gr8Success)
            {
                currentIngredient = null;
            }
        }
    }

    public void Rotate(Vector3 inputDirection)
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

    void simpleMove(Vector3 inputDirection)
    {
        inputDirection *= movementSpeed;
        characterController.SimpleMove(inputDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(debugMode){Debug.Log("PlayerController: OnTriggerEnter called with " + other.tag);}

        if (other.tag == "Pedastal" && !occupiedPedistal)
        {
            occupiedPedistal = other.GetComponent<PedistalOrb>();
        }

        if (other.tag == "Summonager" && !occupiedSummonager)
        {
            occupiedSummonager = other.GetComponent<Summonager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(debugMode){Debug.Log("PlayerController: OnTriggerExit called with " + other.tag);}
        if (other.tag == "Pedastal")
        {
            PedistalOrb exitingPedistal = other.GetComponent<PedistalOrb>();
            if (exitingPedistal == occupiedPedistal)
            {
                occupiedPedistal = null;
            }
        }

        if (other.tag == "Summonager")
        {
            Summonager exitingSummonager = other.GetComponent<Summonager>();
            if (exitingSummonager == occupiedSummonager)
            {
                occupiedSummonager = null;
            }
        }
    }
}
