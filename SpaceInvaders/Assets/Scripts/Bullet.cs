using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    public float speed = 6f;
    public List<Team> ignoreTeams = new List<Team>();

    private Vector3 direction = Vector3.zero;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Shoot(Vector3 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null) return;

        Hittable hittable = other.attachedRigidbody.GetComponent<Hittable>();

        if (hittable == null) return;

        foreach (Team team in ignoreTeams)
        {
            if (hittable.team == team)
                return;
        }

        hittable.Destroy();
        Destroy(gameObject);
    }
}
