using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public ParticleSystem thruster;

    public void ToggleThruster(bool toggle)
    {
        if (toggle)
            thruster.Play();
        else
            thruster.Stop();
    }
}
