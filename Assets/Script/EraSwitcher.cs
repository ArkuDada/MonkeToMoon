using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public List<EraInfo> eraInfos;
    void Start()
    {
        EraManager.Instance.onEraChange.AddListener(UpdateEra);
    }

    public void UpdateEra()
    {
        foreach (EraInfo eraInfo in eraInfos)
        {
            if (eraInfo.era == EraManager.Instance.CurrentEraData.era)
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
