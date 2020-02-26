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
    public UnityEvent OnDestroy;

    public void Destroy()
    {
        OnDestroy?.Invoke();
    }
}
