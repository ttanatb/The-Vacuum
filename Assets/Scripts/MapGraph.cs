using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGraph : SingletonMonoBehaviour<MapGraph>
{
    //transform of the player
    //TO-DO: make private and get reference from game manager
    public Transform player;

    private MapNode[] nodes;    //array of all the nodes
    private MapNode currNode;   //node in which the player is in

    // Use this for initialization
    void Start()
    {
        //get all room objects
        Object[] objs = FindObjectsOfType<Room>();

        //populate the array of nodes
        nodes = new MapNode[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            nodes[i] = new MapNode(objs[i] as Room);
        }

        #region debug
        /*
        for(int i =0; i <nodes.length; i++)
        {
            debug.log(nodes[i].data.name + ": " + nodes[i].data.transform.position);
        }
        */
        #endregion

        //loop through all the nodes
        for (int i = 0; i < nodes.Length; i++)
        {
            //get the surround positions of the nodes
            Vector3[] surroundingPos = nodes[i].Data.GetConnectingPos();

            //create an array of neighbhors
            MapNode[] neighbors = new MapNode[surroundingPos.Length];
            int count = 0;

            #region debug
            /*
            Debug.Log("We are on " + nodes[i].data.name);
            string s = "";
            foreach (Vector3 pos in surroundingPos)
                s += pos.ToString() + " ";

            Debug.Log("Surround positions are: " + s);
            */
            #endregion

            //loop through all the surrounding positions
            for (int k = 0; k < surroundingPos.Length; k++)
            {
                
                //loop through all the other nodes
                for (int j = 0; j < nodes.Length; j++)
                {
                    //check if same node
                    if (j == i) continue;

                    //sets the y pos to be the same
                    surroundingPos[k].y = nodes[j].Data.transform.position.y;

                    #region debug
                    /*
                    //Debug.DrawLine(surroundingPos[k], nodes[j].data.transform.position);
                    //string otherName = nodes[j].data.name;
                    //Vector3 pos1 = surroundingPos[k];
                    //Vector3 pos2 = nodes[j].data.transform.position;
                    */
                    #endregion

                    //if the position is basically the same, add to the array
                    if ((surroundingPos[k] - nodes[j].Data.transform.position).sqrMagnitude < 1f)
                    {
                        neighbors[count] = nodes[j];
                        count++;
                    }
                }
            }

            //Logs with error if a node is not connected properly
            if (count != neighbors.Length)
            {
                Debug.LogError(nodes[i].Data.name + " did not connect properly");
            }

            nodes[i].Neighbors = neighbors;
        }
    }

    /// <summary>
    /// A method to update the currentNode
    /// </summary>
    /// <param name="n">A node to switch to</param>
    public void SetCurrentNode(MapNode n)
    {
        currNode = n;
        currNode.Visited = true;
    }

    //TO-DO:    write a public function to return the closest, unvisited room to spawn enemies in
    //          extend to return the closest, unvisited room that is out of the player's line of sight

    //TO-DO: Delete this later (or comment out or something)
    private void OnDrawGizmos()
    {
        if (nodes == null) return;

        //loop through all the nodes and draw whtie if visisted (black otherwise)
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i] == null) continue;

            Color c = Color.black;
            if (!nodes[i].Visited)
                c = Color.white;

            c.a = 0.2f;
            Gizmos.color = c;
            Gizmos.DrawSphere(nodes[i].Data.transform.position, 0.5f);
        }

        //drawing the current node & its neighbors
        if (currNode != null)
        {
            Vector3 scale = Vector3.one * 0.3f;
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(currNode.Data.transform.position, scale);

            Gizmos.color = Color.red;
            foreach (MapNode n in currNode.Neighbors)
            {
                if (n != null)
                    Gizmos.DrawCube(n.Data.transform.position, scale);
            }
        }
    }
}

