using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FPS camera movement adapted from: http://wiki.unity3d.com/index.php/SmoothMouseLook
public class PlayerMovement : MonoBehaviour
{
    //editable speed vector
    public float speed = 1.0f;
    //Final vector, should be unit vector 
    //to be modified by speed
    private Vector3 finalVector;
    // Use this for initialization
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
    Quaternion startingRot;

    private GameObject cameraObj;

    //Position
    void Start()
    {
        position = gameObject.transform.position;
        Cursor.lockState = CursorLockMode.Locked;

        cameraObj = GetComponentInChildren<Camera>().gameObject;
        startingRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        position += finalVector * speed;
        finalVector = Vector3.zero;
        //gameObject.transform.position = position;

        UpdateDirection();
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
            temp += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            temp += new Vector3(1, 0, 0);
        }

        finalVector = Vector3.Normalize(temp);

        transform.Translate(Vector3.ProjectOnPlane(transform.forward, Vector3.down) * Input.GetAxis("Vertical") * speed, Space.World);
        transform.Translate(transform.right * Input.GetAxis("Horizontal") * speed, Space.World);
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

        cameraObj.transform.rotation = startingRot * xRot * yRot;
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

