using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayObject : MonoBehaviour
{
   public void SetDataForFrame(replayData data)
    {
        this.transform.position = data.position;
        this.transform.rotation = data.rotation;
    }
}
