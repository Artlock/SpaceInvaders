using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAction : CustomAction
{
    public MovementSettings settings;
    public Transform transform;

    public float moved = 0f; // How far we moved

    public MovementAction(MovementSettings s, Transform t)
    {
        settings = s;
        transform = t;
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        transform.position += settings.direction * settings.speed;
        moved += (settings.direction * settings.speed).magnitude;
    }

    public override void End()
    {
        
    }

    public override bool IsDone()
    {
        return moved >= settings.distance;
    }
}
