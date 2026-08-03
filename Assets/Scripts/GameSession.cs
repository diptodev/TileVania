using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI liveText;
    int liveRemaining=3;
    int score=000;

    private void Awake() {
        int currentObjects=FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (currentObjects>1)
        {
            Destroy(gameObject);
        }else
        {
            DontDestroyOnLoad(gameObject);
        }
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
        
        
    }
   public int TakeLife()
    {
        liveRemaining-=1;
        UpdateScore();
        ResetGameSession();
        return liveRemaining;
    }
  public void ResetGameSession()
    {
       
        if (liveRemaining==0)
        {
            FindAnyObjectByType<ScenePresistent>().ResetScenePresist();
            Destroy(gameObject);
        }
        
    }
}
 