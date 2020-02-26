using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToggle : MonoBehaviour
{
    public GameObject off;
    public GameObject on;

    public void Toggle(bool isOn)
    {
        if (isOn)
        {
            off.SetActive(false);
            on.SetActive(true);
        }
        else
        {
            off.SetActive(true);
            on.SetActive(false);
        }
    }
}
