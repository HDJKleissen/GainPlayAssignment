using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : UnitySingleton<UIManager>
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject pauseMenu;

    private void OnEnable()
    {
        GameManager.OnScoreChange += UpdateScoreText;
        GameManager.OnPausedChange += TogglePauseMenu;
    }
    private void OnDisable()
    {
        GameManager.OnScoreChange -= UpdateScoreText;
    }

    void UpdateScoreText(int score)
    {
        scoreText.SetText("Score: " + score);
    }

    void TogglePauseMenu(bool paused)
    {
        pauseMenu.SetActive(paused);
    }
}
