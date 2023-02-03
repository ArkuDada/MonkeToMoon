using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PeriodManager : MonoBehaviour
{
    public static PeriodManager instance;
    public Era currentEra;
    
    public UnityEvent onEraChange = new UnityEvent();

    private void Awake()
    {
        instance = this;
    }
    public void ChangeEra(Era era)
    {
        currentEra = era;
        onEraChange.Invoke();
    }
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