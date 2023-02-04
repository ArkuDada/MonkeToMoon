using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PeriodManager : MonoSingleton<PeriodManager>
{
    public int year;
    float timer;
    public float yearDuration = 15;
    public Era currentEra;
    
    public UnityEvent onEraChange = new UnityEvent();

    private void Start()
    {
    }

    public void ChangeEra(Era era)
    {
        currentEra = era;
        onEraChange.Invoke();
    }
    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > yearDuration)
        {
            timer = 0;
            year += 1;
        }
    }
    
    [Button]
    public void NextEra()
    {
        if(currentEra<Era.Future) currentEra++;
        onEraChange.Invoke();
    }
}

public enum Era
{
    Monke =0,
    Stone=1,
    Ore=2,
    Modern=3,
    Future=4,
}