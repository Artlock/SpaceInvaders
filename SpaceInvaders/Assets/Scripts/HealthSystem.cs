using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    public float health = 100f;
    public UnityEvent OnDeath;

    public bool isDead { get; private set; } = false;

    public void Damage(float dmg)
    {
        if (isDead) return;

        health -= dmg;

        if (health <= 0f)
        {
            isDead = true;
            OnDeath?.Invoke();
        }
    }
}
