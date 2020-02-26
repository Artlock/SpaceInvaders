using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bound : MonoBehaviour
{
    public bool right = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null || !other.attachedRigidbody.CompareTag("Enemy")) return;

        Enemy enemy = other.attachedRigidbody?.GetComponent<Enemy>();

        if (enemy != null)
        {
            List<CustomAction> movements = new List<CustomAction>();

            movements.Add(new MovementAction(enemy.moveDown, enemy.transform));
            if (right) movements.Add(new MovementAction(enemy.moveRight, enemy.transform));
            else movements.Add(new MovementAction(enemy.moveLeft, enemy.transform));

            enemy.QueueActions(movements, true);
        }
    }
}
