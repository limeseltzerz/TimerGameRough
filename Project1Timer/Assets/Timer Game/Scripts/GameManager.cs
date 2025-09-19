using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float Timer = 100;
    public static GameManager Instance;
    public int score = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText; // score display
    public TextMeshProUGUI timerText; // timer display

    void Start()
    {
        UpdateScoreText();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;

        if (Timer < 0) Timer = 0;

        int minutes = (int)(Timer / 60);
        int seconds = (int)(Timer % 60);

        if (seconds < 10)
        {
            timerText.text = "Time: " + minutes + ":0" + seconds;
        }
        else
        {
            timerText.text = "Time: " + minutes + ":" + seconds;
        }

        if (Timer <= 0)
        {
            if (score < 15)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                SceneManager.LoadScene("GameWin!");
            }
        }
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
        Debug.Log("Your score is:" + score);
    }
    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}

