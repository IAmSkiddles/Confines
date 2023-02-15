using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerAnimator playerAnimator;
    private PlayerController playerController;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput => pointerInput;

    //private Hammer hammer;

    [SerializeField]
    private InputActionReference movement, attack, interact, pointerPosition;

    private void Awake()
    {
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        playerController = GetComponent<PlayerController>();
    }


    private void Animate()
    {
        playerAnimator.Animate(movementInput);
    }

    private void Update()
    {
        //pointerInput = Camera.main.ScreenToWorldPoint(pointerPosition.action.ReadValue<Vector2>());
        movementInput = movement.action.ReadValue<Vector2>();
        playerController.MovementInput = movementInput.normalized;

        Animate();
    }
}
