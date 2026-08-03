using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalExit : MonoBehaviour
{
    bool hasExited=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    { 
    
        int portalLayer=LayerMask.NameToLayer("Player");
         
        if (portalLayer==collision.gameObject.layer && !hasExited)
        {
           
            hasExited=true;
           int currentIndex= SceneManager.GetActiveScene().buildIndex;
           SceneManager.LoadScene(currentIndex+1);
        }
    }
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("Collider");
    // }

}
