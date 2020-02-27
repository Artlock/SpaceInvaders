using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    public float speed = 6f;
    public float damagePerShot = 10f;
    public List<Team> ignoreTeams = new List<Team>();

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
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

        hittable.Hit(damagePerShot);
        Destroy(gameObject);
    }
}
