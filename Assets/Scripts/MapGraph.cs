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

    public Room goalRoom;
    private MapNode goalNode = null;

    private MapNode spawnNode = null;

    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;

    private float timer = 0f;
    public float timeToSpawn = 5f;
    public float timerReductionRate = 0.99f;
    public float timeBetweenUpdates = 0.5f;

    // Use this for initialization
    void Start()
    {
        //get all room objects
        Object[] objs = FindObjectsOfType<Room>();

        //populate the array of nodes
        List<MapNode> bigRooms = new List<MapNode>();
        nodes = new MapNode[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            nodes[i] = new MapNode(objs[i] as Room);
            if (goalNode == null)
            {
                if (nodes[i].Data == goalRoom)
                {
                    goalNode = nodes[i];
                }
            }

            if (nodes[i].IsBigRoom)
                bigRooms.Add(nodes[i]);
        }
        //Debug.Log(bigRooms.Count);

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
                surroundingPos = nodes[i].Data.GetConnectingPosDouble();
                for (int k = 0; k < surroundingPos.Length; k++)
                {
                    //loop through all the other nodes
                    for (int j = 0; j < bigRooms.Count; j++)
                    {
                        //sets the y pos to be the same
                        surroundingPos[k].y = bigRooms[j].Data.transform.position.y;
                        Debug.DrawLine(surroundingPos[k], bigRooms[j].Data.transform.position);

                        //if the position is basically the same, add to the array
                        if ((surroundingPos[k] - bigRooms[j].Data.transform.position).sqrMagnitude < 1f)
                        {
                            neighbors[count] = bigRooms[j];
                            count++;
                        }
                    }
                }
                //if (count != neighbors.Length && !nodes[i].Data.name.Contains("Room_Cross"))
                    //Debug.LogError(nodes[i].Data.name + " did not connect properly");

            }

            nodes[i].Neighbors = neighbors;
        }

        IEnumerator coroutine = UpdateSpawnLoc(timeBetweenUpdates);
        StartCoroutine(coroutine);
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

    private MapNode BreadthFirstSearch()
    {
        Queue<MapNode> queueStart = new Queue<MapNode>();
        Queue<MapNode> queueEnd = new Queue<MapNode>();
        List<MapNode> startVisited = new List<MapNode>();
        List<MapNode> endVisited = new List<MapNode>();

        queueStart.Enqueue(goalNode);
        queueEnd.Enqueue(currNode);

        List<MapNode> meeting = new List<MapNode>();

        while (queueStart.Count > 0 || queueEnd.Count > 0)
        {
            MapNode nodeF = null;
            MapNode nodeB = null;
            if (queueStart.Count > 0)
            {

                nodeF = queueStart.Dequeue();
                startVisited.Add(nodeF);
                if (nodeF == currNode)
                    nodeF = null;
            }

            if (queueEnd.Count > 0)
            {
                nodeB = queueEnd.Dequeue();
                endVisited.Add(nodeB);

                if (startVisited.Contains(nodeB))
                {
                    meeting.Add(nodeB);
                    nodeB = null;
                }
            }

            if (nodeF != null && nodeF.Neighbors != null)
            {
                foreach (MapNode child in nodeF.Neighbors)
                {
                    if (child != null)
                    {
                        if (startVisited.Contains(child) || endVisited.Contains(child))
                            continue;
                        else
                        {
                            queueStart.Enqueue(child);
                        }

                    }
                }
            }

            if (nodeB != null && nodeB.Neighbors != null)
            {
                foreach (MapNode child in nodeB.Neighbors)
                {
                    if (child != null)
                    {
                        if (endVisited.Contains(child))
                            continue;
                        else
                        {
                            queueEnd.Enqueue(child);
                        }

                    }
                }
            }
        }

        float maxDist = float.MaxValue;
        MapNode nearest = null;
        foreach (MapNode n in meeting)
        {
            float dist = (n.Data.transform.position - goalNode.Data.transform.position).sqrMagnitude;
            if (dist < maxDist)
            {
                maxDist = dist;
                nearest = n;
            }
        }

        return nearest;
    }


    private void Update()
    {
        timer += Time.deltaTime;


        if (timer > timeToSpawn)
        {
            SpawnEnemy();
            timer = 0f;
            timeToSpawn *= timerReductionRate;
        }
    }

    void SpawnEnemy()
    {
        if (meleeEnemyPrefab && rangedEnemyPrefab && spawnNode != null)
        {
            GameObject obj = null;
            if (Random.value < 0.5f)
            {
                obj = Instantiate(meleeEnemyPrefab, spawnNode.SpawnLoc, Quaternion.identity);
            }
            else
            {
                obj = Instantiate(rangedEnemyPrefab, spawnNode.SpawnLoc, Quaternion.identity);
            }

            obj.GetComponent<EnemyScript>().toSeek = player.GetComponent<Rigidbody>();
        }
    }

    IEnumerator UpdateSpawnLoc(float timeToWait)
    {
        for (;;)
        {
            spawnNode = BreadthFirstSearch();
            yield return new WaitForSeconds(timeToWait);
        }
    }


    /*
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

        if (spawnNode != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(spawnNode.Data.transform.position, Vector3.one * 0.3f);
        }
    }
    */
}

