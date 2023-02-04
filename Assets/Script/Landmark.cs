using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : InteractableScripted
{
    private float interactTimer = 0;
    public float cooldownCount = .75f;

    public ItemType itemType;

    public GameObject pickupPrefab;

// Start is called before the first frame update
    public override void Interact()
    {
        if (interactTimer <= 0)
        {
            GameObject pickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity);
            var pick = pickup.GetComponent<ItemPickup>();
            pick.ItemType = itemType;

            interactTimer = cooldownCount;
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
