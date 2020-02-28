using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InOutFade : MonoBehaviour
{
    public float fadeInTime = 1f;
    public float displayTime = 5f;
    public float fadeOutTime = 1f;

    public Text text;

    private Coroutine fadeCor;

    public void StartInOut()
    {
        if (fadeCor != null)
        {
            StopCoroutine(fadeCor);
        }

        fadeCor = StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        Color originalColor = text.color;

        float time = 0f;
        while (time < fadeInTime)
        {
            time += Time.unscaledDeltaTime;

            text.color = Color.Lerp(Color.clear, originalColor, time / fadeInTime);

            yield return null;
        }

        text.color = originalColor;

        yield return new WaitForSecondsRealtime(displayTime);

        time = 0f;
        while (time < fadeOutTime)
        {
            time += Time.unscaledDeltaTime;

            text.color = Color.Lerp(originalColor, Color.clear, time / fadeOutTime);

            yield return null;
        }

        text.color = Color.clear;

        fadeCor = null;
        gameObject.SetActive(false); // Self disable
    }
}
