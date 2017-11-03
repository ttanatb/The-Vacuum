using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    private static int score;
    private float timeInGame;


    public int Score
    {
        get { return score; }
    }
    public float TimeInGame
    {
        get { return timeInGame; }
    }

    // Use this for initialization
    void Start ()
    {
        score = 0;

        // Keeps this object from being destroyed between scenes
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(GameManagerScript.Instance && (GameManagerScript.Instance.CurrentGameState == GameState.Play))
        {
            timeInGame += Time.deltaTime;
        }
    }

    public void IncrementScore(int value)
    {
        score += value;
    }
    public void EndLevelIncrementScore()
    {
        // Give the user points dependent it takes for them to escape
        Debug.Log((int)(10000000 / (timeInGame * timeInGame)));
        IncrementScore((int)(10000000 / (timeInGame * timeInGame)));
    }

    public void ResetScore()
    {
        score = 0;
    }

}
