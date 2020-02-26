using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Team
{
    Enemy,
    Ally,
    Any
}

public class Hittable : MonoBehaviour
{
    public Team team;
    public HitEvent OnHit;

    public void Hit(float dmg)
    {
        OnHit?.Invoke(dmg);
    }
}

[Serializable]
public class HitEvent : UnityEvent<float> { }
