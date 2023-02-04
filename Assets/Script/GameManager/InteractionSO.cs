using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;


[CreateAssetMenu(fileName = "InteractionSO", menuName = "ScriptableObjects/Interaction")]
public class InteractionSO : ScriptableObject
{
    [SerializeField]
    public InteractionRequirement Requirement;
    public Era era;
    
    public bool isResultItem = false;
    public ItemType ResultItem;
    public int ResultAmount = 1;
}

[Serializable]
public class InteractionRequirement
{
    [SerializeField]
    public List<Requirement> Requirements = new List<Requirement>();

    public int Count => Requirements.Count;
}

[Serializable]
public struct Requirement
{
    public ItemType Item;
    public int Amount;
}