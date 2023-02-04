using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScripted : Interactable
{
    
    public List<InteractionSO> interactionList;
    protected int currentInteraction;
    
    private bool IsCrafting => interactionList[currentInteraction].InteractionResultType == InteractionResultType.Craft;

    
    public GameObject prompt;
    // Start is called before the first frame update

    private void Awake()
    {

        IsPlayerInRange += ShowPrompt;
        IsPlayerInRange += FreezeTime;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll > 0 && prompt.activeSelf)
        {
            currentInteraction++;
            if(currentInteraction >= interactionList.Count)
            {
                currentInteraction = 0;
            }
            prompt.GetComponent<InteractionPrompt>().SetPromptDetail(interactionList[currentInteraction], TryCraft());
            FreezeTime(true);
        }
        else if(scroll < 0 && prompt.activeSelf)
        {
            currentInteraction--;
            if(currentInteraction < 0)
            {
                currentInteraction = interactionList.Count - 1;
            }
            prompt.GetComponent<InteractionPrompt>().SetPromptDetail(interactionList[currentInteraction], TryCraft());
            FreezeTime(true);
        }
    }

    public override void Interact()
    {
        if(TryCraft())
        {
            foreach(var r in interactionList[currentInteraction].Requirement.Requirements)
            {
                InventoryManager.Instance.RemoveItem(r.Item, r.Amount);
            }
            if(interactionList[currentInteraction].InteractionResultType == InteractionResultType.Craft)
            {
                InventoryManager.Instance.AddItem(interactionList[currentInteraction].ResultItem, 1);

            }
            else
            {
                //upgrade landmark
            }
        }
    }
    public bool TryCraft()
    {
        if(interactionList[currentInteraction].Requirement.Count > 0)
        {
            foreach(var r in interactionList[currentInteraction].Requirement.Requirements)
            {
                if(!InventoryManager.Instance.HaveItems(r.Item, r.Amount))
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
            prompt.GetComponent<InteractionPrompt>().SetPromptDetail(interactionList[currentInteraction], TryCraft());
        }
    }

    protected void FreezeTime(bool isFreeze)
    {
        EraManager.Instance.freezeTime = isFreeze && IsCrafting;
    }
}