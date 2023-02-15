using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    private bool flipped = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    public void Animate(Vector2 MovementInput)
    {
        // Placeholder sprite only has a right facing animation, flipped here when going left.
        // Used transform to flip so that the hammer changes direction too.
        if (MovementInput.x > 0 && flipped)
        {
            Flip();
        }
        else if (MovementInput.x < 0 && !flipped)
        {
            Flip();
        }

        if (MovementInput != Vector2.zero)
        {
            animator.SetFloat("Horizontal", MovementInput.x);
            animator.SetFloat("Vertical", MovementInput.y);
        }

        animator.SetFloat("Speed", playerController.speed);
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        flipped = !flipped;
    }
}
