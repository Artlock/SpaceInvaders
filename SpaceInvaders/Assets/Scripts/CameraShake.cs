using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;

    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.7f;

    public float maxDistanceDelta = 0.1f;

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
            camTransform.localPosition = Vector3.MoveTowards(camTransform.localPosition, originalPos, maxDistanceDelta);
        }
    }

    public void Shake()
    {
        timeToShake = shakeDuration;
    }
}
