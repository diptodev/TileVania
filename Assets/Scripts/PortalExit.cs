using System.Collections;
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
            StartCoroutine(Load());
        }
    }
    IEnumerator Load()
    {
        yield return new WaitForEndOfFrame();
        int currentIndex= SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.sceneCountInBuildSettings==currentIndex)
        {
            SceneManager.LoadScene(0);
            Debug.Log("1st Scene loaded");
        }else
        {
            SceneManager.LoadScene(currentIndex+1);
            Debug.Log("Scene loaded");
        }
    }
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("Collider");
    // }

}
