using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered, OnAttackTriggered;

    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackTriggered?.Invoke();
    }
}
