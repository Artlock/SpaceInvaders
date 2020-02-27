using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hittable))]
public class Enemy : MonoBehaviour
{
    private Queue<CustomAction> actionList = new Queue<CustomAction>();
    private CustomAction currentAction = null;

    public GameObject bulletPrefab;
    public Transform bulletsContainer;
    public Hittable hittable;

    // Shooting related
    public float cooldown = 1f;
    public float bulletSpeed = 8f;
    public float damagePerShot = 10f;
    public float chanceToShoot = 0.1f;
    private float lastShotTime = 0f;

    [field: SerializeField] public MovementSettings moveRight { get; private set; }
    [field: SerializeField] public MovementSettings moveLeft { get; private set; }
    [field: SerializeField] public MovementSettings moveDown { get; private set; }

    // Add actions:
    private void Start()
    {
        actionList.Enqueue(new MovementAction(moveRight, transform));

        // Example of how to override at runtime
        //moveRight = MovementSettings.CreateInstance(dir, spd, dist);
    }

    private void Update()
    {
        Movement();

        Shooting();
    }

    private void OnDestroy()
    {
        // VFX here maybe?
    }

    private void Movement()
    {
        if (currentAction == null)
        {
            if (actionList.Count > 0)
            {
                currentAction = actionList.Dequeue();
                currentAction.Start();
            }
        }
        else
        {
            currentAction.Update();

            if (currentAction.IsDone())
            {
                currentAction.End();
                currentAction = null;
            }
        }
    }

    private void Shooting()
    {
        // Shooting
        if (Time.time > (lastShotTime + cooldown))
        {
            lastShotTime = Time.time;

            if (Random.Range(0f, 1f) > chanceToShoot) return;

            // Instantiate (Will shoot in awake)
            Bullet blt = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletsContainer.transform).GetComponent<Bullet>();
            blt.ignoreTeams.Add(hittable.team);
            blt.damagePerShot = damagePerShot;
            blt.speed = bulletSpeed;
            blt.Shoot(Vector3.down);
        }
    }

    public void Die()
    {
        SoundManager.Instance.Play("EnemyExplode");
        Destroy(gameObject);
    }

    public void QueueActions(List<CustomAction> actions, bool overrideActions = true)
    {
        if (overrideActions)
        {
            actionList.Clear();

            if (currentAction != null && currentAction.IsDone())
                currentAction.End();

            currentAction = null;
        }

        foreach (CustomAction act in actions)
            actionList.Enqueue(act);
    }
}
