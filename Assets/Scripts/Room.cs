using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum RoomType
    {
        Straight,
        Turn,
        T
    }


    public RoomType type;

    //public Transform entrance;
    //public Transform[] exits = new Transform[0];

    //private Vector3 entranceOrigLocalPos;
    //private Vector3 exitOrigLocalPos;

    //private Quaternion entranceOrigLocalRot;
    //private Vector3 exitOrigForward;

    public void Start()
    {
        if (gameObject.name.Contains("Straight"))
        {
            type = RoomType.Straight;
        }
        else if (gameObject.name.Contains("Turn"))
        {
            type = RoomType.Turn;
        }
        else if (gameObject.name.Contains("T"))
        {
            type = RoomType.T;
        }
    }

    /*
    // Use this for initialization
    //void Awake()
    //{
    //    if (!entrance)
    //    {
    //        for(int i = 0; i< transform.childCount; i++)
    //        {
    //            Transform child = transform.GetChild(i);
    //            if (child.name.Contains("Entrance"))
    //            {
    //                entrance = child;
    //                break;
    //            }
    //        }
    //    }

    //    entranceOrigLocalPos = entrance.localPosition;
    //    entranceOrigLocalRot = entrance.localRotation;

    //    if (exits.Length == 0)
    //    {
    //        List<Transform> currExits = new List<Transform>();
    //        for (int i = 0; i < transform.childCount; i++)
    //        {
    //            Transform child = transform.GetChild(i);
    //            if (child.name.Contains("Exit"))
    //            {
    //                currExits.Add(child);
    //            }
    //        }

    //        exits = currExits.ToArray();
    //    }

    //    //exitOrigLocalPos = exit.localPosition;
    //    //exitOrigForward = exit.forward;
    //}

    //public void RepositionToEntrance()
    //{
    //    transform.Translate(entrance.localPosition - entranceOrigLocalPos);
    //    entrance.localPosition = entranceOrigLocalPos;
    //    Vector3 entrPos = entrance.position;
    //    Quaternion q = Quaternion.Inverse(entranceOrigLocalRot) * entrance.localRotation;
    //    transform.rotation = q * transform.rotation;
    //    entrance.localRotation = entranceOrigLocalRot;
    //    transform.Translate(entrPos - entrance.position, Space.World);
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.D))
    //        RepositionToEntrance();
    //}
    */


    private void Update()
    {
        switch (type)
        {
            case RoomType.Straight:
                Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);
                Debug.DrawLine(transform.position, transform.position - transform.up, Color.green);
                break;
            case RoomType.Turn:
                Debug.DrawLine(transform.position, transform.position + transform.right, Color.red);
                Debug.DrawLine(transform.position, transform.position - transform.up, Color.green);

                break;
            case RoomType.T:
                Debug.DrawLine(transform.position, transform.position - transform.right, Color.red);
                Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);
                Debug.DrawLine(transform.position, transform.position - transform.up, Color.green);

                break;
        }
        //Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);

        //Debug.DrawLine(transform.position, transform.position + transform.forward);
    }
}
