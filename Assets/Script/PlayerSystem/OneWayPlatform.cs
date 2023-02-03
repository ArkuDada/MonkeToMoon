using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [Header("Condition")]
    public bool OnDrop;
    private GameObject onewayPlatform;
    public GameObject OnewayPlatform => onewayPlatform;

    [SerializeField] private CapsuleCollider2D capCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            onewayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            onewayPlatform = null;
        }
    }

    public IEnumerator CollisionOff()
    {
        BoxCollider2D platformCollider = onewayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(capCollider, platformCollider);
        OnDrop = true;
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(capCollider, platformCollider, false);
        OnDrop = false;
    }
}