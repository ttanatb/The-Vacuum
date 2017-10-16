using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {
    private int pHealth;
    private uint pScore;
    private float timer;
    private float pInvunerability; //stores how long the player is invunerable after being hit
	// Use this for initialization
	void Start () {
        pHealth = 3;
        pScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //just checking time here
        if (timer>= pInvunerability)
        {
            timer = 0;
        }
        else if (timer > 0)
        {
            timer += Time.deltaTime;
        }

        //lose state?
        if (pHealth <= 0)
        {
            Debug.Log("You lose, NERD!");
        }
	}

    //Handling collision
    //doing certain things based on tags.
    private void OnCollisionEnter(Collision collision)
    {
        //losing health and starting invunerability
        //if 
        if (collision.gameObject.tag == "Enemy"&& pInvunerability == 0)
        {   
                pHealth--;
                timer += Time.deltaTime;
           
        }else if(collision.gameObject.tag == "Item")
        {

        }
    }
}
