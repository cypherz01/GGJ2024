using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candle : MonoBehaviour
{
    GameObject player;
    GameObject ui;

    private void Awake()
    {
        ui = GameObject.Find("CrossHair");
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<Movement>().allRing)
        {
            GameEventsManager.instance.GoalReached();
            GameEventsManager.instance.ChangeCameraTarget(GameObject.Find("wallCam"));

            Destroy(ui);
            player.GetComponent<Movement>().canMove = false;
            player.GetComponent<AudioSource>().Stop();
            player.transform.position = new Vector3(65.4000015f, -79.1999969f, 4.53389549f);
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
