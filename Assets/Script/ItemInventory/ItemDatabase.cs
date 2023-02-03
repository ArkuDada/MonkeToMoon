using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoSingleton<ItemDatabase>
{
    public Dictionary<ItemType, ItemDataSO> Database;

    [SerializeField] private List<ItemDataSO> itemDataSOs;
    private void Awake()
    {
        foreach(ItemDataSO idSO in itemDataSOs)
        {
            Database.Add(idSO.itemType,idSO);
        }
    }
}
