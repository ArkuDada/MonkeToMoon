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
    
    public bool isResultItem;
    public ItemType ResultItem;
    public int ResultAmount;

    void Interact()
    {
        if(Requirement.IsAble())
        {
            foreach(var r in Requirement.Requirements)
            {
                InventoryManager.Instance.RemoveItem(r.Item,r.Amount);

            }
            
            if(isResultItem)
            {
                InventoryManager.Instance.AddItem(ResultItem, ResultAmount);

            }
            else
            {
                //upgrade landmark
            }
        }
     
    }
}

[Serializable]
public class InteractionRequirement
{
    [SerializeField]
    public List<Requirement> Requirements = new List<Requirement>();

    //Imprement in object level
    public bool IsAble()
    {
        if(Requirements.Count == 0)
        {
            return true;
        }
        
        bool isAble = true;

        foreach(var r in Requirements)
        {
            if(!InventoryManager.Instance.HaveItems(r.Item, r.Amount))
            {
                isAble = false;
                break;
            }
        }
        return isAble;
    }
}

[Serializable]
public struct Requirement
{
    public ItemType Item;
    public int Amount;
}