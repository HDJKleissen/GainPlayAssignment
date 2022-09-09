using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingleton<GameManager>
{
    public static event Action<int> OnScoreChange;
    public static event Action<float> OnTimerChange;
    public static event Action OnTimerEnd;
    public static event Action<bool> OnPausedChange;
    public static event Action<int> OnDifficultyChange;

    [SerializeField] float timePerCheckpoint;

    float timeRemaining, timePlayed;
    int currentScore, difficulty;
    string currentLevelName;
    bool gamePaused;

    void Start()
    {
        timeRemaining = timePerCheckpoint;
        timePlayed = 0;
    }

    void OnEnable()
    {
        Checkpoint.OnCheckpointReached += IncreaseTimer;
    }
    void OnDisable()
    {
        Checkpoint.OnCheckpointReached -= IncreaseTimer;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Pause"))
        {
            SetPaused(!gamePaused);
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            OnTimerChange?.Invoke(timeRemaining);
        }
        else
        {
            OnTimerEnd?.Invoke();
        }
    }

    public void SetPaused(bool paused)
    {
        gamePaused = paused;
        Time.timeScale = paused ? 0 : 1;
        OnPausedChange?.Invoke(paused);
    }

    public void IncreaseScore(int increase)
    {
        currentScore += increase;
        OnScoreChange?.Invoke(currentScore);
    }


    void IncreaseTimer()
    {
        timeRemaining += timePerCheckpoint;
        OnTimerChange?.Invoke(timeRemaining);
    }

    void LoadLevel(string levelName)
    {

    }

    public void ResetLevel()
    {
        SetPaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif

    }
}