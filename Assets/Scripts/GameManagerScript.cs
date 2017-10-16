using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enum for different Gamestates (play, pause, lose, win)
public enum gamestate { play, paused, gameOver, win};

public class GameManagerScript : MonoBehaviour
{


    // Attributes
    // Enum that tracks the current gameState
    private gamestate gameState;

    // Properties
    // Property for other scripts to access the current game state
    public gamestate GameState
    {
        get { return gameState; }
    }


	// Use this for initialization
	void Start ()
    {
        gameState = gamestate.play;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseHandling();
        }
	}

    /// <summary>
    /// Helper method to help pause the game
    /// </summary>
    void PauseHandling()
    {
        // If you are playing currently
        if(gameState == gamestate.play)
        {
            // Pause the game
            Time.timeScale = 0;

            // Change the Game State to paused
            gameState = gamestate.paused;
        }
        // If you are paused resume the game
        else if (gameState == gamestate.paused)
        {
            // Resume the game
            Time.timeScale = 1;

            // Change the Game State to paused
            gameState = gamestate.play;
        }

    }



}
