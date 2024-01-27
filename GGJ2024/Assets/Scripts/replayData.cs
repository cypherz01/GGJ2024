using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class replayData
{
    public Vector3 position { get; private set; }
    public Quaternion rotation { get; private set; }

    public replayData(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
