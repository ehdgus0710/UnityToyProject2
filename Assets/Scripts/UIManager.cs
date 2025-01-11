using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static readonly string scoreFormat = "SCORE: {0}";

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    private bool isPause = false;

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = string.Format(scoreFormat, newScore);
    }

    public void OnGameOverPanel(bool active)
    {
        gameOverPanel.SetActive(active);
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPause()
    {
        if (isPause)
            OffPause();
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPause = true;
        }
    }

    private void OffPause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }
}
