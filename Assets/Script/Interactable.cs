using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityAction<bool> IsPlayerInRange;
    public bool IsInRange;

    private void Start()
    {
    }

    protected bool IsInteractable = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(IsInteractable && other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.attackController.SetInteractable(this);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(IsInteractable && other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.attackController.RemoveInteractable(this);
        }
    }

    public virtual void Interact()
    {
        throw new NotImplementedException();
    }
}