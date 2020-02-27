using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStartupManager : MonoBehaviour
{
    public static ParticleStartupManager Instance { get; private set; }

    public JuicyToggler particleToggler;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public void Spawn(GameObject system, Vector3 position, Transform container = null)
    {
        if (!particleToggler.toggleState) return;

        if (container != null) Instantiate(system, position + Vector3.back, Quaternion.identity, container);
        else Instantiate(system, position + Vector3.back, Quaternion.identity);
    }
}
