using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<ItemType, int> Inv;

    public UnityEvent<Dictionary<ItemType, int>> OnInvUpdate;

    private void UpdateInv()
    {
        OnInvUpdate.Invoke(Inv);
    }
    public void AddItem(ItemType type, int count)
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
