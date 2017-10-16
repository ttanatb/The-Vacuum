using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {
    public Rigidbody toSeek;       
    public int outDamage =2;
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

        if (isActive && toSeek != null)
        {   
            myAgent.destination = toSeek.transform.position;     
           
        }
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
            otherScript.TakeDamage(outDamage);
        }

    }

}
