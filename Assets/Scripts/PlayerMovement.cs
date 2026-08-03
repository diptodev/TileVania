using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;

    float runSpeed=10f;
    float jumpSpeed=20f;
    float climbSpeed=5f;
    float startGravityScale;
    bool isAlive=true;
float fadeDuration=1f;
    Rigidbody2D playerRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    SpriteRenderer spriteRenderer;
Vector2 diedVector=new Vector2(10f,20f);
[SerializeField] GameObject bullet;
[SerializeField] Transform gunTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidBody=GetComponent<Rigidbody2D>();
         myAnimator=GetComponent<Animator>();
         myBodyCollider=GetComponent<CapsuleCollider2D>();
         myFeetCollider=GetComponent<BoxCollider2D>();
         spriteRenderer=GetComponent<SpriteRenderer>();
        
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
      
       
        if (playerRigidBody.IsTouchingLayers(LayerMask.GetMask( "Enemy","KillWater","Hazards")) && isAlive)
        {
            
          Die();          
        }
    }
    void Die()
    {
        int currentLive=  FindAnyObjectByType<GameSession>().TakeLife();
          playerRigidBody.linearVelocity=diedVector;
          myAnimator.SetTrigger("isDead");
          isAlive=false; 
          if (currentLive==0)
          {
            Invoke("FadeoutPlayer",1f);
          }else
          {
            RestartGame(currentLive);
          }
 }
    void RestartGame(int currentLive)
    {
        if (currentLive==0)
        {
       FindAnyObjectByType<GameSession>().ResetGameSession() ;
       FindAnyObjectByType<ScenePresistent>().ResetScenePresist(); 
        }
        
    StartCoroutine(Reload());
    }
    IEnumerator Reload()
    {
        yield return new WaitForEndOfFrame();
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void OnAttack(InputValue inputValue)
    {
         if (!isAlive){return;}
        Instantiate(bullet,gunTransform.position,gunTransform.rotation);
    }
    void FadeoutPlayer()
    {
        StartCoroutine(Fadeout());
    }
    IEnumerator Fadeout()
    {
     
        float time=0f;
         bool visible=true;
        while (time<fadeDuration)
        {
            // Color c=spriteRenderer.color;
            // c.a=Mathf.Lerp(1f,0,time/fadeDuration);
            // spriteRenderer.color=c;
            // time+=0.1f;
            // yield return new WaitForSeconds(0.05f);
            // c.a=0f;
            // spriteRenderer.color=c;
            // yield return new WaitForSeconds(0.05f); 
        float fadeAlpha=Mathf.Lerp(1f,0,time/fadeDuration);
        Color c=spriteRenderer.color;
        c.a=visible?fadeAlpha:0f;
        visible=!visible;
        spriteRenderer.color=c;
        time+=0.1f;
        yield return new WaitForSeconds(0.1f);

        } 
        RestartGame(0);
         
    }
}
