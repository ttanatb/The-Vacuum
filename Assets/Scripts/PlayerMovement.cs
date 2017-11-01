using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FPS camera movement adapted from: http://wiki.unity3d.com/index.php/SmoothMouseLook
public class PlayerMovement : MonoBehaviour
{
    //editable speed vector
    public float speed = 1.0f;
    public float sprintFactor = 2f;
    public float sprintDrainRate = 1f;

    private PlayerCombat combat;

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

        cameraObj = GetComponentInChildren<Camera>().gameObject;
        cameraStartingRot = cameraObj.transform.rotation;
        playerObjStartingRot = transform.rotation;

        rigidBody = GetComponent<Rigidbody>();
        combat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManagerScript.Instance && GameManagerScript.Instance.CurrentGameState != GameState.Play)
            return;

        PlayerInput();
    }

    private void Update()
    {
        if (GameManagerScript.Instance && GameManagerScript.Instance.CurrentGameState != GameState.Play)
            return;

        UpdateDirection();
    }

    void PlayerInput()
    {
        Vector3 movement = (transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal")) * speed * Time.deltaTime;

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
            combat.PEnergy > sprintDrainRate * Time.deltaTime)
        {
            combat.PEnergy -= sprintDrainRate * Time.deltaTime;

            if (combat.PEnergy > sprintDrainRate * Time.deltaTime * 2)
                movement *= sprintFactor;
        }

        rigidBody.MovePosition(movement + transform.position);
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

