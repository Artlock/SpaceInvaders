using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    public float speed = 6f;

    private Vector3 direction = Vector3.zero;

    private void Awake()
    {
        Shoot(Vector3.up, speed);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Shoot(Vector3 dir, float spd)
    {
        direction = dir;
        speed = spd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && !other.attachedRigidbody.CompareTag("Enemy")) return;

        Enemy enemy = other.attachedRigidbody?.GetComponent<Enemy>();

        if (enemy != null)
        {
            Destroy(enemy.gameObject);
            Destroy(gameObject);
        }
    }
}
