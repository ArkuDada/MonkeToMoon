using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PeriodManager : MonoBehaviour
{
    public static PeriodManager instance;
    public int year;
    public float yearDuration = 15;
    public Era currentEra;
    
    public UnityEvent onEraChange = new UnityEvent();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(YearCounter());
    }

    public void ChangeEra(Era era)
    {
        currentEra = era;
        onEraChange.Invoke();
    }
    public void Update()
    {
        
    }

    private IEnumerator YearCounter()
    {
        while (true)
        {
            yield return  new WaitForSeconds(yearDuration);
            year += 1;
            yield return null;
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