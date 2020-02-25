using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform enemyContainer;

    public bool moveRight = true;

    public float spawnDelayMax = 3f; // Easiest
    public float spawnDelayMin = 1f; // Hardest
    public float timeToMin = 30f;
    public AnimationCurve difficultyCurve;

    private float lastSpawnTime = 0f;

    private void Update()
    {
        if (Time.timeSinceLevelLoad > (lastSpawnTime + Mathf.Lerp(spawnDelayMax, spawnDelayMin, difficultyCurve.Evaluate(Time.timeSinceLevelLoad / timeToMin))))
        {
            // Instantiate (Will start moving in update)
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemyContainer.transform);
            Enemy enemyCmp = enemy.GetComponent<Enemy>();

            // Set movement direction (Default to right)
            List<CustomAction> movements = new List<CustomAction>();
            if (moveRight)
            {
                movements.Add(new MovementAction(enemyCmp.moveRight, enemy.transform));
            }
            else
            {
                movements.Add(new MovementAction(enemyCmp.moveLeft, enemy.transform));
            }

            enemyCmp.QueueActions(movements, true);

            lastSpawnTime = Time.timeSinceLevelLoad;
        }
    }
}
