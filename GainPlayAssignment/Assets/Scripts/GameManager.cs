using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager>
{
    public static event Action<int> OnScoreChange;

    int currentScore;
    string currentLevelName;

    private void OnEnable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int increase)
    {
        currentScore += increase;
        OnScoreChange?.Invoke(currentScore);
    }

    void LoadLevel(string levelName)
    {

    }

    void ResetLevel()
    {

    }
}
