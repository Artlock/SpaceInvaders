using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hittable))]
public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletsContainer;
    public Collider playableArea;
    public Hittable hittable;

    // Movement related
    public float speed = 4f;
    private Vector3 direction = Vector3.zero;

    // Shooting related
    public float damagePerShot = 10f;
    public float cooldown = 1f;
    private float lastShotTime = 0f;

    public void Update()
    {
        Movement();

        Shooting();
    }

    public void Movement()
    {
        // Movement
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)) // Left
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Right
        {
            direction += Vector3.right;
        }

        if (playableArea.bounds.Contains(transform.position + direction * speed * Time.deltaTime))
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void Shooting()
    {
        // Shooting
        if (Input.GetKey(KeyCode.Space) && Time.time > (lastShotTime + cooldown))
        {
            lastShotTime = Time.time;

            // Instantiate (Will shoot in awake)
            Bullet blt = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletsContainer.transform).GetComponent<Bullet>();
            blt.ignoreTeams.Add(hittable.team);
            blt.damagePerShot = damagePerShot;
            blt.Shoot(Vector3.up);
        }
    }

    public void Die()
    {
        GameManager.Instance.EndGame(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null || !other.attachedRigidbody.CompareTag("Enemy")) return;

        Enemy enemy = other.attachedRigidbody?.GetComponent<Enemy>();

        if (enemy != null)
        {
            hittable.Hit(Mathf.Infinity);
        }
    }
}
