using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManagerScript : MonoBehaviour
{
    // Attributes
    // Reference to the player and GameManager
    private GameObject player;
    private GameObject gameManager;

    // Menu references
    private GameObject pauseMenu;


    // GUI Bars
    // Health Bar
    private int maxHealth;
    private int currentHealth;
    public Slider healthBar;

    // Energy Bar
    private int maxEnergy;
    private int currentEnergy;
    public Slider energyBar;



	// Use this for initialization
	void Start ()
    {
        // Assign the reference to the player object
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("GameManager");
        pauseMenu = transform.Find("PauseMenu").gameObject;


        // Set max health and energy for guns
        maxHealth = player.GetComponent<PlayerCombat>().PHealth();
        maxEnergy = player.GetComponent<PlayerCombat>().PHealth();
        healthBar.maxValue = maxHealth;
        energyBar.maxValue = maxEnergy;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Get current values for the GUI bars
        currentHealth = player.GetComponent<PlayerCombat>().PHealth();
        currentEnergy = player.GetComponent<PlayerCombat>().PHealth();

        // Set the bars to those current values
        healthBar.value = currentHealth;
        energyBar.value = currentHealth;

        // Check for activating the pause menu
        if(gameManager.GetComponent<GameManagerScript>().GameState == gamestate.paused)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }

    }
}
