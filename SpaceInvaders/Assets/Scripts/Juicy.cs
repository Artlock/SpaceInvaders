using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Juicy : MonoBehaviour
{
    public static Juicy Instance { get; private set; }

    public enum Features
    {
        Sfx,
        Particles,
        Controls,
        Animations,
        Cam,
        Ui,
        Music,
        Graphical
    }

    [Serializable]
    public class FeatureToggle
    {
        public Features feature;
        public KeyCode key = KeyCode.None;
        public bool enabled = false;
    }

    public List<FeatureToggle> features = new List<FeatureToggle>();

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

    private void Update()
    {
        foreach (FeatureToggle feature in features)
            FlipOnInput(feature);
    }

    private void FlipOnInput(FeatureToggle feature)
    {
        if (Input.GetKeyDown(feature.key))
            feature.enabled = !feature.enabled;
    }

    public bool IsFeatureEnabled(Features feat)
    {
        // The multiple finds every frame is ugly
        // But that's because we can't serialize dictionaries (Would need the Odin plugin)
        // And we need to serialize our entries (Because of keycodes)
        FeatureToggle ft = features.Find(x => x.feature == feat);

        if (ft == null)
            return false;
        else
            return ft.enabled;
    }
}
