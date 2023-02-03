using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [Header("Condition")]
    [SerializeField]private bool onGround;
    public bool OnGround => onGround;

    public bool OnWall => onRightWall||onLeftWall;

    [SerializeField] private bool onRightWall;
    public bool OnRightWall => onRightWall;
    [SerializeField] private bool onLeftWall;
    public bool OnLeftWall => onLeftWall;

    [SerializeField] private int wallSide;
    public int WallSide => wallSide;

    [Header("Layer")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Collision")]
    [SerializeField] private float collisionRadius = 0.25f;

    [Header("Offset")]
    [SerializeField] private Vector2 bottomOffset;
    [SerializeField] private Vector2 rightOffset;
    [SerializeField] private Vector2 leftOffset;

    private Color debugCollisionColor = Color.red;

    private void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer );

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer );
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] {bottomOffset};

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}
