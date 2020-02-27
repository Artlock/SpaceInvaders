using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Hittable))]
public class Enemy : MonoBehaviour
{
    private Queue<CustomAction> actionList = new Queue<CustomAction>();
    private CustomAction currentAction = null;

    public GameObject bulletPrefab;
    public Transform bulletsContainer;
    public Hittable hittable;

    public GameObject explosionPrefab;
    public GameObject enemyExplosionPrefab;

    // Shooting related
    public float cooldown = 1f;
    public float bulletSpeed = 8f;
    public float damagePerShot = 10f;
    public float chanceToShoot = 0.1f;
    private float lastShotTime = 0f;
    public float shootSoundVolume = 0.15f;

    public bool shootAtPlayer = false;

    [field: SerializeField] public MovementSettings moveRight { get; private set; }
    [field: SerializeField] public MovementSettings moveLeft { get; private set; }
    [field: SerializeField] public MovementSettings moveDown { get; private set; }

    public GameObject scoreGainText;
    public ScoreScript _scoreScript;
    // Add actions:
    private void Start()
    {
        actionList.Enqueue(new MovementAction(moveRight, transform));
        _scoreScript = GameObject.FindObjectOfType<ScoreScript>();
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
            Bullet blt = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(Vector3.forward, Vector3.down), bulletsContainer.transform).GetComponent<Bullet>();
            blt.team = hittable.team;
            blt.damagePerShot = damagePerShot;
            blt.speed = bulletSpeed;

            if (shootAtPlayer)
            {
                blt.transform.rotation = Quaternion.LookRotation(Vector3.forward, (Player.Instance.transform.position - blt.transform.position).normalized);
            }

            SoundManager.Instance.Play("PlayerShoot", shootSoundVolume);
        }
    }

    public void Die()
    {

        // incrementing the score
        ScoreScript.scoreValue += (50 * ScoreScript.multiplierValue);
        ScoreScript.killCount++;

        SoundManager.Instance.Play("EnemyExplode");
        ParticleStartupManager.Instance.Spawn(explosionPrefab, transform.position, bulletsContainer);
        ParticleStartupManager.Instance.Spawn(enemyExplosionPrefab, transform.position, bulletsContainer);

        GameObject tempText = Instantiate(scoreGainText.gameObject, transform.position, transform.rotation);
        tempText.transform.SetParent(_scoreScript.canvasWorld.transform,false);
        tempText.GetComponent<Text>().text = ScoreScript.targetKillCount * ScoreScript.multiplierValue + "";

        if (ScoreScript.killCount >= ScoreScript.targetKillCount)
        {
            GameObject tempMultiplierText = Instantiate(_scoreScript.multiplierText.gameObject, transform.position + _scoreScript.multiplierTextOffset, transform.rotation);
            tempMultiplierText.transform.SetParent(_scoreScript.canvasWorld.transform,false);
            tempMultiplierText.GetComponent<Text>().text = "x" + ScoreScript.multiplierValue+1;
            Debug.Log("MULTI");
        }
        //Debug.Log(tempText.GetComponent<Text>().text);
        

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
