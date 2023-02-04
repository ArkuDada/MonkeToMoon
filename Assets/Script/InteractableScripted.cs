using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScripted : Interactable
{
    public InteractionSO interactionSO;
    public GameObject prompt;
    // Start is called before the first frame update

    private void Awake()
    {
        IsPlayerInRange += ShowPrompt;
    }
    public override void Interact()
    {
        if(TryCraft())
        {
            foreach (var r in interactionSO.Requirement.Requirements)
            {
                InventoryManager.Instance.RemoveItem(r.Item, r.Amount);
            }
            if(interactionSO.InteractionResultType == InteractionResultType.Craft)
            {
                InventoryManager.Instance.AddItem(interactionSO.ResultItem, 1);

            }
            else
            {
                //upgrade landmark
            }
        }
    }
    public bool TryCraft()
         {
             if(interactionSO.Requirement.Count > 0)
             {
                 foreach(var r in interactionSO.Requirement.Requirements)
                 {
                     if (!InventoryManager.Instance.HaveItems(r.Item, r.Amount))
                     {
                         return false;
                     }
                 }
                 
             }
             return true;
         }

    protected void ShowPrompt(bool isActive)
    {
        if(prompt.activeSelf == isActive)
        {
            return;
        }
        
        prompt.SetActive(isActive);
        if(isActive)
        {
            prompt.GetComponent<InteractionPrompt>().SetPromptDetail(interactionSO,TryCraft());
        }
    }
}
