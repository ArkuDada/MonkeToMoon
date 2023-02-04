using Sirenix.OdinInspector;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float popUpForce = 10f;
    public float flyForce = 5f;
    private Rigidbody2D rb;
    [SerializeField][HideInInspector] private ItemType _type;
    public ItemType ItemType { get => _type; set => SetType(value); }
    public float delay;
    private void Start()
    {
        Spawn();
        delay = 0.5f;
    }
    [Button]

    private void SetType(ItemType type)
    {
         ItemDataSO data =ItemDatabase.Instance.Database[type];
         GetComponent<SpriteRenderer>().sprite = data.sprite;
         _type = type;
    }

    private void Update()
    {if(delay>0)delay-= Time.deltaTime;
        if (isPlayerInRange && delay <= 0)
        {
            PickUp();
        }
    }


    public void Spawn()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * popUpForce, ForceMode2D.Impulse);
        Vector2 flyDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddForce(flyDirection * flyForce, ForceMode2D.Impulse);
    }
bool isPlayerInRange = false;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
    void PickUp()
    {
        //Debug.Log($"Pickup {ItemType}");
        InventoryManager.Instance.AddItem(ItemType);
        Destroy(gameObject);
    }
}