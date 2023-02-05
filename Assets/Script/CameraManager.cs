using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private Animator am;
    public CinemachineVirtualCamera mainCamera;
    public float panSpeed = 50f;
    public float panTime = 1f;

   [SerializeField] private bool DoPan = true;
    private void Start()
    {
        if(DoPan)PanDown();

    }

    protected override void Awake()
    {
        base.Awake();
        am = GetComponent<Animator>();
        am.enabled = false;
    }
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
        am.enabled = false;

        transform.position = mainCamera.Follow.position;
        mainCamera.Follow = transform;
        FadeUI.Instance.StartCoroutine(PanUpCoroutine());
    }
    float panTimer;
    IEnumerator PanUpCoroutine()
    {
        while(panTimer < panTime)
        {
            transform.position += new Vector3(0, panSpeed * Time.deltaTime, 0);
            panTimer += Time.deltaTime;
            yield return null;
        }
        am.enabled = true;

    }

    [Button]
    public void PanDown()
    {
        am.enabled = false;

        transform.position = mainCamera.Follow.position + new Vector3(0, 15, 0);
        TeleportCamera(transform.position);
        mainCamera.Follow = transform;
        EraManager.Instance.camFreeze = true;
        StartCoroutine(PanDownCoroutine());

    }

    IEnumerator PanDownCoroutine()
    {
        while(transform.position.y > PlayerController.Instance.transform.position.y + 1)
        {
            transform.position -= new Vector3(0, panSpeed * 2 * Time.deltaTime, 0);
        ParallaxController.Instance.ResetCameraPosition();
            yield return null;
        }
        mainCamera.Follow = PlayerController.Instance.transform;
        EraManager.Instance.camFreeze = false;
        am.enabled = true;

    }


    public void DoZoomCam(bool zoomin)
    {
        if(zoomin)
        {
            am.Play("zoomin");
        }
        else
        {
            am.Play("zoomout");
        }
    }
}