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
}

public enum Era
{
    Monke =0,
    Stone,
    Ore,
    Modern,
    Future,
}