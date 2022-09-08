using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : UnitySingleton<UIManager>
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;

    private void OnEnable()
    {
        GameManager.OnScoreChange += UpdateScoreText;
        GameManager.OnPausedChange += TogglePauseMenu;
        PlayerController.OnPlayerDeath += OpenGameOverMenu;
    }
    private void OnDisable()
    {
        GameManager.OnScoreChange -= UpdateScoreText;
        GameManager.OnPausedChange -= TogglePauseMenu;
        PlayerController.OnPlayerDeath -= OpenGameOverMenu;
    }

    void UpdateScoreText(int score)
    {
        scoreText.SetText("Score: " + score);
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
