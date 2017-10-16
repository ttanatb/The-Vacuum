using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private int wEnergy;
    public GameObject projectile;
    public float wFireRate;
    public float timer;
    public float wRechargeRate;
    // Use this for initialization
    void Start()
    {
        wEnergy = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //timer is checked to set fire rate
        //if the timer isn't running it can fire
        if (timer == 0)
        {
            if (Input.GetMouseButton(0)
            {
                Fire();
            }
            //if the timer is running, update time
        }
        else if (timer > 0)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f / wFireRate)
            {
                timer = 0;
            }
        }

    }
    //For when the player fire's the gun
    void Fire()
    {
        //make a projectile
        GameObject.Instantiate(projectile, Camera.main.transform.position, Camera.main.transform.rotation);
        wEnergy--;
        //start the timer
        timer += Time.deltaTime;
    }

    int WEnergy()
    {
        return wEnergy;
    }
}
