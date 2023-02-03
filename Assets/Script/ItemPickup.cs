using Sirenix.OdinInspector;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float popUpForce = 10f;
    public float flyForce = 5f;
    private Rigidbody2D rb;
    public ItemType itemType;
    private void Start()
    {
        Spawn();
    }
[Button]
    public void Spawn()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * popUpForce, ForceMode2D.Impulse);
        Vector2 flyDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddForce(flyDirection * flyForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Pickup {itemType}");
            Destroy(gameObject);
           InventoryManager.Instance.AddItem(itemType);
        }
    }
}