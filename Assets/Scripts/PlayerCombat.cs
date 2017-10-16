using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
      
       
	}

    //Handling collision
    //doing certain things based on tags.
    private void OnCollisionEnter(Collision collision)
    {
       
        
        //picking up item       
        if(collision.gameObject.tag == "Item")
        {

        }
    }

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

    public int PHealth()
    {
        return pHealth;
    }

    public uint PScore()
    {
        return pScore;
    }
}
