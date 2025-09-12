using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour  
{
    public float Timer = 30;
    public static GameManager Instance;
    public int score = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateScoreText();
    }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            SceneManager.LoadScene("GameOver");
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
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ItemToPickUp"))
        {
            score++;
        }
    }
}
