using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    public float health = 100f;
    public UnityEvent OnDeath;

    public void Damage(float dmg)
    {
        health -= dmg;

        if (health <= 0f) OnDeath?.Invoke();
    }
}
