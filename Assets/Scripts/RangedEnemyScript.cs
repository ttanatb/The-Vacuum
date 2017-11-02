using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyScript : EnemyScript
{

    public GameObject projectilePrefab;
    public float InaccuracyValue;
    public int outRangedDamage = 1;
    public float maxCooldown;
    public float maxShootCooldown;
    public bool canShoot;
    public float shootCooldown;
    public float cooldown;
    private bool onCooldown;
    private bool onShootBackSwing;
    public RaycastHit HitHolder;

    private AudioSource shootingAudio;
    public AudioClip laser;

    public LayerMask layerMask;

    private Animator animator;
    // Use this for initialization
    protected override void Start()
    {
        myBody = GetComponent<Rigidbody>();
        myAgent = GetComponent<NavMeshAgent>();

        base.Start();

        shootingAudio = gameObject.AddComponent<AudioSource>();
        shootingAudio.playOnAwake = false;
        shootingAudio.clip = laser;

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && toSeek != null)//if we are alive, and have something to seek
        {
            Tick();
            if (onCooldown || onShootBackSwing)
            {
                myAgent.isStopped = true; ;//if we are inactive, don't move at all   

                Vector3 newDirection = toSeek.transform.position - transform.position;
                newDirection.y = transform.forward.y;
                transform.forward = Vector3.Lerp(transform.forward, newDirection, Time.deltaTime * 10f);
            }
            else
            {
                Vector3 direction = toSeek.transform.position - Vector3.up * 0.1f - gameObject.transform.position;

                if (Physics.Raycast(gameObject.transform.position, direction, out HitHolder, 6f, layerMask))
                {
                    if (HitHolder.collider.tag == "Player" && !onShootBackSwing)
                    {
                        myAgent.isStopped = true;
                        Shoot();
                        Vector3 newDirection = toSeek.transform.position - transform.position;
                        newDirection.y = transform.forward.y;
                        transform.forward = Vector3.Lerp(transform.forward, newDirection, Time.deltaTime * 10f);
                    }
                    else
                    {
                        animator.SetBool("Shooting", false);
                        myAgent.isStopped = false;
                        myAgent.destination = toSeek.transform.position;
                    }
                }
                else
                {
                    myAgent.isStopped = false;
                    myAgent.destination = toSeek.transform.position;//move towards stuff
                }
            }
        }
    }

    void Tick()
    {//handles time incrementing.
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (shootCooldown > 0)
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

        if (onShootBackSwing)
        {
            if (shootCooldown <= 0)
            {
                onShootBackSwing = false;
            }
        }
    }

    public void Shoot()
    {
        //Transform bulletTransform = myBody.transform;
        GameObject enemyBullet = (GameObject)Instantiate(projectilePrefab, myBody.transform.position, Quaternion.identity);
        Physics.IgnoreCollision(enemyBullet.GetComponent<Collider>(), GetComponent<Collider>());

        Vector3 enemyPosition = toSeek.transform.position;
        enemyPosition.x += Random.Range(-InaccuracyValue, InaccuracyValue) / 10;
        enemyPosition.z += Random.Range(-InaccuracyValue, InaccuracyValue) / 10;
        enemyBullet.transform.forward = (enemyPosition - enemyBullet.transform.position);//Accuracy

        enemyBullet.GetComponent<ProjectileScript>().sourceTransform = transform;

        onShootBackSwing = true;
        shootCooldown = maxShootCooldown;
        cooldown = maxCooldown / 2;

        shootingAudio.pitch = Random.Range(0.5f, 0.9f);
        shootingAudio.Play();

        animator.SetBool("Shooting", true);
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