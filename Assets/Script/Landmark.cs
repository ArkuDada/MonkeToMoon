using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : Interactable
{

    public ItemType itemType;

    public GameObject pickupPrefab;

    // Start is called before the first frame update
    public override void Interact()
    {
        GameObject pickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        var pick = pickup.GetComponent<ItemPickup>();
        pick.itemType = itemType;
    }
}
