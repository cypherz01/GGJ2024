using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Transform to Follow")]
    [SerializeField] private Transform targetTransform;

    [Header("Configuration")]
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    [SerializeField] private Vector2 offset = Vector2.zero;

    private Transform originalTargetTransform;

    private void Start()
    {
        originalTargetTransform = targetTransform;
        // subscribe to events
        GameEventsManager.instance.onChangeObject+= OnChangeCameraTarget;
        GameEventsManager.instance.onRestartLevel += OnRestartLevel;
    }

    private void OnDestroy()
    {
        // unsubscribe from events
        GameEventsManager.instance.onChangeObject -= OnChangeCameraTarget;
        GameEventsManager.instance.onRestartLevel -= OnRestartLevel;
    }

    

    private void OnChangeCameraTarget(GameObject newTarget)
    {
        GameObject.Find("Player").GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
        GameObject.Find("wallCam").SetActive(true);
    }

    private void OnRestartLevel()
    {
        gameObject.GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
    
    }
}
