using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public List<EraInfo> eraInfos;
    void Start()
    {
        PeriodManager.Instance.onEraChange.AddListener(UpdateEra);
    }

    public void UpdateEra()
    {
        foreach (EraInfo eraInfo in eraInfos)
        {
            if (eraInfo.era == PeriodManager.Instance.currentEra)
            {
                foreach (GameObject obj in eraInfo.objects)
                {
                    obj.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject obj in eraInfo.objects)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
[System.Serializable]
public struct EraInfo
{
    public Era era;
    public GameObject[] objects;
}
