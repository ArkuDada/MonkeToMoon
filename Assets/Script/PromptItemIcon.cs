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
    public void SetIcon(ItemType itemType, int Count,int Percent = -1)
    {
        bool showPersent =( Percent != -1 && Percent < 100.0f);
        sr.sprite = ItemDatabase.Instance?.Database[itemType].sprite;
        textAmount.text = $"{Count}";
        
        textPercent.gameObject.SetActive(showPersent);
        textPercent.text = $"{Percent}{(showPersent ? "%" : "" )}";

    }

    public void IsItemFound(bool isFound)
    {
        if(isFound)
        {
            sr.color = Color.green;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    public void IsInteractable(bool isInteractable)
    {
        Color srColor = sr.color;
        if(isInteractable)
        {
            srColor.a = 1.0f;
        }
        else
        {
            srColor.a = 0.7f;
        }

        sr.color = srColor;
    }
}