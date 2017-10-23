using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //enum for room types
    private enum RoomType
    {
        Straight,
        Turn,
        T
    }

    private RoomType type;
    private MapNode node;

    //Set up the room type correctly
    public void Awake()
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

    private void Update()
    {
        #region debug
        /*
        switch (type)
        {
            case RoomType.Straight:
                //Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);
                //Debug.DrawLine(transform.position, transform.position - transform.up, Color.green);
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
        */
        #endregion
    }

    /// <summary>
    /// Makes sure the room class has a reference to its associated node
    /// </summary>
    /// <param name="n">Map graph node</param>
    public void SetAssociatedNode(MapNode n)
    {
        node = n;
    }

    /// <summary>
    /// Gets all the positions that are connected to the type of hallway
    /// </summary>
    /// <returns>Array of vector3's</returns>
    public Vector3[] GetConnectingPos()
    {
        Vector3[] positions = null;
        switch (type)
        {
            case RoomType.Straight:
                positions = new Vector3[2];
                positions[0] = transform.position + transform.up * 2f;
                positions[1] = transform.position - transform.up * 2f;
                break;
            case RoomType.Turn:
                positions = new Vector3[2];
                positions[0] = transform.position + transform.right * 2f;
                positions[1] = transform.position - transform.up * 2f;
                break;
            case RoomType.T:
                positions = new Vector3[3];
                positions[0] = transform.position - transform.right * 2f;
                positions[1] = transform.position - transform.up * 2f;
                positions[2] = transform.position + transform.up * 2f;
                break;
        }

        return positions;
    }


    //Room has a trigger that is used to track where player is
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MapGraph.Instance.SetCurrentNode(node);
        }
    }
}
