using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScale : MonoBehaviour
{
    public float scaleDown = 1f;

    private void Update()
    {
        if (transform.localScale != Vector3.zero)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, scaleDown * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
