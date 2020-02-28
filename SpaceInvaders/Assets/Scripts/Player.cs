using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hittable))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public GameObject bulletPrefab;
    public Transform bulletsContainer;
    public Collider playableArea;
    public Hittable hittable;
    public HealthSystem healthSystem;

    public GameObject explosionPrefab;

    // Movement related
    public float speed = 4f;
    private Vector3 direction = Vector3.zero;

    // Shooting related
    public float cooldown = 0.15f;
    public float bulletSpeed = 24f;
    public float damagePerShot = 10f;
    private float lastShotTime = 0f;
    public float shootSoundVolume = 1f;
    public float angleSideShots = 10f;

    public static event Action OnShoot;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }

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
            Bullet blt = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(Vector3.forward, Vector3.up), bulletsContainer.transform).GetComponent<Bullet>();
            blt.team = hittable.team;
            blt.damagePerShot = damagePerShot;
            blt.speed = bulletSpeed;

            blt = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, angleSideShots) * Vector3.up), bulletsContainer.transform).GetComponent<Bullet>();
            blt.team = hittable.team;
            blt.damagePerShot = damagePerShot;
            blt.speed = bulletSpeed;

            blt = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, -angleSideShots) * Vector3.up), bulletsContainer.transform).GetComponent<Bullet>();
            blt.team = hittable.team;
            blt.damagePerShot = damagePerShot;
            blt.speed = bulletSpeed;

            SoundManager.Instance.Play("PlayerShoot", shootSoundVolume);

            OnShoot?.Invoke();
        }
    }

    public void Die()
    {
        GameManager.Instance.EndGame(false);
        ParticleStartupManager.Instance.Spawn(explosionPrefab, transform.position, bulletsContainer);

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
