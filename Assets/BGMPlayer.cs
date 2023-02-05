using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> BGMs;
    
    // Start is called before the first frame update

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        audioSource.clip = BGMs[EraManager.Instance.currentEra];
        audioSource.Play();
    }


}
