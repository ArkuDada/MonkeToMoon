using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    protected UnityAction<bool> IsPlayerInRange;
    protected bool IsInRange;

    private void Start()
    {
    }

    protected bool IsInteractable = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(IsInteractable && other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.attackController.interactable = this;
            IsPlayerInRange?.Invoke(true);
            IsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(IsInteractable && other.gameObject.CompareTag("Player"))
        {
            if(PlayerController.Instance.attackController.interactable == this)
                PlayerController.Instance.attackController.interactable = null;
            IsPlayerInRange?.Invoke(false);
            IsInRange = false;
        }
    }

    public virtual void Interact()
    {
        throw new NotImplementedException();
    }
}