using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
   
    public GameObject projectile;
    public float wFireRate;
    private float fireTimer;
    private GameObject player;
    private PlayerCombat playerCombat;
    
    // Use this for initialization
    void Start()
    {
        player = gameObject.transform.parent.gameObject;
        playerCombat = player.GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if the game is paused
        if (GameManagerScript.Instance && GameManagerScript.Instance.CurrentGameState == GameState.Paused)
            return;
        
        //timer is checked to set fire rate
        //if the timer isn't running it can fire
        if (fireTimer == 0)
        {
            if (Input.GetMouseButton(0) && playerCombat.PEnergy > 0)
            {
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
        

    }
   /// <summary>
   /// handles the firing of the player's weapon
   /// makes a projectile reduces the energy and starts the firerate timer
   /// </summary>
    void Fire()
    {
        //make a projectile
        GameObject.Instantiate(projectile, Camera.main.transform.position, Camera.main.transform.rotation);
        playerCombat.PEnergy--;
        //start the timer
        fireTimer += Time.deltaTime;
    }

}
