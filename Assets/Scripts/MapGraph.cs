using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGraph : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    private MapNode startingNode;

    private int nodeCount = 25;

    [SerializeField]
    GameObject[] gameObjects;

    // Use this for initialization
    void Start()
    {
        Object[] objs = FindObjectsOfType<Room>();
        gameObjects = new GameObject[objs.Length];
        for(int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i] = (objs[i] as Room).gameObject;
        }

        //startingNode = new MapNode(Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)]).GetComponent<Room>());

        //MapNode currNode = startingNode;
        //for (int i = 1; i < nodeCount; i++)
        //{
        //    currNode.exits[0] = new MapNode(Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)]).GetComponent<Room>());
        //    Connect(currNode.data, 0, currNode.exits[0].data);

        //    currNode = currNode.exits[0];
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Vector3 scale = Vector3.one * 0.3f;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Gizmos.DrawCube(gameObjects[i].transform.position, scale);
        }
    }

    //private void Connect(Room exitRm, int exitIndex, Room entranceRm)
    //{
    //    entranceRm.entrance.position = exitRm.exits[exitIndex].position;
    //    entranceRm.entrance.rotation = exitRm.exits[exitIndex].rotation;

    //    entranceRm.RepositionToEntrance();
    //}
}


public class MapNode
{
    public Room data;

    int exitCount;
    public MapNode entrance;
    public MapNode[] exits;

    public MapNode(Room room)
    {
        data = room;
        //exitCount = room.exits.Length;
        exits = new MapNode[exitCount];
    }
}