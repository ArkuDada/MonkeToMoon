using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour
{
    [Header("Assign")]
    [SerializeField] private NewCollisionCheck newCollCheck;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AnimationController anim;
    [SerializeField] private ParticleController particleController;
    [SerializeField] private LayerMask platform;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    public void Walk(Vector2 vec2)
    {
        var finalVec = new Vector2(moveSpeed * vec2.x, rb.velocity.y);
        /*//check if the player is on the ground and not on slope
        if (newCollCheck.OnGround && !newCollCheck.OnSlope && !newCollCheck.IsJumping && !newCollCheck.OnPlatform)
        {
            finalVec = new Vector2(moveSpeed * vec2.x, 0.0f);
        }
        //check if the player is on the ground and on slope
        else if (newCollCheck.OnGround && newCollCheck.OnSlope && newCollCheck.CanWalkOnSlope && !newCollCheck.IsJumping)
        {
            finalVec = new Vector2(moveSpeed * newCollCheck.SlopeNormalPerp.x * -vec2.x, moveSpeed * newCollCheck.SlopeNormalPerp.y * -vec2.x);
            //vec2 = Vector3.Project(vec2, newCollCheck.SlopeNormalPerp);
        }
        //if not grounded
        else if (!newCollCheck.OnGround)
        {
            
        }*/
        rb.velocity = finalVec;
    }
}