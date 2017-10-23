using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that represents a map node
/// </summary>
public class MapNode
{
    //private variables
    private Room data;
    private MapNode[] neighbors;
    private bool visisted;

    //properties
    public Room Data
    {
        get { return data; }
    }

    public MapNode[] Neighbors
    {
        get { return neighbors; }
        set { neighbors = value; }
    }

    public bool Visited
    {
        get { return visisted; }
        set { visisted = value; }
    }

    /// <summary>
    /// Constructor to build a mapnode based off of the data
    /// </summary>
    /// <param name="room"></param>
    public MapNode(Room room)
    {
        data = room;
        room.SetAssociatedNode(this);       //make it so that the room object also have a reference to the node
        visisted = false;
    }
}
