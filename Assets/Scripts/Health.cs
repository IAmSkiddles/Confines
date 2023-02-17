using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health, maxHealth;

    public UnityEvent<GameObject> OnHitEvent, OnDeathEvent;

    [SerializeField]
    private bool alive = true;

    public void InitializeHealth(int healthValue)
    {
        health = healthValue;
        maxHealth = healthValue;
        alive = true;
    }

    public void OnHit(int amount, GameObject attacker)
    {
        if (!alive) return;
        if (attacker.layer == gameObject.layer) return;

        health -= amount;

        if (health > 0) 
        { 
            OnHitEvent?.Invoke(attacker);
        }
        else
        {
            OnDeathEvent?.Invoke(attacker);
            alive = false;
            Destroy(gameObject);
        }
    }
}
