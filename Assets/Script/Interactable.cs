using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public GameObject toggleObject;
    public InteractableType ite;
    public ItemType itemType;
    public GameObject pickupPrefab;
    private void Start()
    {
        toggleObject.SetActive(false);
    }

    public void Interact()
    {
        Debug.Log("Interact");
        switch (ite)
        {
            case InteractableType.Collectable:
                GameObject pickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity);
                var pick = pickup.GetComponent<ItemPickup>();
                pick.itemType = itemType;
                    
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.attackController.interactable = this;
            if (toggleObject != null)
            {
                toggleObject.SetActive(true);
            }

           
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(PlayerController.Instance.attackController.interactable == this) PlayerController.Instance.attackController.interactable = null;
            if (toggleObject != null)
            {
                toggleObject.SetActive(false);
            }
        }
    }
}
public enum InteractableType
{
    Collectable,
}
