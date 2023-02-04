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
    }

    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis",Mathf.Abs(x));
        anim.SetFloat("VerticalAxis", Mathf.Abs(y));
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