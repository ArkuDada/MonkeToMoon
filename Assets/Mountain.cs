using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : MonoBehaviour
{
    public SpriteRenderer sr;
    
    
    public float fadeSpeed = 1f;
    
    public float fadeValue = 1.0f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Color srColor = sr.color;
        srColor.a = Mathf.Lerp(srColor.a, fadeValue, fadeSpeed * Time.deltaTime);
        sr.color = srColor;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            fadeValue = 0.0f;
            CameraManager.Instance.DoZoomCam(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        
        if(col.CompareTag("Player"))
        {
            fadeValue = 1.0f;
            CameraManager.Instance.DoZoomCam(false);

        }
    }
}
