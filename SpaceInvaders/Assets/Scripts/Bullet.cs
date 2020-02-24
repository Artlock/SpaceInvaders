using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction = new Vector3();
    private float speed = 1f;

    private void Update()
    {
        transform.position += direction * speed;
    }

    public void Shoot(Vector3 dir, float spd)
    {
        direction = dir;
        speed = spd;
    }
}
