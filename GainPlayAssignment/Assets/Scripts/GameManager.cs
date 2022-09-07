using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingleton<GameManager>
{
    public static event Action<int> OnScoreChange;
    public static event Action<bool> OnPausedChange;

    int currentScore;
    string currentLevelName;
    bool gamePaused;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            SetPaused(!gamePaused);
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