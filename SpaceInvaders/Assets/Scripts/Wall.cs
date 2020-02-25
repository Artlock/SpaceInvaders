using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.rigidbody.GetComponent<Bullet>();

        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
    }
}
