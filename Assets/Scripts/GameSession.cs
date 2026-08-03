using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI liveText;
    int liveRemaining=3;
    int score=000;

    private void Awake() {
        int currentObjects=FindObjectsByType<GameSession>().Length;
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

    // Update is called once per frame
    void Update()
    {
        
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
}
