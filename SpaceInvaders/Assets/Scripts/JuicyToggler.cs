using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JuicyToggler : MonoBehaviour
{
    public Juicy.Features feature;
    public ToggleEvent OnToggleChange;

    public bool toggleState { get; private set; } = false;

    private void Start()
    {
        toggleState = Juicy.Instance.IsFeatureEnabled(feature);
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
