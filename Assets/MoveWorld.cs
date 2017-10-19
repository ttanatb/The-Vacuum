using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorld : MonoBehaviour {
    public Vector3 dir;
    public float speed;
    public float acceleration;
    public GameObject enableatEnd;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += dir * speed * Time.deltaTime;
        speed += acceleration * Time.deltaTime;
	}

    void Stop()
    {
        enabled = false;
        enableatEnd.SetActive(true);
    }
}
