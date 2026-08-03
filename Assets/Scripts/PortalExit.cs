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
         ScenePresistent scenePresistent=FindAnyObjectByType<ScenePresistent>();
        if (portalLayer==collision.gameObject.layer && !hasExited)
        {
            hasExited=true;
            Debug.Log("Before Reset");
            if (scenePresistent!=null)
            {
                scenePresistent.ResetScenePresist();
            }
            Debug.Log("After reset");
            int currentIndex= SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentIndex);
            Debug.Log("Scene loaded");
        }
    }
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("Collider");
    // }

}
