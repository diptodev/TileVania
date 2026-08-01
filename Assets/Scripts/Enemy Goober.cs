using UnityEngine;

public class EnemyGoober : MonoBehaviour
{
    Rigidbody2D myRigidBody2D;
    float movementSpeed=1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody2D=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody2D.linearVelocity=new Vector2(movementSpeed,0f);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        flipPlayer();
    }
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     int layerIndex=LayerMask.NameToLayer("Player");
    //     if (collision.gameObject.layer==layerIndex)
    //     {
           
    //         flipPlayer();
    //     }
    // }
    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     int layerIndex=LayerMask.NameToLayer("Player");
    //     if (collision.gameObject.layer==layerIndex)
    //     {
    //         Debug.Log("Enter");
    //         flipPlayer();
    //     }
    // }
    void flipPlayer()
    {
        transform.localScale=new Vector2(-(Mathf.Sign(myRigidBody2D.linearVelocity.x)),1f);
        movementSpeed=-movementSpeed;
    }
}
