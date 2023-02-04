using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptItemIcon : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    private SpriteRenderer sr;
    private TextMeshPro text;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        text = textObject.GetComponentInChildren<TextMeshPro>();
    }
    public void SetIcon(ItemType itemType, int Count)
    {
        sr.sprite = ItemDatabase.Instance?.Database[itemType].sprite;
        text.text = $"{Count}";
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