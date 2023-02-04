using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ParallaxObject
{
    public Transform transform;
    public float parallaxFactor;
}

public class ParallaxController : MonoSingleton<ParallaxController>
{
    [SerializeField]
    private List<ParallaxObject> parallaxObjects;

    [SerializeField]
    private Vector3 offset;
    
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;

    private void Start()
    {
        ResetCameraPosition();
    }
    
    public void ResetCameraPosition()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        for (int i = 0; i < parallaxObjects.Count; i++)
        {
            ParallaxObject parallaxObject = parallaxObjects[i];
            parallaxObject.transform.position = offset;
        }

    }

    private void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - previousCameraPosition;

        for (int i = 0; i < parallaxObjects.Count; i++)
        {
            ParallaxObject parallaxObject = parallaxObjects[i];
            Vector3 transformPosition = parallaxObject.transform.position;
            transformPosition.x += delta.x * parallaxObject.parallaxFactor;
            parallaxObject.transform.position = transformPosition;
        }

        previousCameraPosition = cameraTransform.position;
    }
}