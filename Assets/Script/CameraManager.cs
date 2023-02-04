using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public CinemachineVirtualCamera mainCamera;
    public float panSpeed = 50f;
    public float panTime = 1f;
    public void TeleportCamera(Vector3 position)
    {
        //        mainCamera.OnTargetObjectWarped(GameObject.FindWithTag("Player").transform, position);

        mainCamera.gameObject.SetActive(false);
        mainCamera.transform.position = position;
        StartCoroutine(UpdateCameraFrameLater());
    }

    private IEnumerator UpdateCameraFrameLater()
    {
        yield return null;
        mainCamera.gameObject.SetActive(true);
    }
[Button]
    public void Reset()
    {
        mainCamera.Follow = PlayerController.Instance.transform;
        panTimer = 0;
    }
    [Button]
    public void PanUp()
    {
        transform.position = mainCamera.Follow.position;
        mainCamera.Follow = transform;
        StartCoroutine(PanUpCoroutine());
    }
    float panTimer;
    IEnumerator PanUpCoroutine()
    {
        while (panTimer < panTime)
        {
            transform.position += new Vector3(0, panSpeed* Time.deltaTime, 0);
            panTimer += Time.deltaTime;
            yield return null;
        }
    }
}