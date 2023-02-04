using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    public List<PromptItemIcon> Icons;
    public float iconSize = 1f;
    public float iconPadding = 0.5f;

    [SerializeField]
    private GameObject iconPrefab;
    private void OnEnable()
    {
//        throw new NotImplementedException();
    }
    public void SetPromptDetail(InteractionSO interactionDetail, bool canInteract)
    {
        if(Icons.Count > 0)
        {
            foreach(var icon in Icons)
            {
                Destroy(icon.gameObject);
            }
            Icons.Clear();
        }

        int count = interactionDetail.Requirement.Requirements.Count;
        float offset = ((iconSize * count) + (iconPadding * (count - 1))) / 2.0f;
        foreach(var requirement in interactionDetail.Requirement.Requirements)
        {
            GameObject iconObject = Instantiate(iconPrefab, transform);
            iconObject.transform.localScale = new Vector3(iconSize, iconSize, 1.0f);
            iconObject.transform.localPosition = new Vector3(-offset + (Icons.Count * (iconSize + iconPadding)), 0, 0);

            PromptItemIcon icon = iconObject.GetComponent<PromptItemIcon>();
            if(icon != null)
            {
                icon.SetIcon(requirement.Item, requirement.Amount);
                icon.IsItemFound(InventoryManager.Instance.FoundItem(requirement.Item));
                icon.IsInteractable(canInteract);
            }

            Icons.Add(icon);

        }
    }
}