using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
    private int ammo;
    public GameObject projectile;
    public float fireRate;
    public float timer;
	// Use this for initialization
	void Start () {
        ammo = 50;
	}
	
	// Update is called once per frame
	void Update () {
        //timer is checked to set fire rate
        //if the timer isn't running it can fire
        if (timer == 0)
        {
            if (Input.GetMouseButton(0))
            {
                Fire();
            }
        //if the timer is running, update time
        }else if (timer > 0)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f / fireRate)
            {
                timer = 0;
            }
        }
        
	}
    //For when the player fire's the gun
    void Fire() {
        //make a projectile
        GameObject.Instantiate(projectile, gameObject.transform.position,gameObject.transform.rotation, gameObject.transform);
        
        //start the timer
        timer += Time.deltaTime;
    }
}
