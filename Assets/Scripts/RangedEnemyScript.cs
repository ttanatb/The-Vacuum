﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyScript : EnemyScript {

    public GameObject projectilePrefab;
    public float InaccuracyValue;
    public int outRangedDamage= 1;
    public float maxCooldown ;
    public float maxShootCooldown ;
    public bool canShoot;
    public float shootCooldown;
    public float cooldown;    
    private bool onCooldown;
    private bool onShootCooldown;    
    public RaycastHit HitHolder;


    private AudioSource shootingAudio;
    public AudioClip laser;
    // Use this for initialization
    protected override void Start()
    {
        myBody = GetComponent<Rigidbody>();
        myAgent = GetComponent<NavMeshAgent>();

        base.Start();

        shootingAudio = gameObject.AddComponent<AudioSource>();
        shootingAudio.playOnAwake = false;
        shootingAudio.clip = laser;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && toSeek != null)//if we are alive, and have something to seek
        {
            Tick();
            if (!onCooldown)//and active
            {

               
                Vector3 direction = toSeek.transform.position - gameObject.transform.position;
             
                Physics.Raycast(gameObject.transform.position, direction, out HitHolder);
                if (HitHolder.collider.tag == "Player" && !onShootCooldown)
                {
                    myAgent.isStopped = true;
                    Shoot();
                }
                else if (onShootCooldown)
                {
                    myAgent.isStopped = true;                    
                }
                else{
                    myAgent.isStopped = false;
                    myAgent.destination = toSeek.transform.position;//move towards stuff
                }
            }
            else
            {
                myAgent.isStopped = true; ;//if we are inactive, don't move at all             
               
            }

        }
    }

    void Tick()
    {//handles time incrementing.
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if(shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }

        if (onCooldown)
        {
            if (cooldown <= 0)
            {
                onCooldown = false;
            }
        }

        if (onShootCooldown)
        {
            if (shootCooldown <= 0)
            {
                onShootCooldown = false;
            }
        }
    }

    public void Shoot()
    {
        //Transform bulletTransform = myBody.transform;
       

        GameObject enemyBullet = (GameObject)Instantiate(projectilePrefab, myBody.transform.position, Quaternion.identity);
        Physics.IgnoreCollision(enemyBullet.GetComponent<Collider>(), GetComponent<Collider>());


        Vector3 enemyPosition = toSeek.transform.position;
        enemyPosition.x += Random.Range(-InaccuracyValue,InaccuracyValue)/10;
        enemyPosition.z += Random.Range(-InaccuracyValue, InaccuracyValue) / 10;
        enemyBullet.transform.forward = (enemyPosition - enemyBullet.transform.position);//Accuracy

        //Debug.Log("pew-pew");
        onShootCooldown = true;
        shootCooldown = maxShootCooldown;
        cooldown = maxCooldown / 2;


        shootingAudio.pitch = Random.Range(0.5f, 0.9f);
        shootingAudio.Play();
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