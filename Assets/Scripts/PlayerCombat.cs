using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    private int pHealth;
    private int pHealthMax;
    private uint pScore;
    private float timer;
    private float pInvunerability; //stores how long the player is invunerable after being hit
    private float pEnergy;
    public float pRechargeRate;
    public int pEnergyMax;
    private float rechargeTimer;

    private AudioSource pickUpAudio;
    private Vector2 pitch = new Vector2(0.8f, 1.2f);



    // Use this for initialization
    void Start()
    {
        pHealthMax = 5;
        pHealth = 5;

        //make sure the max energy is set
        if (pEnergyMax <= 0)
        {
            pEnergyMax = 10;
        }
        pEnergy = pEnergyMax;
        pScore = 0;
        pInvunerability = 1.0f;
        rechargeTimer = 0;
        timer = 0;


        pickUpAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if the game is paused
        if (GameManagerScript.Instance && GameManagerScript.Instance.CurrentGameState != GameState.Play)
            return;


        //just checking time here
        if (timer >= pInvunerability)
        {
            timer = 0;
        }
        else if (timer > 0)
        {
            timer += Time.deltaTime;
           
        }
        rechargeTimer += Time.deltaTime;


        // Testing new ways for recharge for the gun
        pEnergy += pRechargeRate * Time.deltaTime;
        if (pEnergy > pEnergyMax)
        {
            pEnergy = pEnergyMax;
        }
       

    }

    /// <summary>
    /// Handling player collision here
    /// Health decrementing is handled enemey sider
    /// Currently does nothing
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
           

            // Scene 2 is the win screen
            SceneManager.LoadScene(2);

            // Change the Game State to Menu
            GameManagerScript.Instance.CurrentGameState = GameState.Menu;

            // unlock and show curser
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        //Health pick up   
        if (other.gameObject.tag == "HealthPickUp")
        {
            pHealth += 1;
            //incase we exceed the max
            if (PHealth > pHealthMax)
            {
                pHealth = pHealthMax;
            }
            Destroy(other.gameObject);
            Debug.Log("Health Pick up");
            PlayPickUpAudio();

        }

        //energy pick up
        if (other.gameObject.tag == "EnergyPickUp")
        {
            pEnergyMax += 3;
            pEnergy = pEnergyMax;
            Destroy(other.gameObject);
            Debug.Log("Energy Pick up");
            PlayPickUpAudio();

        }


    }

    /// <summary>
    /// Handles the player taking damage
    /// </summary>
    /// <param name="damageAmount">Amount of Damage the player takes</param>
    public void TakeDamage(int damageAmount)
    {
        if (timer == 0)
        {
            pHealth -= damageAmount;
            timer += Time.deltaTime;
            
        }
        if (pHealth <= 0)
        {
            

            // Scene 1 is the death screen
            SceneManager.LoadScene(1);

            // Change the Game State to Menu
            GameManagerScript.Instance.CurrentGameState = GameState.Menu;

            // unlock and show curser
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void TakeDamage(int damageAmount, Vector3 damageSrcPos)
    {
        Vector3 vec = damageSrcPos - transform.position;
        vec.y = transform.forward.y;
        float angle = Vector3.Angle(transform.forward, vec);
        Vector3 cross = Vector3.Cross(transform.forward, vec);
        if (cross.y > 0)
            angle = -angle;

        GUIManagerScript.Instance.FlashTakeDamage(angle);
        TakeDamage(damageAmount);
    }

    public void GainEnergy(float energyAmt)
    {
        pEnergy += energyAmt;
        if (pEnergy > pEnergyMax)
        {
            pEnergy = pEnergyMax;
        }

        PlayPickUpAudio();
    }

    private void PlayPickUpAudio()
    {
        if (pickUpAudio.isPlaying)
            pickUpAudio.Stop();

        pickUpAudio.pitch = Random.Range(pitch.x, pitch.y);
        pickUpAudio.Play();
    }


    /// <summary>
    /// Returns Player Health
    /// </summary>
    public int PHealth
    {
        get { return pHealth; }
    }
    /// <summary>
    /// Returns Player score
    /// </summary>
    public uint PScore
    {
        get { return pScore; }
        set { pScore = value; }
    }

    /// <summary>
    /// Returns and modifies the players engery
    /// </summary>
    public float PEnergy
    {

        get { return pEnergy; }
        set { pEnergy = value; }

    }



}
