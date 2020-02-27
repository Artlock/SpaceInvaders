using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;
    public JuicyToggler cameraToggle;

    public float shakeDuration = 0.1f;
    public float shakeAmount = 0.1f;
    public float maxDistanceDeltaDuringReset = 0.05f;

    private Vector3 originalPos;
    private float timeToShake;

    private void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent<Transform>();
        }

        Player.OnShoot += Shake;
    }

    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    private void Update()
    {
        if (timeToShake > 0)
        {
            camTransform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * shakeAmount;

            timeToShake -= Time.deltaTime;
        }
        else
        {
            timeToShake = 0f;
            camTransform.localPosition = Vector3.MoveTowards(camTransform.localPosition, originalPos, maxDistanceDeltaDuringReset);
        }
    }

    public void Shake()
    {
        if (!cameraToggle.toggleState) return;

        timeToShake = shakeDuration;
    }
}
