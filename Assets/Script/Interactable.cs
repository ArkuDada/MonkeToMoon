using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public GameObject toggleObject;
    private void Start()
    {
        toggleObject.SetActive(false);
    }

    public virtual void Interact()
    {
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
