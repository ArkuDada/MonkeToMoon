using System.Collections;
using System.Collections.Generic;
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
        }

        Inv[type] += count;

        UpdateInv();
    }
    
    
    public void RemoveItem(ItemType type, int count)
    {
        if(HaveItems(type,count))
        {
            Inv[type] -= count;
            if(Inv[type] == 0)
            {
                Inv.Remove(type);
            }

            UpdateInv();
        }
    }

    public bool HaveItems(ItemType type, int count)
    {
        return Inv.ContainsKey(type) && Inv[type] >= count;
    }
}
