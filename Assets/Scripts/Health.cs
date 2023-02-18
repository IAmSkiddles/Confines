using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health, maxHealth, invincibilityTimer, invincibilityDeltaTime;

    public UnityEvent<GameObject> OnHitEvent, OnDeathEvent;

    [SerializeField]
    private bool alive = true;

    private bool invincible = false;

    public void InitializeHealth(int healthValue)
    {
        health = healthValue;
        maxHealth = healthValue;
        alive = true;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    private IEnumerator Blink()
    {
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        playerSprite.enabled = false;
        yield return new WaitForSeconds(0.05f);
        playerSprite.enabled = true;
    }

    private IEnumerator InvincibilityFrames()
    {
        invincible = true;
        for (float i = 0; i < invincibilityTimer; i += invincibilityDeltaTime)
        {
            StartCoroutine(Blink());

            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        
        invincible = false;
    }

    public void OnHit(int damage, GameObject attacker)
    {
        if (!alive || invincible) return;
        if (attacker.layer == gameObject.layer) return;

        health = Mathf.Clamp(health - damage, 0, maxHealth);

        if (health > 0) 
        {
            StartCoroutine(InvincibilityFrames());
            OnHitEvent?.Invoke(attacker);
        }
        else
        {
            OnDeathEvent?.Invoke(attacker);
            alive = false;
        }
    }

    public void Destory()
    {
        Destroy(gameObject);
    }
}
