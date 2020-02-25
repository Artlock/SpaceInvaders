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
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)) // Left
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Right
        {
            direction += Vector3.right;
        }

        transform.position += direction * speed * Time.deltaTime;
    }

    public void Shooting()
    {
        // Shooting
        if (Input.GetKey(KeyCode.Space) && Time.time > (lastShotTime + cooldown))
        {
            lastShotTime = Time.time;

            // Instantiate (Will shoot in awake)
            Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletsContainer.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.attachedRigidbody?.GetComponent<Enemy>();

        if (enemy != null)
        {
            GameManager.Instance.EndGame(false);
        }
    }
}
