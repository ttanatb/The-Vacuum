using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public Rigidbody toSeek;
    public GameObject currentTile;
    public float maxSpeed;
    public float maxAccel;
    public float maxDodge;
    Vector3 velocity;
    Vector3 destination;
    public bool shouldDodge = false;
    public int Health;
    private bool isActive = true;
    private Rigidbody myBody;

    // Use this for initialization
    void Start () {
        myBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        // add code for activation when currentTile becomes relevent

        if (isActive && toSeek != null)
        {   
            destination = toSeek.transform.position;
            //later on change this players tile, then nearest waypoint, then actual position.        
            Vector3 goalSpeed = destination - this.transform.position;
            shouldDodge = false;
            if (goalSpeed.sqrMagnitude > 25)
            {
                shouldDodge = true;
            }
            goalSpeed.Normalize();
            goalSpeed = goalSpeed * Time.deltaTime;
            velocity += Vector3.ClampMagnitude(goalSpeed, maxAccel);
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            if (shouldDodge)
            {
                Dodge();
            }
            //update the actual position
            myBody.transform.position += velocity;
        }
    }



    void Dodge()
    {
        Vector3 storeVelocity = velocity;
        Vector3 vertical = new Vector3(0, 1, 0);

        Vector3 StoreA = this.transform.position + velocity;
        Vector3 StoreB = this.transform.position + vertical;

        Vector3 Dodgevector = Vector3.Cross(StoreA, StoreB);
        Dodgevector.Normalize();

        Dodgevector = Vector3.ClampMagnitude(Dodgevector * Mathf.Sin(Time.time), maxDodge);
        Dodgevector.y = 0;
        velocity += Dodgevector;
    }

    void TakeDamage(int incDamage)//damage for object to take in
    {
        Health -= incDamage;
        if(Health <= 0)
        {
            isActive = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision otherObject)
    {
        if(otherObject.gameObject.tag == "Player")
        {
           EnemyScript otherScript = otherObject.gameObject.GetComponent<EnemyScript>();
            otherScript.TakeDamage(2);
        }

    }

}
