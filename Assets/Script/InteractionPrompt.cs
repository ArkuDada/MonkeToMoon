using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    public List<PromptItemIcon> Icons;
    public float iconSize = 1f;
    public float iconPadding = 0.5f;
    public GameObject scrollPrompt;
    public GameObject interactPrompt;
    
    [SerializeField]
    private GameObject iconPrefab;
    private void OnEnable()
    {
//        throw new NotImplementedException();
    }
    public void SetPromptDetail(InteractionSO interactionDetail, bool canInteract,int interactCount)
    {
        interactPrompt.SetActive(canInteract);
        scrollPrompt.SetActive(interactCount > 1);
        if(Icons.Count > 0)
        {
            foreach(var icon in Icons)
            {
                Destroy(icon.gameObject);
            }
            Icons.Clear();
        }

        bool isResource = interactionDetail.InteractionResultType == InteractionResultType.Resource;
        
        int count = interactionDetail.Requirement.Requirements.Count;
        //TODO:: count += isResource ? 1 : 0;
        float offset = (((iconSize * count) + (iconPadding * (count - 1))) / 2.0f) - iconPadding;
        foreach(var requirement in interactionDetail.Requirement.Requirements)
        {
            GameObject iconObject = Instantiate(iconPrefab, transform);
            iconObject.transform.localScale = new Vector3(iconSize, iconSize, 1.0f);
            iconObject.transform.localPosition = new Vector3(-offset + (Icons.Count * (iconSize + iconPadding)), 0, 0);

            PromptItemIcon icon = iconObject.GetComponent<PromptItemIcon>();
            if(icon != null)
            {
                icon.SetIcon(requirement.Item, requirement.Amount , 
                    isResource ? (int)requirement.Droprate : -1, InventoryManager.Instance.FoundItem(requirement.Item), canInteract);
            }

            Icons.Add(icon);

        }

        if(isResource)
        {
            //TODO:: show clock icon
        }
    }
}