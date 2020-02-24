using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomAction
{
    public abstract void Start();

    public abstract void Update();

    public abstract void End();

    public abstract bool IsDone();
}
