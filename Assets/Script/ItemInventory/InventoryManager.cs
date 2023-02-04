using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    public bool isDebug = false;
    
    private Dictionary<ItemType, int> Inv = new Dictionary<ItemType, int>();
public Dictionary<ItemType, int> GetInv() { return Inv; }
    [HideInInspector]
    public UnityEvent<Dictionary<ItemType, int>> OnInvUpdate = new UnityEvent<Dictionary<ItemType, int>>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

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
        Inv[type] = Mathf.Clamp(Inv[type],0,99);

        UpdateInv();
    }
    
    
    public void RemoveItem(ItemType type, int count)
    {
        if(isDebug) return;

        if(HaveItems(type,count))
        {
            Inv[type] -= count;
            if( Inv[type]  <= 0)
            {
                Inv[type] = 0;
            }
            UpdateInv();
        }
    }

    public bool FoundItem(ItemType type)
    {
        return Inv.ContainsKey(type);
    }
    
    public bool HaveItems(ItemType type, int count)
    {
        return isDebug || (Inv.ContainsKey(type) && Inv[type] >= count);
    }
}
