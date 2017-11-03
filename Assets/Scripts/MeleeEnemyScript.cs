using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyScript : EnemyScript {
    public float maxCooldown=3;
    public float cooldown;
    private bool onCooldown;





	// Use this for initialization
    protected override void Start()
    {
        myBody = GetComponent<Rigidbody>();
        myAgent = GetComponent<NavMeshAgent>();

        base.Start();
    }

    // Update is called once per frame
    void Update () {
        if (isActive && toSeek != null)//if we are alive, and have something to seek
        {
            Tick();
            if (!onCooldown)//and active
            {
                myAgent.isStopped = false;
                myAgent.destination = toSeek.transform.position;//move towards player
            }
            else
            {              
                myAgent.isStopped = true;       
            }

        }
    }

    void Tick()
    {//handles time incrementing.
        if(cooldown >0)
        {
            cooldown -= Time.deltaTime;
        }

        if(onCooldown)
        {
            if(cooldown <= 0)
            {
                onCooldown = false;
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
                otherScript.TakeDamage(outDamage, transform.position);//have them take damage
                cooldown = maxCooldown;//and go on cooldown
                onCooldown = true;
                //Add a bone-crunch sound, if we can.
            }
        }

    }

}
