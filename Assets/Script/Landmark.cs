using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : InteractableScripted
{
    private float cooldown = 0f;

    public GameObject pickupPrefab;

    // Start is called before the first frame update
    public override void Interact()
    {
        if(Time.timeSinceLevelLoad >= cooldown)
        {
            GameObject pickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity);
            var pick = pickup.GetComponent<ItemPickup>();

            pick.ItemType = interactionList[currentInteraction].ResultItem;

            cooldown = Time.timeSinceLevelLoad + .75f;
        }
    }
}