using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    private GameObject playerWeapon;

    private bool flipped = false;

    public Vector2 PointerPosition { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        playerWeapon = transform.Find("Weapon").gameObject;
    }

    public void Animate()
    {
        animator.SetFloat("Speed", playerController.speed);

        Transform weaponTransform = playerWeapon.transform;
        Vector2 direction = (PointerPosition - (Vector2) transform.position).normalized;

        // Change the sprite based on the location of the mouse
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        // Weapon rotation
        float rotation_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        Vector2 scale = weaponTransform.localScale;

        if (Mathf.Abs(rotation_z) > 90)
        {
            scale.y = -1;
        }
        else if (Mathf.Abs(rotation_z) < 90)
        {
            scale.y = 1;
        }

        weaponTransform.localScale = scale;

        // The player sprite has no left facing animations
        // Flipping the sprite to fix this
        if (PointerPosition.x < transform.position.x && !flipped)
        {
            Flip();
        }
        else
        if (PointerPosition.x > transform.position.x && flipped)
        {
            Flip();
        }

        // Place the weapon behind the player sprite when the hammer is above
        if (weaponTransform.eulerAngles.z > 0 && weaponTransform.eulerAngles.z < 180) 
        { 
            playerWeapon.GetComponentInChildren<SpriteRenderer>().sortingOrder = GetComponentInChildren<SpriteRenderer>().sortingOrder - 1;
        } else {
            playerWeapon.GetComponentInChildren<SpriteRenderer>().sortingOrder = GetComponentInChildren<SpriteRenderer>().sortingOrder + 1;
        }
    }

    void Flip()
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        sr.flipX = !sr.flipX;

        flipped = !flipped;
    }
}
