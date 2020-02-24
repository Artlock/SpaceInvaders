using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Movement")]
public class MovementSettings : ScriptableObject
{
    public Vector3 direction = Vector3.right; // Where to go
    public float speed = 4f; // How fast to go
    public float distance = Mathf.Infinity; // How far to go

    public void Init(Vector3 dir, float spd, int dist)
    {
        direction = dir;
        speed = spd;
        distance = dist;
    }

    public static MovementSettings CreateInstance(Vector3 dir, float spd, int dist)
    {
        var data = ScriptableObject.CreateInstance<MovementSettings>();
        data.Init(dir, spd, dist);
        return data;
    }
}
