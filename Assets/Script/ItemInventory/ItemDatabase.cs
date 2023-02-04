using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoSingleton<ItemDatabase>
{
    public Dictionary<ItemType, ItemDataSO> Database = new Dictionary<ItemType, ItemDataSO>();

    [SerializeField] private List<ItemDataSO> itemDataSOs = new List<ItemDataSO>();

    protected override void Awake()
    {
        base.Awake();
        foreach(ItemDataSO idSO in itemDataSOs)
        {
            Database.Add(idSO.itemType,idSO);
        }
    }
    public ItemDataSO GetItemDataSO(ItemType itemType)
    {
        if (Database.ContainsKey(itemType))
        {
            return Database[itemType];
        }
        else
        {
            Debug.LogError("Item type not found in database: " + itemType);
            return Database[ItemType.Stone];
        }
    }
}
