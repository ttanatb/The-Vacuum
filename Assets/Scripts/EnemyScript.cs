﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {
    public Rigidbody toSeek;
    public GameObject lootDrop;
    public int inverseDropChance=4;
    public int outDamage =2;
    public int Health;
    public float maxCooldown=3;
    private float cooldown;
    private bool onCooldown;
    public bool isActive = true;
    public Rigidbody myBody;
    public NavMeshAgent myAgent;
    // Use this for initialization
    void Start () {
        myBody = GetComponent<Rigidbody>();
        myAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {     

        if (isActive && toSeek != null)//if we are alive, and have something to seek
        {
            if (!onCooldown)//and active
            {
                myAgent.destination = toSeek.transform.position;//move towards stuff
            }
            else
            {
                myAgent.destination = transform.position;//if we are inactive, just move towards yourself. (don't move at all.)
                cooldown -= Time.deltaTime;//lower your cooldown.
                if(cooldown <= 0)//if your cooldown is up, say so.
                {
                    cooldown = 0;//make it precisely 0
                    onCooldown = false; // and turn off the cooldown

                }
            }
           
        }
    }

   public void TakeDamage(int incDamage)//damage for object to take in
    {
        if (isActive)//can't kill me if im already dead.
        {
            Health -= incDamage;
            if (Health <= 0)//if i'm dead, do on death code.
            {
                if (Random.Range(0, inverseDropChance) == 0)// if we generate the right number, drop a healthpack.
                  {
                    GameObject DroppedHealthPack = (GameObject)  Instantiate(lootDrop, myBody.transform);
                    DroppedHealthPack.transform.SetParent(gameObject.transform.parent, true);
                 }
                isActive = false;
                
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision otherObject)
    {//if i hit someone
        if (isActive)//and i am active
        {
            if (otherObject.gameObject.tag == "Player")//if it is a player
            {
                PlayerCombat otherScript = otherObject.gameObject.GetComponent<PlayerCombat>();
                otherScript.TakeDamage(outDamage);//have them take damage
                cooldown = maxCooldown;//and go on cooldown
                onCooldown = true;
                //Add a bone-crunch sound, if we can.
            }
        }

    }

}
