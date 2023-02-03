using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [Header("Assign")]
    [SerializeField] private NewCollisionCheck newCollCheck;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AnimationController anim;
    [SerializeField] private ParticleController particleController;

    [Header("Movement")]
    [SerializeField] private float jumpForce;

    [SerializeField] private float coyoteTime;
    [SerializeField] private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float jumpBufferCounter;

    public void Jump(Vector2 direction)
    {
        if (newCollCheck.CanJump && !newCollCheck.IsJumping)
        {
            newCollCheck.CanJump = false;
            newCollCheck.IsJumping = true;

            particleController.JumpParticle.Play();

            playerController.Rigid2D.velocity = new Vector2(playerController.Rigid2D.velocity.x, 0) + direction * jumpForce;
        }
    }

    public void JumpCall()
    {
        if (newCollCheck.OnGround)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("jump");
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            if(jumpBufferCounter>=0) jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            Jump(Vector2.up);
            jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && playerController.Rigid2D.velocity.y > 0f)
        {
            coyoteTimeCounter = 0f;
        }
    }
}