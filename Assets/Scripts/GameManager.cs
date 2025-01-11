using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpawnerSystem spawnerSystem;
    [SerializeField] private UIManager uiManager;

    private bool isGameOver = false;
    private int Score = 0;

    private void Start()
    {
        spawnerSystem.SetSpawnTime(5);
        uiManager.UpdateScoreText(Score);
    }

    public void AddScore()
    {
        Score += 100;
        uiManager.UpdateScoreText(Score);
    }

    public void OnGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
    }

    public void OnQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Update()
    {
        if(isGameOver)
        {
            if(Keyboard.current.anyKey.isPressed || Mouse.current.press.isPressed)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
