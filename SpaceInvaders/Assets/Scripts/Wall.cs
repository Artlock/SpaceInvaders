using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.attachedRigidbody?.GetComponent<Bullet>();

        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
    }
}
