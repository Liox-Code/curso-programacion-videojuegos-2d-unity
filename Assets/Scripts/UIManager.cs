using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI finalScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateUIScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
    public void UpdateUIHealth(int newHealth)
    {
        healthText.text = newHealth.ToString();
    }
    public void UpdateUITime(int newTime)
    {
        timeText.text = newTime.ToString();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        finalScore.text = "SCORE:" + GameManager.Instance.Score;
    }
}
