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
            ChangeInteraction(1);
        }
        else if(scroll < 0 && prompt.activeSelf)
        {
            ChangeInteraction(-1);
        }
    }

    private void ChangeInteraction(int value)
    {
        currentInteraction += value;
            if (currentInteraction >= interactionList.Count)
            {
                currentInteraction = 0;
            }
            else if (currentInteraction < 0)
            {
                currentInteraction = interactionList.Count - 1;
            }
            prompt.GetComponent<InteractionPrompt>()
                .SetPromptDetail(interactionList[currentInteraction], CanCompleteInteract());
            FreezeTime(true);

    }
    public override void Interact()
    {
        if(CanCompleteInteract())
        {
            foreach(var r in interactionList[currentInteraction].Requirement.Requirements)
            {
                InventoryManager.Instance.RemoveItem(r.Item, r.Amount);
            }
            if(interactionList[currentInteraction].InteractionResultType == InteractionResultType.Craft)
            {
                InventoryManager.Instance.AddItem(interactionList[currentInteraction].ResultItem, 1);
                //TODO:: change to spawn item

            }
            else if(interactionList[currentInteraction].InteractionResultType == InteractionResultType.Landmark)
            {
                GameManager.Instance.CompleteLandMark(interactionList[currentInteraction].landmarkType);
                interactionList.RemoveAt(currentInteraction);
                
                
                Destroy(gameObject);
            }
            else if(interactionList[currentInteraction].InteractionResultType == InteractionResultType.Victory)
            {
                
            }
           
        }
    }
    public bool CanCompleteInteract()
    {
        if(interactionList[currentInteraction].InteractionResultType == InteractionResultType.Resource )
        {
            return true;
        }
        
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
        if(prompt.activeSelf == isActive || interactionList.Count == 0)
        {
            return;
        }

        prompt.SetActive(isActive);
        if(isActive)
        {
            prompt.GetComponent<InteractionPrompt>()
                .SetPromptDetail(interactionList[currentInteraction], CanCompleteInteract());
        }
    }

    protected void FreezeTime(bool isFreeze)
    {
        if(interactionList.Count == 0)
        {
            return;
        }
        EraManager.Instance.freezeTime = isFreeze && IsCrafting;
    }
}