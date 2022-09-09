using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;  
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

    public int Difficulty { get; private set; }

    float timeRemaining, timePlayed;
    int currentScore;
    bool gamePaused, timerRunning;

    void Start()
    {
        timerRunning = true;
        timeRemaining = timePerCheckpoint;
        timePlayed = 0;
        Difficulty = 0;
    }

    void OnEnable()
    {
        Checkpoint.OnCheckpointReached += CheckpointReached;
        PlayerController.OnPlayerDeath += EndGame; 
    }
    void OnDisable()
    {
        Checkpoint.OnCheckpointReached -= CheckpointReached;
        PlayerController.OnPlayerDeath -= EndGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            SetPaused(!gamePaused);
        }

        if (timerRunning)
        {
            timePlayed += Time.deltaTime;
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


    void CheckpointReached()
    {
        timeRemaining += timePerCheckpoint;
        OnTimerChange?.Invoke(timeRemaining);

        Difficulty++;
        OnDifficultyChange?.Invoke(Difficulty);
    }

    void EndGame()
    {
        timerRunning = false;
        CreateLevelOverviewJSON();
    }

    public void ResetLevel()
    {
        SetPaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CreateLevelOverviewJSON()
    {
        GameInfo info = new GameInfo();
        info.timePlayed = timePlayed;
        info.timeJsonMade = DateTime.Now.Ticks;
        info.gameName = "blocky-pushy";
        info.score = currentScore;

        string jsonOutput = JsonConvert.SerializeObject(info);

        string destination = Application.persistentDataPath + "/" + info.gameName + "-" + info.timeJsonMade + ".json";
        Debug.Log("writing to " + destination);
        File.WriteAllText(destination, jsonOutput);
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

struct GameInfo
{
    public float timePlayed;
    public long timeJsonMade;
    public string gameName;
    public int score;
}