using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private int wEnergy;
    public GameObject projectile;
    public float wFireRate;
    private float fireTimer;
    private float rechargeTimer;
    public float wRechargeRate;
    public float pEnergyMax;
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
        if (fireTimer == 0)
        {
            if (Input.GetMouseButton(0)&&wEnergy>0)
            {
                Fire();
            }
            //if the timer is running, update time
        }
        else if (fireTimer > 0)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= 1.0f / wFireRate)
            {
                fireTimer = 0;
            }
        }
        rechargeTimer += Time.deltaTime;
        if (rechargeTimer >= 1/wRechargeRate && wEnergy != pEnergyMax)
        {
            wEnergy++;
            rechargeTimer = 0;
        }

    }
    //For when the player fire's the gun
    void Fire()
    {
        //make a projectile
        GameObject.Instantiate(projectile, Camera.main.transform.position, Camera.main.transform.rotation);
        wEnergy--;
        //start the timer
        fireTimer += Time.deltaTime;
    }

    int WEnergy()
    {
        return wEnergy;
    }
}
