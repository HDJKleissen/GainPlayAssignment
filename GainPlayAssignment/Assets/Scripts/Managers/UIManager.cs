using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : UnitySingleton<UIManager>
{
    [SerializeField] TextMeshProUGUI difficultyText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;

    private void OnEnable()
    {
        GameManager.OnDifficultyChange += UpdateDifficultyText;
        GameManager.OnScoreChange += UpdateScoreText;
        GameManager.OnTimerChange += UpdateTimer;
        GameManager.OnPausedChange += TogglePauseMenu;
        PlayerController.OnPlayerDeath += OpenGameOverMenu;
    }
    private void OnDisable()
    {
        GameManager.OnDifficultyChange -= UpdateDifficultyText;
        GameManager.OnScoreChange -= UpdateScoreText;
        GameManager.OnTimerChange -= UpdateTimer;
        GameManager.OnPausedChange -= TogglePauseMenu;
        PlayerController.OnPlayerDeath -= OpenGameOverMenu;
    }

    void UpdateDifficultyText(int difficulty)
    {
        difficultyText.SetText("Difficulty: " + difficulty);
    }

    void UpdateScoreText(int score)
    {
        scoreText.SetText("Score: " + score);
    }
    void UpdateTimer(float time)
    {
        timerText.SetText("Time: " + MiscUtil.FormatTime(time));
    }

    void TogglePauseMenu(bool paused)
    {
        pauseMenu.SetActive(paused);
    }

    void OpenGameOverMenu()
    {
        StartCoroutine(CoroutineHelper.DelaySeconds(() => gameOverMenu.SetActive(true), 1));
    }
}
