using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ring : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Awake()
    {
        m_AudioSource = GetComponentInParent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit it");
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        gameObject.SetActive(false);
        
    }
}
