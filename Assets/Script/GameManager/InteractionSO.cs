using System;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;


[CreateAssetMenu(fileName = "InteractionSO", menuName = "ScriptableObjects/Interaction")]
public class InteractionSO : ScriptableObject
{
    [SerializeField]
    public InteractionRequirement Requirement;

    public InteractionResultType InteractionResultType = InteractionResultType.Craft;
    
    [ShowIf("InteractionResultType", InteractionResultType.Craft )]
    public ItemType ResultItem;
    
    [ShowIf("InteractionResultType", InteractionResultType.Landmark)]
    public LandmarkType landmarkType;
    [ShowIf("InteractionResultType", InteractionResultType.Landmark)]
    public Era era;
    
    [ShowIf("InteractionResultType", InteractionResultType.Victory)]
    public VictoryType victoryType;

    [ShowIf("InteractionResultType", InteractionResultType.Resource)]
    public float CooldownTime = 0.0f;
}

[Serializable]
public class InteractionRequirement
{
    [SerializeField]
    public List<Requirement> Requirements = new List<Requirement>();

    public int Count => Requirements.Count;
}

[Serializable]
public class Requirement
{
    public ItemType Item;
    public int Amount;
    public float Droprate = 100.0f;
}

public enum InteractionResultType
{
    Craft,
    Landmark,
    Victory,
    Resource
}

public enum LandmarkType
{
    Spawn,River,Forest,Mountain,Mine
}

public enum VictoryType
{
    Stair,Ladder,Balloon,Rocket,Portal
}