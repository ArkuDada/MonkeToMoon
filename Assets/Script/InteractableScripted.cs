using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScripted : Interactable
{
    public InteractionSO interactionSO;
    // Start is called before the first frame update
    public override void Interact()
    {
        if(IsAble())
        {
            foreach(var r in interactionSO.Requirement.Requirements)
            {
                InventoryManager.Instance.RemoveItem(r.Item,r.Amount);

            }
            
            if(interactionSO.isResultItem)
            {
                InventoryManager.Instance.AddItem(interactionSO.ResultItem, interactionSO.ResultAmount);

            }
            else
            {
                //upgrade landmark
            }
        }
    }
    public bool IsAble()
         {
             if(interactionSO.Requirement.Count == 0)
             {
                 return true;
             }
             
             bool isAble = true;
     
             foreach(var r in interactionSO.Requirement.Requirements)
             {
                 if(!InventoryManager.Instance.HaveItems(r.Item, r.Amount))
                 {
                     isAble = false;
                     break;
                 }
             }
             return isAble;
         }
}
