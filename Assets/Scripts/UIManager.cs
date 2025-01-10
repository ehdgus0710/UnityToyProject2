using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static readonly string scoreFormat = "SCORE: {0}";

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject gameOverPanel;

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = string.Format(scoreFormat, newScore);
    }

    public void SetActiveGameOverPaner(bool active)
    {
        gameOverPanel.SetActive(active);
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
