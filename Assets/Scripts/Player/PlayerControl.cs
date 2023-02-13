using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Character Movement Attributes")]
    public float MOVEMENT_SPEED = 1.0f;
    public float movementSpeedMultiplier = 1.0f;

    private Vector2 movementDirection;
    private float movementSpeed;

    [Space]
    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;

    bool flipped = false;


    void Start()
    {
        
    }

    void Update()
    {
        ProcessInput();
        ProcessMovement();
        Animate();
    }

    void ProcessInput() 
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f) * movementSpeedMultiplier;
        movementDirection.Normalize();
    }

    void ProcessMovement()
    {
        rb.velocity = movementDirection * movementSpeed * MOVEMENT_SPEED;
    }

    void Animate() 
    {
        if (Input.GetAxis("Horizontal") > 0 && flipped)
        {
            Flip();
        } else if (Input.GetAxis("Horizontal") < 0 && !flipped)
        {
            Flip();
        }

        if(movementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }

        animator.SetFloat("Speed", movementSpeed);
    }

    void Flip() 
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        flipped = !flipped;
    }
}  
