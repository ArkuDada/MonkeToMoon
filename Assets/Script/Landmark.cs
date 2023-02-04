using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Landmark : InteractableScripted
{
    private float interactTimer = 0;


// Start is called before the first frame update
    public override void Interact()
    {
        var InteractionDetail = interactionList[currentInteraction];

        if(InteractionDetail.InteractionResultType == InteractionResultType.Resource)
        {
            if(interactTimer > 0)
            {
                return;
            }
            
            foreach(var drop in InteractionDetail.Requirement.Requirements)
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
        else
        {
            base.Interact();
        }
    }

    private void Update()
    {
        if(interactTimer > 0)
        {
            interactTimer -= Time.deltaTime;
        }
    }
}