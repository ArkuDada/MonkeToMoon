using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "ScriptableObjects/Item Data")]
public class ItemDataSO : ScriptableObject
{
    public ItemType itemType;
    public Sprite sprite;
}

public enum ItemType
{
    ResourceItem = 0,
    Wood,
    Stone,
    Fiber,
    Coal,
    Fuel,
    Copper_Ore, 
    Iron_Ore,
    Gold_Ore,
    Magnet_Ore,
    CraftItem = 99,
    Rope,
    Cloth,
    Stone_Block,
    Rope_Ladder,
    Wood_Plank,
    Torch,
    Bronze_Ore,
    Bronze_Bar,
    Copper_Bar,
    Iron_Bar,
    Gold_Bar,
    Engine,
    Battery,
    KeyItem = 999,
    Bronze_Pickaxe,
    Bronze_Axe,
    Copper_Pickaxe,
    Copper_Axe,
    Meteor_Ore,
}