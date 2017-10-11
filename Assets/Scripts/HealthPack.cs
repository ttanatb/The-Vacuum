using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Collectible
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
