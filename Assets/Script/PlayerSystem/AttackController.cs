using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float attackRange = 3;
    public Vector2 direction = Vector2.right;

    public void Fire()
    {
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
            /*if (hits[i].transform.gameObject.TryGetComponent<Attackable>(out Attackable attackable))
            {
                attackable.Hit();
                break;
            }*/
        }
    }
}
