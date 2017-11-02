using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {
    public AudioClip die;

    public Rigidbody toSeek;
    public GameObject lootDrop;
    public GameObject altLootDrop;
    public int inverseDropChance=4;
    public int outDamage =2;
    public int Health;  
    public bool isActive = true;
    public Rigidbody myBody;
    public NavMeshAgent myAgent;
    // Use this for initialization
    protected virtual void Start () {
        GetComponent<AudioSource>().pitch = Random.Range(0.6f, 1.4f);
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
                else if (Random.Range(0, inverseDropChance) == 1)// if we generate the right number, drop a healthpack.
                {
                    GameObject DroppedEnergyPack = (GameObject)Instantiate(altLootDrop, myBody.transform);
                    DroppedEnergyPack.transform.SetParent(gameObject.transform.parent, true);
                }
                isActive = false;
                
                Destroy(gameObject, die.length);
                PlayDeathAudio();
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void PlayDeathAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio.isPlaying)
            audio.Stop();

        audio.clip = die;
        audio.Play();
    }



    private void OnDestroy()
    {
        if (ScoreManager.Instance)
            ScoreManager.Instance.IncrementScore(5);
    }
}
