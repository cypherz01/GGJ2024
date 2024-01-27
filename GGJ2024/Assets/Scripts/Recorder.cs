using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    [Header("Prefab to Instantiate")]
    [SerializeField] private GameObject replayObjectPrefab;
    public Queue<replayData>recordingQueue{ get; private set; }

    private Recording recording;

    private bool isDoingReplay = false;
    private void Awake()
    {
        recordingQueue = new Queue<replayData>();
    }
    private void StartReplay()
    {
        isDoingReplay = true;
        recording = new Recording(recordingQueue);
        recordingQueue.Clear();
        recording.InstantiateReplayObject(replayObjectPrefab);
    }

    public void RecordReplayFrame(replayData data)
    {
        recordingQueue.Enqueue(data);
    }

    private void RestartReplay()
    {
        isDoingReplay = true;
        recording.RestartFromBeginning();

    }

    private void Start()
    {
       GameEventsManager.instance.onGoalReached += OnGoalReached;
       GameEventsManager.instance.onRestartLevel+= OnRestartLevel;
        
    }
    private void OnDestroy() {
       GameEventsManager.instance.onGoalReached -= OnGoalReached;
       GameEventsManager.instance.onRestartLevel -= OnRestartLevel;
    }

    private void OnGoalReached()
    {
        StartReplay();
    }

    private void OnRestartLevel()
    {
        Reset();
    }

    private void Update()
    {
        if (!isDoingReplay)
        {
            return;
        }

        bool hasMoreFrames = recording.PlayNextFrame();

        if (!hasMoreFrames)
        {
            RestartReplay();
        }
    }

    private void Reset()
    {
        isDoingReplay= false;   
        recordingQueue.Clear();
        recording.DestroyReplayObjectIfExists();
        recording = null;  

    }
}
