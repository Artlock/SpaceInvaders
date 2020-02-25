using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletsContainer;

    public float speed = 4f;
    public float cooldown = 1f;

    private Vector3 direction = Vector3.zero;
    private float lastShotTime = 0f;

    public void Update()
    {
        Movement();

        Shooting();
    }

    public void Movement()
    {
        direction = Vector3.zero;

        // Movement
        if (Input.GetKeyDown(KeyCode.Q)) // Left
        {
            direction = Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.D)) // Right
        {
            direction = Vector3.left;
        }

        transform.position += direction * speed * Time.deltaTime;
    }

    public void Shooting()
    {
        // Shooting
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > (lastShotTime + cooldown))
        {
            lastShotTime = Time.time;

            // Instantiate (Will shoot in awake)
            Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletsContainer.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            GameManager.Instance.EndGame(false);
        }
    }
}
