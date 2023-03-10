using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoSingleton<GameManager>
{
    [FormerlySerializedAs("landMarkSprites")] [SerializeField] private LandmarkStage[] landMark;
    public GameObject VictoryObject;
    public GameObject PickupPrefab;
    public Era VictoryEra = Era.Monke;
    public void Start()
    {
        OnLandMarkProgressUpdate();
        for(int i = 0; i < landMark.Length; i++)
        {
            for(int j = 0; j < landMark[i].objects.Count; j++)
            {
                landMark[i].objects[j].SetActive(false);
            }
        }
        if(VictoryObject)
            VictoryObject.SetActive(false);
    }
    public void CompleteLandMark(LandmarkType type)
    {
        foreach (LandmarkStage landmarkStage in landMark)
        {
            if (landmarkStage.type == type)
            {
                landmarkStage.Complete();
            }
        }
        OnLandMarkProgressUpdate();
    }

    private void OnLandMarkProgressUpdate()
    {
        if(landMark.Length == 0)
        {
            return;
        }
        bool allCompleted = true;
        for(int i = 0; i < landMark.Length; i++)
        {
            if (!landMark[i].completed)
            {
                allCompleted = false;
            }
        }

        if (allCompleted)
        {
            Debug.Log("All Landmark Completed");
            StartCoroutine(LandmarkCoroutine());
        }
    }
    [Button]
    public void LandmarkComplete()
    {
        StartCoroutine(LandmarkCoroutine());
    }
    IEnumerator LandmarkCoroutine()
    {
        EraManager.Instance.gameFreeze = true;
        yield return new WaitForSeconds(3);
        CameraManager.Instance.PanUp();
        FadeUI.Instance.shouldFadeToBlack = true;
        yield return new WaitForSeconds(2);
        EraManager.Instance.NextEra();
    }
    [Button]
    public void GameVictory()
    {
        EraManager.Instance.UpdateVictoryEra();
        StartCoroutine(VictoryCoroutine());
    }
    IEnumerator VictoryCoroutine()
    {
        EraManager.Instance.gameFreeze = true;
        if(VictoryObject)VictoryObject.SetActive(true);
        yield return new WaitForSeconds(2);
        Debug.Log("Game Victory");
        CameraManager.Instance.PanUp();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("End");
    }
}

[System.Serializable]
public class LandmarkStage
{
    public bool completed;
    public LandmarkType type;
    public List<GameObject> objects;
    public void Complete()
    {

        completed = true;
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }
}
