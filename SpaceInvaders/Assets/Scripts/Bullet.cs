using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    private Vector3 direction = Vector3.zero;
    private float speed = 1f;

    private void Awake()
    {
        Shoot(Vector3.up, speed);
    }

    private void Update()
    {
        transform.position += direction * speed;
    }

    public void Shoot(Vector3 dir, float spd)
    {
        direction = dir;
        speed = spd;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            Destroy(enemy.gameObject);
            Destroy(gameObject);
        }
    }
}
