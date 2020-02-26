using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DefeatArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null || !other.attachedRigidbody.CompareTag("Enemy")) return;

        Enemy enemy = other.attachedRigidbody?.GetComponent<Enemy>();

        if (enemy != null)
        {
            GameManager.Instance.EndGame(false);
        }
    }
}
