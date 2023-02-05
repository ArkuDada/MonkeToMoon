using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterArea : MonoBehaviour
{
    private PlayerWalkSound listenner;
    private void Start()
    {
         listenner = FindObjectOfType<DistantSoundListenner>().playerWalkSound;
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
           listenner.ChangeWalkSound(1);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            listenner.ChangeWalkSound(0);
        }
    }
}
