using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform entrance;
    public Transform[] exits = new Transform[0];

    private Vector3 entranceOrigLocalPos;
    //private Vector3 exitOrigLocalPos;

    private Quaternion entranceOrigLocalRot;
    //private Vector3 exitOrigForward;

    // Use this for initialization
    void Awake()
    {
        if (!entrance)
        {
            for(int i = 0; i< transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.name.Contains("Entrance"))
                {
                    entrance = child;
                    break;
                }
            }
        }

        entranceOrigLocalPos = entrance.localPosition;
        entranceOrigLocalRot = entrance.localRotation;

        if (exits.Length == 0)
        {
            List<Transform> currExits = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.name.Contains("Exit"))
                {
                    currExits.Add(child);
                }
            }

            exits = currExits.ToArray();
        }

        //exitOrigLocalPos = exit.localPosition;
        //exitOrigForward = exit.forward;
    }

    public void RepositionToEntrance()
    {
        transform.Translate(entrance.localPosition - entranceOrigLocalPos);
        entrance.localPosition = entranceOrigLocalPos;
        Vector3 entrPos = entrance.position;
        Quaternion q = Quaternion.Inverse(entranceOrigLocalRot) * entrance.localRotation;
        transform.rotation = q * transform.rotation;
        entrance.localRotation = entranceOrigLocalRot;
        transform.Translate(entrPos - entrance.position, Space.World);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            RepositionToEntrance();
    }
}
