using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ring : MonoBehaviour
{
    public GameObject Player;
    private AudioSource m_AudioSource;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        m_AudioSource = GetComponentInParent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit it");
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        Player.GetComponent<Movement>().addRing();
        gameObject.SetActive(false);
        
    }
}
