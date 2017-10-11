using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //editable speed vector
    public float speed = 1.0f;
    //Final vector, should be unit vector 
    //to be modified by speed
    private Vector3 finalVector;
    // Use this for initialization
    public Vector3 position;
    //Position
    void Start()
    {
        position = gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        position += finalVector*speed;
        finalVector = Vector3.zero;
        gameObject.transform.position = position;
    }

    void PlayerInput()
    {
        Vector3 temp = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            temp += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            temp += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            temp += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            temp += new Vector3(-1, 0, 0);
        }

       finalVector = Vector3.Normalize(temp);
        
    }

}

