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
        // Start the gamestate in play
        gameState = gamestate.play;

        // Keeps this object from being destroyed between scenes
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Call the pause handling helper method
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseHandling();
        }
	}

    /// <summary>
    /// Helper method to help pause the game
    /// </summary>
    public void PauseHandling()
    {

        // If you are playing currently
        if (gameState == gamestate.play)
        {
            // Pause the game
            Time.timeScale = 0;

            // Change the Game State to paused
            gameState = gamestate.paused;

            // unlock and show curser
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        // If you are paused resume the game
        else if (gameState == gamestate.paused)
        {
            // Resume the game
            Time.timeScale = 1;

            // Change the Game State to paused
            gameState = gamestate.play;

            // Lock and hide the curser
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

}
