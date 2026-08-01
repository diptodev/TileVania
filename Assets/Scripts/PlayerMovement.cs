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
    bool isAlive=true;
    Rigidbody2D playerRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
Vector2 diedVector=new Vector2(10f,20f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidBody=GetComponent<Rigidbody2D>();
         myAnimator=GetComponent<Animator>();
         myBodyCollider=GetComponent<CapsuleCollider2D>();
         myFeetCollider=GetComponent<BoxCollider2D>();
         startGravityScale=playerRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive){return;}
         Run();
          flipSprite();
         LadderClimbing();
    }
   void OnMove(InputValue value)
    {
         if (!isAlive){return;}
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
         if (!isAlive){return;}
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
          
         if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer==LayerMask.NameToLayer("Enemy") && isAlive)
        {
            playerRigidBody.linearVelocity=diedVector;
            myAnimator.SetTrigger("isDead");
            isAlive=false;           
        }
    }
}
