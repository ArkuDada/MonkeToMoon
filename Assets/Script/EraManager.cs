using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class EraManager : MonoSingleton<EraManager>
{
    public int year;
    float timer;
    public float yearDuration = 1;
    public List<EraData> eraDatas;
    public int currentEra;
    public EraData CurrentEraData => eraDatas[currentEra];
    public UnityEvent onEraChange = new UnityEvent();

    public int birthYear;
    public int Age => year - birthYear;
    
    private void Start()
    {
        
    }

    public void ChangeEra(Era era)
    {
        currentEra = eraDatas.FindIndex(x => x.era == era);
        onEraChange.Invoke();
        Die();
    }
    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > yearDuration)
        {
            timer = 0;
            year += 1;
            if (Age > CurrentEraData.lifeSpan)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        birthYear = year;
    }

    [Button]
    public void NextEra()
    {
        if(currentEra<eraDatas.Count) currentEra++;
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