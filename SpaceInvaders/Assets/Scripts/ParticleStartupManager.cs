using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

        if (container != null)
        {
            GameObject ga = Instantiate(system, position + Vector3.back, Quaternion.identity, container);
        }
        else
        {
            GameObject ga = Instantiate(system, position + Vector3.back, Quaternion.identity);
        }
    }
}