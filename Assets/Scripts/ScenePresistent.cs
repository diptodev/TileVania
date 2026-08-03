using UnityEngine;

public class ScenePresistent : MonoBehaviour
{
    
private static ScenePresistent instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake() {
    int totalObjects=FindObjectsByType<ScenePresistent>(FindObjectsSortMode.None).Length;
    if (1<totalObjects)
    {
        Destroy(gameObject);
    }else
    {
        DontDestroyOnLoad(gameObject);
    }
    // if (instance !=null && instance !=this)
    // {
    //     Destroy(gameObject);
    //     return;
    // }
    // instance=this;
    // DontDestroyOnLoad(gameObject);
}
  public void ResetScenePresist()
    {        Debug.Log("Reset Called");
         Destroy(gameObject);
    }
}
