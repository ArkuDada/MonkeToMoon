using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Assign")]
    [SerializeField] private NewCollisionCheck newCollCheck;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRen;

    public SpriteRenderer SpriteRen => spriteRen;

    private void Update()
    {
        anim.SetBool("onGround", newCollCheck.OnGround);
        anim.SetBool("onPlatform", newCollCheck.OnPlatform);
        anim.SetBool("canMove", playerController.CanMove);
        anim.SetBool("isAttacking", playerController.IsAttacking);
    }

    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {
        bool state = (side != 1);
        SpriteRen.flipX = state;
    }
}