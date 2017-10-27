﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
   
    public GameObject projectileMini;
    public GameObject projectileScatter;
    public GameObject projectileAuto;
    public float wFireRate;
    private float fireTimer;
    private GameObject player;
    private PlayerCombat playerCombat;
    private Weapons currentWeapon;
    
    /// <summary>
    /// Different kinds of weapons.
    /// </summary>
    enum Weapons { AutoProjector, MiniProjector, ScatterProjector}
    
    // Use this for initialization
    void Start()
    {
        player = gameObject.transform.parent.gameObject;
        playerCombat = player.GetComponent<PlayerCombat>();
        Random.InitState((int)Time.time);
        currentWeapon = Weapons.MiniProjector;

    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if the game is paused
        if (GameManagerScript.Instance && GameManagerScript.Instance.CurrentGameState != GameState.Play)
            return;
        
        //timer is checked to set fire rate
        //if the timer isn't running it can fire
        if (fireTimer == 0)
        {
            
            if (Input.GetMouseButton(0))
            {
                //checking ammo based on current weapon.
                if ((currentWeapon == Weapons.MiniProjector|| currentWeapon == Weapons.AutoProjector) && playerCombat.PEnergy >= 1) 
                    Fire();
                if (currentWeapon == Weapons.ScatterProjector && playerCombat.PEnergy >= 5)
                    Fire();
            }
            //if the timer is running, update time
        }
        else if (fireTimer > 0)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= 1.0f / wFireRate)
            {
                fireTimer = 0;
            }
        }
        //handling the switching of weapons from one to another
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = Weapons.MiniProjector;
            Debug.Log("Mini Projector Equiped");
            wFireRate = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = Weapons.ScatterProjector;
            Debug.Log("Scatter Projector Equiped");
            wFireRate = 1;
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = Weapons.AutoProjector;
            Debug.Log("Auto Projector Equiped");
            wFireRate = 5;
        }


    }
   /// <summary>
   /// handles the firing of the player's weapon
   /// makes a projectile reduces the energy and starts the firerate timer
   /// </summary>
    void Fire()
    {
        //make a projectile based on current weapon
        if (currentWeapon == Weapons.MiniProjector) 
        {
            GameObject.Instantiate(projectileMini, Camera.main.transform.position, Camera.main.transform.rotation);
            playerCombat.PEnergy--;
        }
        else if (currentWeapon == Weapons.ScatterProjector)
        {
            for (int i =0; i < 5; i++)
            {
                Quaternion projectileRotation = Camera.main.transform.rotation;
                projectileRotation.x += Random.Range(-0.50f, 0.50f);
                projectileRotation.y += Random.Range(-0.50f, 0.50f);

                GameObject.Instantiate(projectileScatter, Camera.main.transform.position, projectileRotation);
            }
            playerCombat.PEnergy-=5;
        }
        else if(currentWeapon == Weapons.AutoProjector){
            GameObject.Instantiate(projectileAuto, Camera.main.transform.position, Camera.main.transform.rotation);
            playerCombat.PEnergy--;
        }
        
        //start the timer
        fireTimer += Time.deltaTime;
    }
    
}
