using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] private AnimationController anim;
    [SerializeField] private NewCollisionCheck newCollCheck;
    [SerializeField] private WalkController walkController;
    [SerializeField] private JumpController jumpController;
    [SerializeField] private OneWayPlatform oneWay;
    [SerializeField] private ParticleController particleController;
    public AttackController attackController;
    
    [Header("Assign")]
    [SerializeField] private Rigidbody2D rigid2D;
    [SerializeField] private CapsuleCollider2D capCollider;
    public Rigidbody2D Rigid2D => rigid2D;
    public CapsuleCollider2D CapCollider => capCollider;

    [Header("Movement")]
    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;
    [SerializeField] private float fallMultiplier = 7;
    [SerializeField] private float lowJumpMultiplier = 6;
                     private Vector2 controllerInput;
    public float Horizontal => horizontal;
    public float Vertical => vertical;
    
    [Header("Condition")]
    [SerializeField] private bool groundInteract;
    [SerializeField] private bool canMove = true;
    
    public static PlayerController Instance;
    public bool CanMove => canMove;
    public int Side = 1;


    [SerializeField] private bool isMainCharacter;
    public bool IsMainCharacter => isMainCharacter;
    public bool IsAttacking;
    public float attackCooldown;
    private bool canAttack;


    private void Start()
    {
        Instance = this;
        canAttack = true;
        IsAttacking = false;

       
        newCollCheck.SetColliderSize();
        oneWay.OnDrop = false;
    }

    private void Awake()
    {
        Instance = this;
    }
    private void FixedUpdate()
    {
        if (canMove && controllerInput.x != 0 && !newCollCheck.OnSlip)
        {
            walkController.Walk(controllerInput);
        }

        newCollCheck.GroundCheck();
        newCollCheck.SlipCheck();
        newCollCheck.SlopeCheck();
        newCollCheck.PlatformCheckJump();
        newCollCheck.PlatformCheckDrop();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        controllerInput = new Vector2(horizontal, vertical);

        anim.SetHorizontalMovement(horizontal, vertical, rigid2D.velocity.y);
        //Debug.Log($"ve; {rigid2D.velocity.x}");
        /*if (!newCollCheck.DropPlatform || !newCollCheck.OnSlip)
        {
            jumpController.JumpCall();
        }*/

        //if (IsAttacking && newCollCheck.OnGround || newCollCheck.OnPlatform)
        //{
        //    horizontal = 0f;
        //    vertical = 0f;
        //}

        if (Input.GetButtonDown("Fire1"))
        {
                particleController.AttackParticle.Play();
                attackController.direction = new Vector2(Side, 0);
                attackController.Fire();
                //StartCoroutine(DisableMovement());
        }

        if (newCollCheck.OnGround && !groundInteract && !newCollCheck.OnPlatform)
        {
            Side = anim.SpriteRen.flipX ? -1 : 1;
            particleController.GroundTouch();
            groundInteract = true;
        }

        if (!newCollCheck.OnGround && groundInteract)
        {
            groundInteract = false;
        }

        if (horizontal > 0)
        {
            Side = 1;
            anim.Flip(Side);
        }
        
        if (horizontal < 0)
        {
            Side = -1;
            anim.Flip(Side);
        }
        
        if (rigid2D.velocity.y < 0)
        {
            rigid2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        /*else if (rigid2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigid2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        */

        /*if (vertical<0)
        {
            if (oneWay.OnewayPlatform != null)
            {
                StartCoroutine(oneWay.CollisionOff());
            }
        }*/
        //code pete sux here
        /*if (horizontal == 0 && Input.GetButton("Jump"))
        {
            rigid2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rigid2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }*/
    }
    
    public IEnumerator DisableMovement()
    {
        //canMove = false;
        IsAttacking = true;
        canAttack = false;
        yield return new WaitForSeconds(0.75f);
        //canMove = true;
        canAttack = true;
        IsAttacking = false;
    }

}