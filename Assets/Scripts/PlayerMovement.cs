﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FPS camera movement adapted from: http://wiki.unity3d.com/index.php/SmoothMouseLook
public class PlayerMovement : MonoBehaviour
{
    //editable speed vector
    public float speed = 1.0f;
    public float maxSpeed = 2f;

    public Vector3 position;

    private List<float> rotListX = new List<float>();
    float avgRotX = 0f;

    private List<float> rotListY = new List<float>();
    float avgRotY = 0f;

    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    public float minX = -360f;
    public float maxX = 360f;

    public float minY = -60f;
    public float maxY = 60f;

    private float rotX = 0f;
    private float rotY = 0f;

    public uint frameCounter = 20;

    private GameObject cameraObj;
    Quaternion cameraStartingRot;
    Quaternion playerObjStartingRot;

    private Rigidbody rigidBody;

    //Position
    void Start()
    {
            position = gameObject.transform.position;
            Cursor.lockState = CursorLockMode.Locked;

            cameraObj = GetComponentInChildren<Camera>().gameObject;
            cameraStartingRot = cameraObj.transform.rotation;
            playerObjStartingRot = transform.rotation;

            rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        UpdateDirection();
    }

    void PlayerInput()
    {
        Vector3 vel = rigidBody.velocity;
        vel += transform.forward * Input.GetAxis("Vertical") * speed;
        vel += transform.right * Input.GetAxis("Horizontal") * speed;
        vel = Vector3.ClampMagnitude(vel, maxSpeed);

        rigidBody.velocity = vel;
    }

    void UpdateDirection()
    {
        avgRotX = 0f;
        avgRotY = 0f;

        rotY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotX += Input.GetAxis("Mouse X") * sensitivityX;

        rotListY.Add(rotY);
        rotListX.Add(rotX);

        if (rotListX.Count > frameCounter)
        {
            rotListX.RemoveAt(0);
        }

        if (rotListY.Count > frameCounter)
        {
            rotListY.RemoveAt(0);
        }

        for (int i = 0; i < rotListX.Count; i++)
        {
            avgRotX += rotListX[i];
            avgRotY += rotListY[i];
        }

        avgRotX /= rotListX.Count;
        avgRotY /= rotListY.Count;

        avgRotY = ClampAngle(avgRotY, minY, maxY);
        avgRotX = ClampAngle(avgRotX, minX, maxX);

        Quaternion yRot = Quaternion.AngleAxis(avgRotY, Vector3.left);
        Quaternion xRot = Quaternion.AngleAxis(avgRotX, Vector3.up);

        //Rotating camera around x axis
        cameraObj.transform.rotation = cameraStartingRot * xRot * yRot;
        transform.rotation = playerObjStartingRot * xRot;
        //Rotating player around y axis
    }


    private float ClampAngle(float angle, float min, float max)
    {
        angle %= 360;
        if ((angle >= -360f) && (angle <= 360f))
        {
            if (angle < -360f)
            {
                angle += 360f;
            }
            if (angle > 360f)
            {
                angle -= 360f;
            }
        }

        return Mathf.Clamp(angle, min, max);
    }
}

