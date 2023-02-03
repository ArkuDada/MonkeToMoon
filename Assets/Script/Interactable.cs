using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool destroyOnHit;
    public UnityEvent onHit = new UnityEvent();
    
    public virtual void Hit()
    {
        if(destroyOnHit) Destroy(gameObject);
        onHit.Invoke();
        
    }
}
