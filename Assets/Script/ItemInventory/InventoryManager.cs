using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    private Dictionary<ItemType, int> Inv = new Dictionary<ItemType, int>();

    [HideInInspector]
    public UnityEvent<Dictionary<ItemType, int>> OnInvUpdate = new UnityEvent<Dictionary<ItemType, int>>();

    private void UpdateInv()
    {
        OnInvUpdate.Invoke(Inv);
    }
    
    public void AddItem(ItemType type, int count = 1)
    {
        if(!Inv.ContainsKey(type))
        {
            Inv.Add(type,0);
            
            //Inv = Inv.OrderBy(pair=>pair.Key).ToDictionary(pair=>pair.Key,pair=>pair.Value);
        }

        Inv[type] += count;
        //TODO:: add item limit to 99

        UpdateInv();
    }
    
    
    public void RemoveItem(ItemType type, int count)
    {
        if(HaveItems(type,count))
        {
            Inv[type] -= count;

            UpdateInv();
        }
    }

    public bool FoundItem(ItemType type)
    {
        return Inv.ContainsKey(type);
    }
    
    public bool HaveItems(ItemType type, int count)
    {
        return Inv.ContainsKey(type) && Inv[type] >= count;
    }
}
