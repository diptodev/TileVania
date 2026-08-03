using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI liveText;
    int liveRemaining=3;
    int score=000;
private static GameSession instance;
    private void Awake() {
        // int currentObjects=FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        // if (currentObjects>1)
        // {
        //     Destroy(gameObject);
        // }else
        // {
        //     DontDestroyOnLoad(gameObject);
        // }
     if (instance !=null && instance !=this)
    {
        Destroy(gameObject);
        return;
    }
    instance=this;
    DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     UpdateScore();   
    }

  
    public void addScore(int score)
    {
        this.score+=score;
        UpdateScore();
    }
    void UpdateScore()
    {
        liveText.text=liveRemaining.ToString();
        scoreText.text=score.ToString();
        Debug.Log("Score");
        
    }
   public int TakeLife()
    {
        liveRemaining-=1;
        UpdateScore();
         
        return liveRemaining;
    }
  public void ResetGameSession()
    {
       
     Destroy(gameObject);
    }
    
}
 