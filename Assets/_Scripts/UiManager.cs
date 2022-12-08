using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public static bool gameOver;
    public static bool isGameStarted;
    public GameObject gameOverPanel;
    public GameObject tapToPlay;
    public Text score_Text;
    public Text Yourscore_Text;
    public Text Highscore_Text;
    [SerializeField]
    private int Score = 0;
    public int highScore;
    public Text coin_Text;
    private int coin = 0;
    public GameObject panel;

    public bool isNewHighScore = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        //PlayerPrefs.SetInt("Score", 0);
        highScore = PlayerPrefs.GetInt("Score");

    }

    void Update()
    {

        //if (gameOver)
        //{
        //    Time.timeScale = 0;
        //    gameOverPanel.SetActive(true);
        //    if (isNewHighScore)
        //    {
        //        Debug.Log("New High Score");
        //    }
        //}
        if (SwipeManager.tap)
        {
            isGameStarted = true;
            tapToPlay.SetActive(false);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        gameOverPanel.SetActive(false);

    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }
    public void ScoreManager()
    {
        Score++;
        score_Text.text = Score.ToString();
        Yourscore_Text.text = Score.ToString();
        //score_Text.text = highScore.ToString();
        Highscore_Text.text = highScore.ToString();
        if (Score > highScore)
        {
            highScore = Score;
            PlayerPrefs.SetInt("Score", highScore);
            isNewHighScore = true;
        }
          
        Highscore_Text.text = PlayerPrefs.GetInt("Score", highScore).ToString();
    }

    public void NewHighScore()
    {
        if (isNewHighScore)
        {
            Debug.Log("New Score");
            //PlayerPrefs.SetInt("Score", highScore);
        }
    }

    public void CoinManager()
    {
        coin_Text.text = coin.ToString();
        coin++;
    }
    public void GameOver()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            NewHighScore();
            gameOverPanel.SetActive(true);
            //if (isNewHighScore)
            //{
            //    Debug.Log("New High Score");
            //}
        }

    }
}