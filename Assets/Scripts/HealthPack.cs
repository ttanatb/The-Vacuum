using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Collectible
{
    public float energy = 3f;
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerCombat>().GainEnergy(energy);
            Destroy(gameObject);
        }
    }
}
