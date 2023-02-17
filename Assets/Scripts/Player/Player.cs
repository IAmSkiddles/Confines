using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerAnimator playerAnimator;
    private PlayerController playerController;
    

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerPosition => pointerInput;

    [SerializeField]
    private InputActionReference movement, attack, interact, pointerPosition;

    private void Awake()
    {
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        playerController = GetComponent<PlayerController>();
    }

    public void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }

    public void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }
    private void PerformAttack(InputAction.CallbackContext obj)
    {
        playerController.Attack();
    }

    private void Update()
    {
        pointerInput = GetPointerInput();

        movementInput = movement.action.ReadValue<Vector2>();

        playerController.MovementInput = movementInput.normalized;
        playerAnimator.PointerPosition = pointerInput;

        playerAnimator.Animate();
    }

    private Vector2 GetPointerInput() 
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
