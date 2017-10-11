using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Collectible : MonoBehaviour
{

    protected virtual void OnCollisionEnter(Collision collision)
    {

    }
}
