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
     
	}
	
	// Update is called once per frame
	void Update () {     

       
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
                    GameObject DroppedHealthPack = (GameObject)  Instantiate(lootDrop, myBody.transform);
                    DroppedHealthPack.transform.SetParent(gameObject.transform.parent, true);
                 }
                isActive = false;
                
                Destroy(gameObject);
            }
        }
    }

   

    private void OnDestroy()
    {
        ScoreManager.Instance.IncrementScore(5);
    }
}
