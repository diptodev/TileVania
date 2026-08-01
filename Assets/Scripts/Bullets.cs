using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] float bulletSpeed=20f;
    Rigidbody2D myRigidBody;
    PlayerMovement player;
    float xSpeed;
    void Start()
    {
     myRigidBody=GetComponent<Rigidbody2D>();  
     player=FindAnyObjectByType<PlayerMovement>(); 
     xSpeed=player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidBody.linearVelocity=new Vector2(xSpeed,0f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Enemy"))
       {
        Debug.Log("You just attact enemy");
         Destroy(collision.gameObject);

       } 
      Destroy(gameObject );
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject,1f);
    } 
}
