using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bound : MonoBehaviour
{
    public bool right = true;
    public bool isDefeat = false;

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            if (isDefeat) GameManager.Instance.EndGame(false);
            else
            {
                List<CustomAction> movements = new List<CustomAction>();

                movements.Add(new MovementAction(enemy.moveDown, enemy.transform));
                if (right) movements.Add(new MovementAction(enemy.moveRight, enemy.transform));

                enemy.QueueActions(movements, true);
            }
        }
    }
}
