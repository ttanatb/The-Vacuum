using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;
    public float gravityMultiplier;
    public float lifeSpan;
    public int outDamage = 2;
    private Vector3 position;
    private Vector3 firstForward;
    private float timer;
    Rigidbody rigidBody;
    public GameObject particleSystem;

    public Transform sourceTransform;


    void Start()
    {
        //getting position vector to edit
        position = gameObject.transform.position;

        //Detaching from parent 
        transform.parent = null;

        //projectile moves only forward(gun propulsion) and down(Gravity)
        //velocity = new Vector3(0, -gravityMultiplier, -speed);
        rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.AddForce(gameObject.transform.forward * 10, ForceMode.Impulse);

        firstForward = gameObject.transform.forward;
      
    }

    void Update()
    {
        //if the projectile has reached the end of it's lifespan
        //Destroy it
        if (timer >= lifeSpan)
        {
            Destroy(gameObject);
        }
        //updating the timer and position here
        timer += Time.deltaTime;
       

    }
    private void OnCollisionEnter(Collision collision)
    {
        //making a particle system
        GameObject tempParticleSystem = Instantiate(particleSystem,gameObject.transform);

        tempParticleSystem.transform.forward = gameObject.transform.forward*-1;

        tempParticleSystem.transform.SetParent(null);


        //Debug.Log("Colliding with " + collision.gameObject);

        //collision with an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyScript eScript = collision.gameObject.GetComponent<EnemyScript>();
            if (eScript)
            {
                eScript.TakeDamage(outDamage);

                
            }
        }

        if (collision.gameObject.tag == "RangedEnemy" && gameObject.tag !="Enemy")
        {
            RangedEnemyScript eScript = collision.gameObject.GetComponent<RangedEnemyScript>();
            eScript.TakeDamage(outDamage);
            
        
        }

        else if (collision.gameObject.tag == "Player" && gameObject.tag == "Enemy")
        {
            PlayerCombat pScript = collision.gameObject.GetComponent<PlayerCombat>();

            if (sourceTransform)
            {
                pScript.TakeDamage(outDamage, sourceTransform.position);//have them take damage
            }
            else pScript.TakeDamage(outDamage, collision.transform.position);//have them take damage

            

        }
        Destroy(tempParticleSystem, 1.5f);
        //no matter what destroy at the end.
        Destroy(gameObject);

    }
}
