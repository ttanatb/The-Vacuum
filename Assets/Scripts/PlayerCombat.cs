﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour {
    private int pHealth;
    private uint pScore;
    private float timer;
    private float pInvunerability; //stores how long the player is invunerable after being hit
    private int pEnergy;
    public float pRechargeRate;
    public int pEnergyMax;
    private float rechargeTimer;
    // Use this for initialization
    void Start() {


        pHealth = 3;

        //make sure the max energy is set
        if (pEnergyMax <= 0)
        {
            pEnergyMax = 10;
        }
        pEnergy = pEnergyMax;       
        pScore = 0;
        rechargeTimer = 0;
    }

    // Update is called once per frame
    void Update() {
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
        if (rechargeTimer >= 1 / pRechargeRate && pEnergy != pEnergyMax)
        {
            pEnergy++;
            rechargeTimer = 0;
        }
        


    }

   /// <summary>
   /// Handling player collision here
   /// Health decrementing is handled enemey sider
   /// Currently does nothing
   /// </summary>
   /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {


        //picking up item       
        if (collision.gameObject.tag == "Item")
        {

        }
    }
    /// <summary>
    /// Handles the player taking damage
    /// </summary>
    /// <param name="damageAmount">Amount of Damage the player takes</param>
    public void TakeDamage(int damageAmount)
    {
        if (timer == 0) {
            pHealth -= damageAmount;
            timer += Time.deltaTime;
        }
        if (pHealth <= 0)
        {
            Debug.Log("You lose, NERD!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    /// <summary>
    /// Returns Player Health
    /// </summary>
    public int PHealth
    {   
       get{ return pHealth;}
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
    public int PEnergy
    {
       
        get { return pEnergy; }
        set { pEnergy = value; }

    }

   
    
}
