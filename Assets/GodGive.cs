using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GodGive : MonoBehaviour
{
    public static GodGive Instance;
    [SerializeField] private List<RandomGift> gifts;
    public int MeteorStartRate = 10;
    public int CurrentMRate = 10;
    public int MeteorUpRate = 10;

    [SerializeField] private int spawnRate;
    private int rateCount = 0;

    private bool spawnedM = false;
    private void Awake()
    {
        Instance = this;
        CurrentMRate = MeteorStartRate;
    }
    

    public void TrySpawnItem()
    {
        rateCount++;
        if (rateCount >= spawnRate)
        {
            rateCount = 0;
            bool doSpawnM = Random.Range(0, 100) <= CurrentMRate;
            if(doSpawnM && !spawnedM)
            {
                SpawnItem(ItemType.Meteor_Ore);
                spawnedM = true;
            }
            else
            {
                CurrentMRate += MeteorUpRate;
                float rand = Random.Range(0, 100);
                float total = 0;
                foreach (var gift in gifts)
                {
                    total += gift.percent;
                    if (rand <= total)
                    {
                        SpawnItem(gift.type);
                        break;
                    }
                }
            }
            {
                
            }
        }
    }

    private void SpawnItem(ItemType type)
    {
        GameObject pickup = Instantiate(GameManager.Instance.PickupPrefab, transform.position, Quaternion.identity);
        var pick = pickup.GetComponent<ItemPickup>();
        pick.ItemType = type;
    }
}

[Serializable]
struct RandomGift
{
    public ItemType type;
    public float percent;
}