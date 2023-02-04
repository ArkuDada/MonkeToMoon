using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class EraManager : MonoSingleton<EraManager>
{
    public int year;
    public float timer;
    public bool freezeTime;
    public float yearDuration = 1;
    public List<EraData> eraDatas;
    public int currentEra;
    public EraData CurrentEraData => eraDatas[currentEra];
    public UnityEvent onEraChange = new UnityEvent();

    
    
    private void Start()
    {
        
    }

    public void ChangeEra(Era era)
    {
        currentEra = eraDatas.FindIndex(x => x.era == era);
        onEraChange.Invoke();
        LifeManager.Instance.Die();
    }
    public void Update()
    {
        if(!freezeTime)timer += Time.deltaTime;
        if (timer > yearDuration)
        {
            timer = 0;
            year += 1;
            LifeManager.Instance.AgeCheck();
        }
    }

    

    [Button]
    public void NextEra()
    {
        if(currentEra<eraDatas.Count-1) currentEra++;
        onEraChange.Invoke();
    }
}
[System.Serializable]
public struct EraData
{
    public Era era;
    public float lifeSpan;
}
public enum Era
{
    Monke =0,
    Stone=1,
    Ore=2,
    Modern=3,
    Future=4,
}