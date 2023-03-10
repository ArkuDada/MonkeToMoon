using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float attackRange = 3;
    public Vector2 direction = Vector2.right;
    public Interactable interactable;

    public void SetInteractable(Interactable inter)
    {
        if (interactable != inter)
        {
             if(interactable)
             {
                 interactable.IsInRange = false;
                 interactable.IsPlayerInRange?.Invoke(false);
             }
             interactable = inter;
             interactable.IsInRange = true; 
             interactable.IsPlayerInRange?.Invoke(true);
        }
       
    }
    public void RemoveInteractable(Interactable inter)
    {
        if (interactable == inter)
        {
            interactable = null;
        inter.IsInRange = false;
        inter.IsPlayerInRange?.Invoke(false);
        }
    }
    public void Fire()
    {
        if(interactable) interactable.Interact();
        /*//Debug.Log("Fire");
        RaycastHit2D[] hits;
        //hits = Physics2D.RaycastAll(transform.position, direction, attackRange);
        hits = Physics2D.CapsuleCastAll((Vector2)transform.position + direction * (attackRange / 2f),
            new Vector2(attackRange, 1), CapsuleDirection2D.Horizontal, 0, direction, 0);
        hits = hits.OrderBy(
            x => Vector2.Distance(this.transform.position, x.transform.position)
        ).ToArray();
        //Debug.Log("Attack" + hits.Length);
        Debug.DrawRay(transform.position, direction * attackRange, Color.red, 1f);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.TryGetComponent<Interactable>(out Interactable attackable))
            {
                attackable.Hit();
                break;
            }
        }*/
    }
}
