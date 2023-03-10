using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int difficulty = 1;
    public static GameManager Instance;
    public int time = 30;
    public bool gameOver;

    [SerializeField]int score = 0;

    public int Score
    {
        get => score;
        set {
            score = value;
            UIManager.Instance.UpdateUIScore(score);
            if (score % 1000 == 0)
            {
                difficulty++;
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }   
    }

    private void Start()
    {
        StartCoroutine(CountDownRoutine());
        UIManager.Instance.UpdateUIScore(score);
    }

    IEnumerator CountDownRoutine()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            UIManager.Instance.UpdateUITime(time);
        }

        gameOver = true;
        UIManager.Instance.ShowGameOverScreen();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
