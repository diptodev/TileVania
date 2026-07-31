using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;

    float runSpeed=10f;
    float jumpSpeed=20f;
    float climbSpeed=5f;
    float startGravityScale;
    Rigidbody2D playerRigidBody;
    Animator myAnimator;
    CapsuleCollider2D capsuleCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidBody=GetComponent<Rigidbody2D>();
         myAnimator=GetComponent<Animator>();
         capsuleCollider=GetComponent<CapsuleCollider2D>();
         startGravityScale=playerRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        flipSprite();
        LadderClimbing();
    }
   void OnMove(InputValue value)
    {
        moveInput=value.Get<Vector2>();
        if (Math.Abs(moveInput.x)>0)
        {
            myAnimator.SetBool("isRunning",true);
        } else if (moveInput.x==0)
        {
            myAnimator.SetBool("isRunning",false);
        }
    }
   void OnJump(InputValue value)
    {
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            playerRigidBody.linearVelocity+=new Vector2(0,jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVector2=new Vector2(moveInput.x*runSpeed,playerRigidBody.linearVelocity.y);
        playerRigidBody.linearVelocity=playerVector2; 
    }
    void flipSprite()
    {
        bool hasMoveHorizontal=Math.Abs(moveInput.x)>Mathf.Epsilon;
       if (hasMoveHorizontal)
       {
         transform.localScale=new Vector2(Mathf.Sign(moveInput.x),1f);
       }
    }
    void LadderClimbing()
    {
         if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            playerRigidBody.gravityScale=startGravityScale;
            myAnimator.SetBool("isClimbing",false);
            return;
        }
        playerRigidBody.gravityScale=0f;
        Vector2 climbLadder=new Vector2(playerRigidBody.linearVelocity.x,moveInput.y * climbSpeed);
        playerRigidBody.linearVelocity=climbLadder; 
         
            myAnimator.SetBool("isClimbing",true);
            }
}
