using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptItemIcon : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private TextMeshPro textAmount;
    [SerializeField] private TextMeshPro textPercent;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

    }
    public void SetIcon(ItemType itemType, int Count,int Percent,bool isFound,bool isInteractable)
    {
        bool showPersent =( Percent != -1 && Percent < 100.0f);
        sr.sprite =isFound||itemType<ItemType.CraftItem ?  ItemDatabase.Instance?.Database[itemType].sprite : ItemDatabase.Instance.missingItem;
        textAmount.text = $"{Count}";
        
        textPercent.gameObject.SetActive(showPersent);
        textPercent.text = $"{Percent}{(showPersent ? "%" : "" )}";
        Color srColor = sr.color;
        srColor.a = isInteractable ? 1.0f : 0.7f;
        sr.color = srColor;
    }

}