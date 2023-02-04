using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform hotbarParent;
    public Transform keyItemParent;
     List<Image> hotbarImg = new List<Image>();
     List<TMP_Text> itemCount = new List<TMP_Text>();
     List<Image> keyItemImg = new List<Image>();

    void Start()
    {
        hotbarImg = hotbarParent.GetComponentsInChildren<Image>(includeInactive: true).ToList();
        keyItemImg = keyItemParent.GetComponentsInChildren<Image>(includeInactive: true).ToList();
        foreach (var img in hotbarImg)
        {
            itemCount.Add(img.GetComponentInChildren<TMP_Text>());
        }

        InventoryManager.Instance.OnInvUpdate.AddListener(UpdateUI);
        UpdateUI(InventoryManager.Instance.GetInv());
    }

    private void UpdateUI(Dictionary<ItemType, int> inv)
    {
        foreach (var img in keyItemImg)
        {
            img.sprite = null;
            img.color = Color.clear;
        }
        foreach (var text in itemCount)
        {
            text.text = "";
        }
        List<KeyValuePair<ItemType, int>> normalItem = new List<KeyValuePair<ItemType, int>>();
        List<KeyValuePair<ItemType, int>> keyItem = new List<KeyValuePair<ItemType, int>>();
        
        foreach (KeyValuePair<ItemType, int> pair in inv)
        {
            if (pair.Key < ItemType.KeyItem)
            {
                if(pair.Value>0)normalItem.Add(pair);
            }
            else if(pair.Key > ItemType.KeyItem)
                keyItem.Add(pair);
        }
        for (var i = 0; i < hotbarImg.Count; i++)
        {
            
            var img = hotbarImg[i];
            var count = itemCount[i];
            
            count.text = "";
            Sprite s = null;
            if (normalItem.Count > i)
            {
                s = ItemDatabase.Instance.GetItemDataSO(normalItem[i].Key).sprite;
                count.text = normalItem[i].Value.ToString();
            }
            img.sprite = s;
            img.color = img != null ? Color.white : Color.clear;
        }

        int ki = 0;
        for (var i = 0; i < keyItem.Count; i++)
        {
            for (int j = 0; j < keyItem[i].Value; j++)
            {
                if (keyItemImg.Count <= ki)
                {
                    break;
                }
                var img = keyItemImg[ki];
                Sprite s = null;
                s = ItemDatabase.Instance.GetItemDataSO(keyItem[i].Key).sprite;
                img.sprite = s;
                img.color = Color.white;
                ki++;
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
