using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;
    [SerializeField] private float lookDistance;

    private void Awake()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (PlayerController.Instance.Horizontal == 0)
        {
            Panning();
        }
        else
        {
            Vector3 camOffSet = Vector3.zero;
            CinemachineFramingTransposer transposer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            transposer.m_TrackedObjectOffset = camOffSet;
        }
    }

    private void Panning()
    {
        Vector3 camOffSet = Vector3.zero;

        if (PlayerController.Instance.Vertical == 1)
        {
            camOffSet = new Vector3(0, lookDistance, 0);
        }
        else if (PlayerController.Instance.Vertical == -1)
        {
            camOffSet = new Vector3(0, -lookDistance, 0);
        }

        CinemachineFramingTransposer transposer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        transposer.m_TrackedObjectOffset = camOffSet;
    }
}
