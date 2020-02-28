using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Weak,
    Strong,
    Ignore
}

public class Bullet : MonoBehaviour
{
    public GameObject explosionPrefab;

    public float speed = 6f;
    public float damagePerShot = 10f;
    public Team team { get; set; } = Team.None;
    public BulletType type = BulletType.Weak;

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null) return;

        Hittable hittable = other.attachedRigidbody.GetComponent<Hittable>();

        if (hittable != null)
        {
            if (hittable.team == team)
                return;

            hittable.Hit(damagePerShot);
            Destroy(gameObject);
            return;
        }

        Bullet otherBullet = other.attachedRigidbody.GetComponent<Bullet>();

        if (otherBullet != null && otherBullet.team != team)
        {
            UpdateBullet(otherBullet);
            otherBullet.UpdateBullet(this);
        }
    }

    public void UpdateBullet(Bullet otherBullet)
    {
        if (type == BulletType.Ignore || otherBullet.type == BulletType.Ignore)
            return;

        if (type == BulletType.Weak || type == otherBullet.type)
        {
            if (explosionPrefab != null) ParticleStartupManager.Instance.Spawn(explosionPrefab, (transform.position + otherBullet.transform.position) / 2f);

            Destroy(gameObject);
        }
    }
}
