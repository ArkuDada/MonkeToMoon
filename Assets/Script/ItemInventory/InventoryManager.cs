using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    public bool isDebug = false;
    
    private Dictionary<ItemType, int> Inv = new Dictionary<ItemType, int>();
public Dictionary<ItemType, int> GetInv() { return Inv; }
    [HideInInspector]
    public UnityEvent<Dictionary<ItemType, int>> OnInvUpdate = new UnityEvent<Dictionary<ItemType, int>>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void UpdateInv()
    {
        OnInvUpdate.Invoke(Inv);
    }
    
    public void AddItem(ItemType type, int count = 1)
    {
        if(!Inv.ContainsKey(type))
        {
            Inv.Add(type,0);
            
            //Inv = Inv.OrderBy(pair=>pair.Key).ToDictionary(pair=>pair.Key,pair=>pair.Value);
        }

        Inv[type] += count;
        Inv[type] = Mathf.Clamp(Inv[type],0,99);

        UpdateInv();
    }
    
    
    public void RemoveItem(ItemType type, int count)
    {
        if(isDebug) return;

        if(HaveItems(type,count))
        {
            Inv[type] -= count;
            if( Inv[type]  <= 0)
            {
                Inv[type] = 0;
            }
            UpdateInv();
        }
    }

    public bool FoundItem(ItemType type)
    {
        return Inv.ContainsKey(type);
    }
    /*Darkness blacker than black and darker than dark,
    I beseech thee, combine with my deep crimson.
    The time of awakening cometh.
    Justice, fallen upon the infallible boundary,
    appear now as an intangible distortions!
    I desire for my torrent of power a destructive force:
    a destructive force without equal!
    Return all creation to cinders,
    and come frome the abyss!
    Explosion!*/
    [Button]
    public void Kaboom()
    {
        InventoryKaboom(PlayerController.Instance.transform.position);
    }
    public void InventoryKaboom(Vector3 pos)
    {
        List<KeyValuePair<ItemType, int>> invList = new List<KeyValuePair<ItemType, int>>(Inv);
        for (int j = 0; j < invList.Count; j++)
        {
            KeyValuePair<ItemType, int> pair = invList[j];
            for (int i = 0; i < pair.Value; i++)
            {
                GameObject pickup = Instantiate(GameManager.Instance.PickupPrefab, pos + 
                    new Vector3(Random.Range(-3f, 3f), Random.Range(0, 3f))
                    , Quaternion.identity);
                var pick = pickup.GetComponent<ItemPickup>();
                pick.ItemType = pair.Key;
            }
            Inv[pair.Key] = 0;
        }
        UpdateInv();
    }
    public bool HaveItems(ItemType type, int count)
    {
        return isDebug || (Inv.ContainsKey(type) && Inv[type] >= count);
    }
}
