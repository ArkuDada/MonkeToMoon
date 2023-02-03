using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    private bool canSwitch;
    private bool targetRoom;

    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] private CinemachineVirtualCamera roomCam;
    [SerializeField] private GameObject UIObject;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canSwitch)
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
            SetRoomCamera(!targetRoom);

    }
    private void SetRoomCamera(bool value)
    {
        targetRoom = value;
        playerCam.enabled = !targetRoom;
        roomCam.enabled = targetRoom;
        UIObject.SetActive(!targetRoom);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("MainPlayer") && !other.isTrigger)
        {
            canSwitch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("MainPlayer") && !other.isTrigger)
        {
            canSwitch = false;
            SetRoomCamera(false);
        }
    }
}
