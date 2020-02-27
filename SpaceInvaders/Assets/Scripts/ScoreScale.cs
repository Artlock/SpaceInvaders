using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScale : MonoBehaviour
{

    public float initialValue;
    public float scaleDown;

    public float timeToMin;
    public float timer;
    void Start()
    {
        
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (transform.localScale.x >= 0)
        {
            this.transform.localScale -= new Vector3(scaleDown, scaleDown, 0) * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
      
    }
}
