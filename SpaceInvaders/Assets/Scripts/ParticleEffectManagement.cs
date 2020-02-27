using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectManagement : MonoBehaviour
{
    public ParticleSystem ps;

    private float timeElapsed = 0f;

    private void Awake()
    {
        ps.Play();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= ps.main.duration)
        {
            Destroy(gameObject);
            return;
        }
    }
}
