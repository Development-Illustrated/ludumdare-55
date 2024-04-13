using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 6.0f;
    [SerializeField] Vector3 moveDirection = Vector3.forward;

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime);
    }

}
