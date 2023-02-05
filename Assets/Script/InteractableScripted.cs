using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractableScripted : Interactable
{

    public List<InteractionSO> interactionList;
    protected int currentInteraction;
private float interactTimer = 0;
    private bool IsCrafting => interactionList[currentInteraction].InteractionResultType == InteractionResultType.Craft;


    public InteractionPrompt prompt;
    // Start is called before the first frame update

    private void Awake()
    {

        IsPlayerInRange += ShowPrompt;
        IsPlayerInRange += FreezeTime;
    }
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll > 0 && prompt.gameObject.activeSelf)
        {
            ChangeInteraction(1);
        }
        else if(scroll < 0 && prompt.gameObject.activeSelf)
        {
            ChangeInteraction(-1);
        }
        if(interactTimer > 0)
        {
            interactTimer -= Time.deltaTime;
        }

        if (interactionList[currentInteraction].InteractionResultType == InteractionResultType.Resource&&IsInRange)
        {
            SetupPrompt();
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
            SetupPrompt();
            FreezeTime(true);

    }
    public override void Interact()
    {
        if(interactionList[currentInteraction].InteractionResultType == InteractionResultType.Resource)
        {
            if(interactTimer > 0)
            {
                return;
            }
                         
            foreach(var drop in interactionList[currentInteraction].Requirement.Requirements)
            {
                if(Random.Range(0.0f, 100.0f)
                   <= drop.Droprate)
                {
                    for (int i = 0; i < drop.Amount; i++)
                    {
                        GameObject pickup = Instantiate(GameManager.Instance.PickupPrefab, transform.position, Quaternion.identity);
                        var pick = pickup.GetComponent<ItemPickup>();
                        pick.ItemType = drop.Item;
                    }
             
                }
            }
             
            interactTimer = interactionList[currentInteraction].CooldownTime;
        }
        else if(CanCompleteInteract())
        {
            foreach(var r in interactionList[currentInteraction].Requirement.Requirements)
            {
                InventoryManager.Instance.RemoveItem(r.Item, r.Amount);
            }
            if(interactionList[currentInteraction].InteractionResultType == InteractionResultType.Craft)
            {
                GameObject pickup = Instantiate(GameManager.Instance.PickupPrefab, transform.position, Quaternion.identity);
                var pick = pickup.GetComponent<ItemPickup>();
                pick.ItemType = interactionList[currentInteraction].ResultItem;
                //TODO:: change to spawn item
            }
            else if(interactionList[currentInteraction].InteractionResultType == InteractionResultType.Landmark)
            {
                GameManager.Instance.CompleteLandMark(interactionList[currentInteraction].landmarkType);
                interactionList.RemoveAt(currentInteraction);

                if(interactionList.Count == 0)
                {
                    Destroy(gameObject);
                }
            }
            else if(interactionList[currentInteraction].InteractionResultType == InteractionResultType.Victory)
            {
                GameManager.Instance.GameVictory();
                Destroy(gameObject);
            }

        }
    }
    public bool CanCompleteInteract()
    {
        
        if(interactionList[currentInteraction].InteractionResultType == InteractionResultType.Resource)
        {
            return!(interactTimer > 0);
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
        if(prompt.gameObject.activeSelf == isActive || interactionList.Count == 0)
        {
            return;
        }

        prompt.gameObject.SetActive(isActive);
        if(isActive)
        {
            SetupPrompt();
        }
    }

    protected void FreezeTime(bool isFreeze)
    {
        if(interactionList.Count == 0)
        {
            return;
        }
        EraManager.Instance.craftFreeze = isFreeze && IsCrafting;
    }
    void SetupPrompt()
    {
            prompt.SetPromptDetail(interactionList[currentInteraction], CanCompleteInteract(),interactionList.Count, interactTimer);
    }
}