using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    protected UnityAction<bool> IsPlayerInRange;

    private void Start()
    {
    }

    public virtual void Interact()
    {


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.attackController.interactable = this;
            IsPlayerInRange?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(PlayerController.Instance.attackController.interactable == this)
                PlayerController.Instance.attackController.interactable = null;
            IsPlayerInRange?.Invoke(false);
        }
    }

}