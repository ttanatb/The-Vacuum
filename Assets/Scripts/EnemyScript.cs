using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {
    public Rigidbody toSeek;
    public GameObject lootDrop;
    public int inverseDropChance=4;
    public int outDamage =2;
    public int Health;
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

        if (isActive && toSeek != null)
        {   
            myAgent.destination = toSeek.transform.position;     
           
        }
    }

   public void TakeDamage(int incDamage)//damage for object to take in
    {
        if (isActive)//can't kill me if im already dead.
        {
            Health -= incDamage;
            if (Health <= 0)
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
    {
        if(otherObject.gameObject.tag == "Player")
        {
           PlayerCombat otherScript = otherObject.gameObject.GetComponent<PlayerCombat>();
            otherScript.TakeDamage(outDamage);
        }

    }

}
