using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManagerScript : SingletonMonoBehaviour<GUIManagerScript>
{
    // Attributes
    // Reference to the player and GameManager
    private GameObject player;

    // Menu references
    private GameObject pauseMenu;

    // GUI Bars
    // Health Bar
    private int maxHealth;
    private int currentHealth;
    public Slider healthBar;

    // Energy Bar
    private float maxEnergy;
    private float currentEnergy;
    public Slider energyBar;
    public GameObject energyFill;
    private bool energyActive;


    //HurtFlash
    private HurtFlash hurtFlash;

	// Use this for initialization
	void Start ()
    {
        // Assign the reference to the player object
        player = GameObject.FindGameObjectWithTag("Player");
        pauseMenu = transform.Find("PauseMenu").gameObject;

        // Set max health and energy for guns
        maxHealth = player.GetComponent<PlayerCombat>().PHealth;
        maxEnergy = player.GetComponent<PlayerCombat>().pEnergyMax;
        healthBar.maxValue = maxHealth;
        energyBar.maxValue = maxEnergy;

        hurtFlash = GetComponentInChildren<HurtFlash>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Get current values for the GUI bars
        currentHealth = player.GetComponent<PlayerCombat>().PHealth;
        currentEnergy = player.GetComponent<PlayerCombat>().PEnergy;

        // Set the bars to those current values
        healthBar.value = currentHealth;
        energyBar.value = currentEnergy;

        // Check for activating the pause menu
        if (GameManagerScript.Instance && (GameManagerScript.Instance.CurrentGameState == GameState.Paused))
        { 
            pauseMenu.SetActive(true);
        }
        else if (GameManagerScript.Instance && (GameManagerScript.Instance.CurrentGameState == GameState.Play))
        {
            pauseMenu.SetActive(false);
        }

        EmptyBarCheck();

    }

    void EmptyBarCheck()
    {
        if(currentEnergy <= 0)
        {
            energyFill.SetActive(false);
        }
        else if(energyFill.activeSelf == false && currentEnergy>0)
        {
            energyFill.SetActive(true);
        }
    }

    public void FlashTakeDamage(float degree)
    {
        hurtFlash.FlashRed();
        hurtFlash.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, degree);
    }

    public void IncreaseMaxEnergyGUI()
    {
        energyBar.maxValue = player.GetComponent<PlayerCombat>().pEnergyMax;
    }

}
