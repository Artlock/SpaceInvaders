using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Queue<CustomAction> actionList = new Queue<CustomAction>();
    public CustomAction currentAction = null;

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

    private void OnDestroy()
    {
        // VFX here maybe?
    }

    public void QueueActions(List<CustomAction> actions, bool overrideActions = true)
    {
        if (overrideActions)
        {
            actionList.Clear();

            if (currentAction.IsDone())
                currentAction.End();

            currentAction = null;
        }

        foreach (CustomAction act in actions)
            actionList.Enqueue(act);
    }
}
