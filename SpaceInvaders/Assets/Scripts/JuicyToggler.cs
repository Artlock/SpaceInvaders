using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JuicyToggler : MonoBehaviour
{
    public Juicy.Features feature;
    public ToggleEvent OnToggleChange;

    private bool toggleState = false;

    private void Awake()
    {
        OnToggleChange?.Invoke(toggleState);
    }

    private void Update()
    {
        if (Juicy.Instance.IsFeatureEnabled(feature) != toggleState)
        {
            toggleState = !toggleState;
            OnToggleChange?.Invoke(toggleState);
        }
    }
}

[Serializable]
public class ToggleEvent : UnityEvent<bool> { }
