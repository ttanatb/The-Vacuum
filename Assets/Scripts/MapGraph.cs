using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGraph : SingletonMonoBehaviour<MapGraph>
{
    public Transform player;

    public GameObject[] roomPrefabs;

    [SerializeField]
    private MapNode[] nodes;

    private int nodeCount = 25;

    [SerializeField]
    GameObject[] gameObjects;

    private MapNode currNode;

    // Use this for initialization
    void Start()
    {
        Object[] objs = FindObjectsOfType<Room>();
        gameObjects = new GameObject[objs.Length];
        nodes = new MapNode[objs.Length];
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i] = (objs[i] as Room).gameObject;
            nodes[i] = new MapNode(objs[i] as Room);
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            Vector3[] surroundingPos = nodes[i].data.GetConnectingPos();
            List<MapNode> neighbors = new List<MapNode>();

            for (int j = 0; j < nodes.Length; j++)
            {
                for (int k = 0; k < surroundingPos.Length; k++)
                {
                    surroundingPos[k].y = nodes[j].data.transform.position.y;
                    if ((surroundingPos[k] - nodes[j].data.transform.position).sqrMagnitude < 0.1f)
                    {
                        neighbors.Add(nodes[j]);
                    }
                }
            }

            nodes[i].neighbors = neighbors.ToArray();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Vector3 scale = Vector3.one * 0.3f;
        for (int i = 0; i < nodes.Length; i++)
        {
            if (!nodes[i].visited)
            {
                Color c = Color.white;
                c.a = 0.2f;
                Gizmos.color = c;
            }
            else
            {
                Color c = Color.black;
                c.a = 0.2f;
                Gizmos.color = c;
            }

            Gizmos.DrawSphere(nodes[i].data.transform.position, 0.5f);
        }

        if (currNode != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(currNode.data.transform.position, scale);

            Gizmos.color = Color.red;
            foreach (MapNode n in currNode.neighbors)
            {
                Gizmos.DrawCube(n.data.transform.position, scale);
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawCube(currNode.data.transform.position, scale);
        }
    }

    public void SetCurrentNode(MapNode n)
    {
        currNode = n;
        currNode.visited = true;
    }
}

public class MapNode
{
    public Room data;
    public MapNode[] neighbors;
    public bool visited = false;

    public MapNode(Room room)
    {
        data = room;
        room.SetAssociatedNode(this);
    }
}