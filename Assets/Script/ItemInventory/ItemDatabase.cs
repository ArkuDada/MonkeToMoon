using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoSingleton<ItemDatabase>
{
    public Dictionary<ItemType, ItemDataSO> Database = new Dictionary<ItemType, ItemDataSO>();

    [SerializeField] private List<ItemDataSO> itemDataSOs = new List<ItemDataSO>();
    private void Awake()
    {
        foreach(ItemDataSO idSO in itemDataSOs)
        {
            Database.Add(idSO.itemType,idSO);
        }
    }
}
