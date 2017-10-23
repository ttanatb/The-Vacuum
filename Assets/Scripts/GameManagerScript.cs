using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enum for different Gamestates (play, pause, lose, win)
public enum GameState { Play, Paused, GameOver, Win};

public class GameManagerScript : SingletonMonoBehaviour<GameManagerScript>
{
    // Attributes
    // Enum that tracks the current gameState
    private GameState gameState;

    // Properties
    // Property for other scripts to access the current game state
    public GameState CurrentGameState
    {
        get { return gameState; }
    }


	// Use this for initialization
	void Start ()
    {
        // Start the gamestate in play
        gameState = GameState.Play;

        // Keeps this object from being destroyed between scenes
        DontDestroyOnLoad(gameObject);

        // Turn off the curser on start
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        if (gameState == GameState.Play)
        {
            // Pause the game
            Time.timeScale = 0;

            // Change the Game State to paused
            gameState = GameState.Paused;

            // unlock and show curser
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        // If you are paused resume the game
        else if (gameState == GameState.Paused)
        {
            // Resume the game
            Time.timeScale = 1;

            // Change the Game State to paused
            gameState = GameState.Play;

            // Lock and hide the curser
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

}
