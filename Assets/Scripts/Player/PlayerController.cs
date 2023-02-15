using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character Movement Attributes:")]
    public float baseSpeed = 4.0f;
    public float speedMultiplier = 2.0f;
    public float acceleration = 2.0f;
    public float deceleration = 4.0f;
    public Vector2 MovementInput { get; set; }

    private Vector2 oldMovementInput;
    public float speed;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (MovementInput.magnitude > 0 && speed >= 0) 
        {
            oldMovementInput = MovementInput;
            speed += baseSpeed * acceleration * Time.deltaTime;
        }
        else 
        {
            speed -= baseSpeed * deceleration * Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, 0, baseSpeed);
        rb2d.velocity = (oldMovementInput * speed) * speedMultiplier;
    }
}  
