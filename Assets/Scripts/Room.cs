using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Straight,
    Turn,
    T
}

public class Room : MonoBehaviour
{
    public RoomType type;
    MapNode node;

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

    public void SetAssociatedNode(MapNode n)
    {
        node = n;
    }

    private void Update()
    {
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
    }

    public Vector3[] GetConnectingPos()
    {
        Vector3[] positions = null;
        switch(type)
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
                positions = new Vector3[2];
                positions[0] = transform.position - transform.right * 2f;
                positions[1] = transform.position - transform.up * 2f;
                positions[2] = transform.position + transform.up * 2f;
                break;
        }

        return positions;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MapGraph.Instance.SetCurrentNode(node);
        }
    }
}
