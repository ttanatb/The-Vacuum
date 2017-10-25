using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyScript : MonoBehaviour {

    public Rigidbody toSeek;
    public GameObject lootDrop;
    public GameObject projectilePrefab;
    public int inverseDropChance = 3;
    public int outDamage = 2;
    public int outRangedDamage= 1;
    public int Health;
    public float maxCooldown = 3;
    public float maxShootCooldown = 5;
    public bool canShoot;
    public float shootCooldown;
    public float cooldown;    
    private bool onCooldown;
    private bool onShootCooldown;
    public bool isActive = true;
    public Rigidbody myBody;
    private NavMeshAgent myAgent;
    public RaycastHit HitHolder;
    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isActive && toSeek != null)//if we are alive, and have something to seek
        {
            cooldown -= Time.deltaTime;//lower your cooldowns
            shootCooldown -= Time.deltaTime;
            if (!onCooldown)//and active
            {
                myAgent.isStopped = false;
                myAgent.destination = toSeek.transform.position;//move towards stuff
                Vector3 direction = toSeek.transform.position - gameObject.transform.position;
             
                Physics.Raycast(gameObject.transform.position, direction, out HitHolder);
                if (HitHolder.collider.tag == "Player" && !onShootCooldown)
                {
                    Shoot();
                }

            }
            else
            {
                myAgent.isStopped = true; ;//if we are inactive, don't move at all
             
                if (cooldown <= 0)//if your cooldown is up, say so.
                {
                    myAgent.isStopped = false;
                    cooldown = 0;//make it precisely 0
                    onCooldown = false; // and turn off the cooldown
                }

                if (shootCooldown <= 0)//if your cooldown is up, say so.
                {
                    shootCooldown = 0;//make it precisely 0
                    onShootCooldown = false; // and turn off the cooldown
                }
            }

        }
    }

    public void Shoot()
    {
        GameObject enemyBullet = (GameObject)Instantiate(projectilePrefab, myBody.transform);
        enemyBullet.transform.SetParent(gameObject.transform.parent, true);
        Debug.Log("pew-pew");
        onShootCooldown = true;
        shootCooldown = maxShootCooldown;
        cooldown = maxCooldown / 2;
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
                    GameObject DroppedHealthPack = (GameObject)Instantiate(lootDrop, myBody.transform);
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