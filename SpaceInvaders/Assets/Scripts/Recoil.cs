using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public JuicyToggler animationToggler;

    public AnimationCurve recoilCurve; // Recoil curve
    public Vector3 recoilVector = Vector3.down;
    public float maxDistance = 1f;
    public float timeIn = 0.1f;
    public float timeOut = 0.2f;

    private float state = 0f;
    private Vector3 initialLocalPos;
    private Coroutine recoilCor;

    private void Awake()
    {
        initialLocalPos = transform.localPosition;

        Player.OnShoot += DoRecoil;
    }

    private void OnDestroy()
    {
        Player.OnShoot -= DoRecoil;
    }

    public void DoRecoil()
    {
        if (!gameObject.activeInHierarchy || !animationToggler.toggleState) return;

        if (recoilCor != null)
        {
            StopCoroutine(recoilCor);
        }

        recoilCor = StartCoroutine(RecoilCoroutine());
    }

    private IEnumerator RecoilCoroutine()
    {
        float elapsedTime = state * timeIn; // Not from 0 since we want to pick back up from any previous recoil not finished

        while (elapsedTime < timeIn)
        {
            elapsedTime += Time.deltaTime;
            state = elapsedTime / timeIn;

            transform.localPosition = initialLocalPos + recoilVector * recoilCurve.Evaluate(state);

            yield return null;
        }

        state = 1f;
        elapsedTime = 0;

        while (elapsedTime < timeOut)
        {
            elapsedTime += Time.deltaTime;
            state = 1f - (elapsedTime / timeOut); // Reverse behavior to have a decreasing state

            transform.localPosition = initialLocalPos + recoilVector * recoilCurve.Evaluate(state);

            yield return null;
        }

        state = 0f;
    }
}
