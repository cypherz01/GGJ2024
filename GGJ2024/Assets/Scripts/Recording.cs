using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recording 
{
    public ReplayObject replayObject {  get; private set; }

    private Queue<replayData> originalQueue;
    private Queue<replayData> replayQueue;

    public Recording(Queue<replayData> recordingQueue)
    {
        this.originalQueue = new Queue<replayData>(recordingQueue);
        this.replayQueue = new Queue<replayData>(recordingQueue);

    }

    public void RestartFromBeginning()
    {
        this.replayQueue = new Queue<replayData>(originalQueue);

    }

    public bool PlayNextFrame()
    {
        if(replayObject == null)
        {
            Debug.Log("replay object was null");
        }

        bool hasMoreFrames = false;
        if(replayQueue.Count != 0)
        {
            replayData data = replayQueue.Dequeue();
            replayObject.SetDataForFrame(data);
            hasMoreFrames = true;
        }
        return hasMoreFrames;
    }

    public void InstantiateReplayObject(GameObject replayObjectPrefab)
    {
        if (replayQueue.Count != 0)
        {
            replayData startingData = replayQueue.Peek();
            this.replayObject = Object.Instantiate(replayObjectPrefab, startingData.position,Quaternion.identity)
                .GetComponent<ReplayObject>(); 
        }
    }

    public void DestroyReplayObjectIfExists()
    {
        if(replayObject != null)
        {
           Object.Destroy(replayObject.gameObject);
        }
    }
    
}
