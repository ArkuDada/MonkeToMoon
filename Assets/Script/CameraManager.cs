using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public CinemachineVirtualCamera mainCamera;
    public void TeleportCamera(Vector3 position) {
       mainCamera.gameObject.SetActive(false);
         mainCamera.transform.position = position;
        StartCoroutine(UpdateCameraFrameLater());
    }
 
    private IEnumerator UpdateCameraFrameLater() {
        yield return null;
        mainCamera.gameObject.SetActive(true);
    }
}
