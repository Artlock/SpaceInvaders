using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthReader : MonoBehaviour
{
    public Text text;

    private void Update()
    {
        if (Player.Instance != null)
        {
            text.text = Player.Instance.healthSystem.health + " - HP";
        }
        else
        {
            text.text = "0 - HP";
        }
    }
}
