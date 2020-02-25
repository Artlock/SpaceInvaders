using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private Coroutine lerp;

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

        // By default, timescale is 1f
        ChangeTimescale(1f, false);
    }

    public void ChangeTimescale(float target, bool shouldLerp = true, float lerpDuration = 1f) // Lerp by default
    {
        if (!shouldLerp)
        {
            Time.timeScale = target;
        }
        else
        {
            if (lerp != null)
            {
                StopCoroutine(lerp);
                lerp = null;
            }
            lerp = StartCoroutine(LerpTimescale(target, lerpDuration));
        }
    }

    private IEnumerator LerpTimescale(float target, float duration)
    {
        float initialScale = Time.timeScale;
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime < 1f)
        {
            Time.timeScale = Mathf.Lerp(initialScale, target, (Time.realtimeSinceStartup - startTime) / duration);
            yield return null;
        }

        Time.timeScale = target;

        lerp = null;
    }
}
