using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {
    public Rigidbody toSeek;
    public GameObject currentTile;
    public float maxDodge;
    Vector3 velocity;
    Vector3 destination;
    public bool shouldDodge = false;
    public int Health;
    private bool isActive = true;
    private Rigidbody myBody;
    private NavMeshAgent myAgent;
    // Use this for initialization
    void Start () {
        myBody = GetComponent<Rigidbody>();
        myAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        // add code for activation when currentTile becomes relevent

        if (isActive && toSeek != null)
        {   
            myAgent.destination = toSeek.transform.position;
         
           
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
