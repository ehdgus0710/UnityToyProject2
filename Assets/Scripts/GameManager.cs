using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpawnerSystem spawnerSystem;
    [SerializeField] private UIManager uiManager;

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
}
