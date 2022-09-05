using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : UnitySingleton<UIManager>
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        GameManager.OnScoreChange += UpdateScoreText;
    }
    private void OnDisable()
    {
        GameManager.OnScoreChange -= UpdateScoreText;
    }

    void UpdateScoreText(int score)
    {
        scoreText.SetText("Score: " + score);
    }
}
