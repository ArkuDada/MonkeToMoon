using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DistantSoundSource : MonoBehaviour
{
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private DistantSoundListenner listenner;
    [SerializeField]private float MaxDistant = 10.0f;
    [SerializeField]private float MinDistant = 1.0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        listenner = FindObjectOfType<DistantSoundListenner>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = listenner.Position - transform.position;
        float distance = direction.magnitude;
        float volume = Mathf.InverseLerp(MaxDistant, MinDistant, distance);
        audioSource.volume = volume;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MaxDistant);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, MinDistant);
    }
}
