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
    NormalItem = 0,
    Fiber,
    Rope,
    Cloth,
    Stone,
    Stone_Block,
    Rope_Ladder,
    Wood,
    Wood_Plank,
    Coal,
    Torch,
    Bronze_Ore,
    Bronze_Bar,
    Copper_Ore,
    Copper_Bar,
    Iron_Ore,
    Iron_Bar,
    Gold_Ore,
    Gold_Bar,
    Magnet_Ore,
    Fuel,
    Engine,
    Battery,
    KeyItem = 99,
    Bronze_Pickaxe,
    Bronze_Axe,
    Copper_Pickaxe,
    Copper_Axe,
    Meteor_Ore,
}