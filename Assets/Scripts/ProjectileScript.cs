using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;
    public float gravityMultiplier;
    public float lifeSpan;
    private Vector3 position;
    private Vector3 velocity;
    private float timer;

    void Start()
    {
        //getting position vector to edit
        position = gameObject.transform.position;

        //Detaching from parent 
        transform.parent = null;

        //projectile moves only forward(gun propulsion) and down(Gravity)
        //velocity = new Vector3(0, -gravityMultiplier, -speed);
        velocity = speed * transform.forward + Vector3.down * gravityMultiplier;
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

        position += velocity * Time.deltaTime;
        timer += Time.deltaTime;
        gameObject.transform.position = position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //further coding will happen here with enemies, or we may want to handle that in enemy script
        //For now, we destory the projectile
        if (collision.gameObject.tag != "Player") {
            Debug.Log("We Hit something " + collision.gameObject.tag);
            Destroy(gameObject);
        }
    }
}
