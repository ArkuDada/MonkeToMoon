using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "ScriptableObjects/Item Data")]
public class ItemDataSO : ScriptableObject
{
    public ItemType itemType;
    public Sprite sprite;
}

public enum ItemType{
    Wood,
    Stone,
}