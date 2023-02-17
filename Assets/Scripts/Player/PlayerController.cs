using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character Attributes:")]
    public float baseSpeed = 4.0f;
    public float speedMultiplier = 2.0f;
    public float acceleration = 2.0f;
    public float deceleration = 4.0f;
    public float attackDelay = 0.3f;

    [HideInInspector]
    public Vector2 MovementInput { get; set; }
    [HideInInspector]
    public bool attacking = false;
    [HideInInspector]
    public float speed;

    private Vector2 oldMovementInput;

    private Rigidbody2D rb2d;
    public Animator animator;

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

    public void Attack() 
    {
        if (attacking) return;
        animator.SetTrigger("Attack");
        attacking = true;

        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        attacking = false;
    }
}  
