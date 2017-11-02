using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    private static int score;

    public int Score
    {
        get { return score; }
    }

	// Use this for initialization
	void Start ()
    {
        score = 0;

        // Keeps this object from being destroyed between scenes
        DontDestroyOnLoad(gameObject);
    }

    public void IncrementScore(int value)
    {
        score += value;
    }
    public void ResetScore()
    {
        score = 0;
    }

}
