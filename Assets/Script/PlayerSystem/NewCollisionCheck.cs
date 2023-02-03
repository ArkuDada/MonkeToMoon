using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class NewCollisionCheck : MonoBehaviour
{
    [Header("Check Value")]
    [SerializeField] private float checkRadius;
    [SerializeField] private float detectRange;                 /* Platform */
    [SerializeField] private float checkSlopeDistance;
    [SerializeField] private float maxSlopeAngle;

    [Header("Friction")]
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;

    [SerializeField] private float slopeDownAngle;
    [SerializeField] private float slopeSideAngle;
    [SerializeField] private float lastSlopeAngle;

    [Header("Condition")]
    public bool OnPlatform;
    public bool DropPlatform;
    [SerializeField] bool onGround;
    public bool OnGround => onGround;
    public bool OnSlope;
    public bool IsJumping;
    [SerializeField] bool canWalkOnSlope;
    public bool CanWalkOnSlope => canWalkOnSlope;
    public bool CanJump;
    [SerializeField] bool onSlip;
    public bool OnSlip => onSlip;

   [Header("Slope Value")]
    [SerializeField] Vector2 slopeNormalPerp;
    public Vector2 SlopeNormalPerp => slopeNormalPerp;

    [Header("Assign")]
    [SerializeField] OneWayPlatform oneWay;
    [SerializeField] private Transform checkGround;
    [SerializeField] private Transform checkPlatform;
    [SerializeField] private Transform checkDrop;
    [SerializeField] private CapsuleCollider2D capCollider;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask platform;
    [SerializeField] private LayerMask slip;
    private Vector2 capColliderSize;

    public void SetColliderSize()
    {
        capColliderSize = playerController.CapCollider.size;
    }

    public void PlatformCheckDrop()
    {
        if (!oneWay.OnDrop)
        {
            DropPlatform = Physics2D.OverlapCircle(checkDrop.position, checkRadius, platform);
        }
    }

    public void PlatformCheckJump()
    {
        OnPlatform = Physics2D.OverlapCircle(checkPlatform.position, checkRadius, platform);
    }

    public void GroundCheck()
    {
        SetNull();

        onGround = Physics2D.OverlapCircle(checkGround.position, checkRadius, ground | platform);

        if (playerController.Rigid2D.velocity.y <= 0.0f && OnGround)
        {
            IsJumping = false;
            CanJump = true;
        }
         
        if (OnGround || canWalkOnSlope && !IsJumping)
        {
            CanJump = true;
        }
        else
        {
            CanJump = false;
        }
    }

    public void SlipCheck()
    {
        SetNull();

        onSlip = Physics2D.OverlapCircle(checkGround.position, checkRadius, slip);
    }

    public void SlopeCheck()
    {
        SetNull();

        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, capColliderSize.y / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }


    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D hit;

        Vector2 frontCheckPos = checkPos;
        Vector2 backCheckPos = checkPos;

        if (playerController.Side == 1)
        {
            frontCheckPos += new Vector2(0.05f, 0f);
        }
        else
        {
            backCheckPos += new Vector2(-0.05f, 0f);
        }

        RaycastHit2D slopeHitFront = Physics2D.CapsuleCast(capCollider.bounds.center,capColliderSize, CapsuleDirection2D.Vertical, 0f, transform.right*playerController.Side, checkSlopeDistance, ground);
        RaycastHit2D slopeHitBack = Physics2D.CapsuleCast(capCollider.bounds.center, capColliderSize, CapsuleDirection2D.Vertical, 0f, transform.right*-playerController.Side, checkSlopeDistance, ground);

        slopeSideAngle = 0.0f;
        OnSlope = false;
        slopeNormalPerp = Vector2.zero;
        slopeDownAngle = 0;

        hit = slopeHitFront;
        RaycastHit2D slopeHit = slopeHitFront? slopeHitFront : slopeHitBack;
        
        if (slopeHit)
        {
            OnSlope = true;
            hit = slopeHit;
            slopeSideAngle = Vector2.Angle(slopeHit.normal, Vector2.up);
        }

        if (OnSlope)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            Debug.DrawRay(hit.point, SlopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle )
        {
            canWalkOnSlope = false;
            
            if (slopeDownAngle == 90 || slopeSideAngle == 90)
            {
                OnSlope = false;
                canWalkOnSlope = true;
            }
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (playerController.Horizontal == 0.0f && !IsJumping && canWalkOnSlope && !onSlip)
        {
            playerController.Rigid2D.sharedMaterial = fullFriction;
        }
        else
        {
            playerController.Rigid2D.sharedMaterial = noFriction;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        if (playerController.Side == 1)
        {
            checkPos += new Vector2(0.05f, 0f);
        }
        else
        {
            checkPos += new Vector2(-0.05f, 0f);
        }

        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, checkSlopeDistance, ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGround.position, checkRadius);
        Gizmos.DrawWireSphere(checkPlatform.position, detectRange);
        Gizmos.DrawWireSphere(checkDrop.position, detectRange);
    }

    private void SetNull()
    {
        if (playerController.Rigid2D == null)
        {
            return;
        }
    }
}