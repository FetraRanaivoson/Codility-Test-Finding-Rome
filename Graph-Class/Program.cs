using System;
using System.Collections;
using System.Collections.Generic;

namespace Graph_Class
{
    public class Graph
    {
        int nbNodes;
        Dictionary<int, Vertex> adjacencyList;

        public Graph()
        {
            this.nbNodes = 0;
            this.adjacencyList = new Dictionary<int, Vertex>();
        }

        /// <summary>
        /// Add a vertex or a node to this graph.The node will be stored as a Key in an hash table
        /// </summary>
        public void AddVertex(int vertexValue)
        {
            if (!this.adjacencyList.ContainsKey(vertexValue))
            {
                this.adjacencyList.Add(vertexValue, new Vertex(vertexValue)); //Note that the key and the vertex value is the same
                this.nbNodes++;
            }
        }

        /// <summary>
        /// Add an edge or connection to this non oriented graph represented by two existing vertices or two existing nodes
        /// </summary>
        public void AddEdge(int vertexA, int vertexB)
        {
            //  Sanity check
            int failedToAddEdgeCount = 0;

            //Node vertexANode = new Node(vertexA);
            if (!this.adjacencyList.ContainsKey(vertexA))
            {
                Console.WriteLine("Vertex '{0}' doesn't exists on the graph!", vertexA);
                failedToAddEdgeCount++;
            }

            //Node vertexBNode = new Node(vertexB);
            if (!this.adjacencyList.ContainsKey(vertexB))
            {
                Console.WriteLine("Vertex '{0}' doesn't exists on the graph!", vertexB);
                failedToAddEdgeCount++;
            }

            if (failedToAddEdgeCount > 0)
            {
                return;
            }

            //  Add the edges 
            this.adjacencyList[vertexA].AddConn(vertexB, this.adjacencyList[vertexB]);
            this.adjacencyList[vertexB].AddConn(vertexA, this.adjacencyList[vertexA]);
        }

        /// <summary>
        /// The center of a graph is the vertex or node which is connected to all the other vertices or node
        /// directly or indirectly
        /// </summary>
        /// <returns></returns>
        public int Center()
        {
            return -1;
        }

        /// <summary>
        /// Make a Breadth First Search in this graph starting from provided vertex. 
        /// BFS in graph searches for all the neighbours first then deeper last. 
        /// NB: the memory consumption for the queue can be very large if the tree is very wide
        /// </summary>
        /// <param name="startVertex">The starting vertex of the BFS</param>
        /// <returns>The list of order of visit for a BFS</returns>
        public List<int> BreadthFirstSearchR(int startVertex)
        {
            // Initialize
            foreach (KeyValuePair<int, Vertex> vertexKey in this.adjacencyList)
            {
                vertexKey.Value.IsVisited = false;
            }

            Queue<Vertex> vertexQueue = new Queue<Vertex>();
            List<int> list = new List<int>();

            if (!this.adjacencyList.ContainsKey(startVertex))
            {
                Console.WriteLine("BFS Error : the graph doesn't contains the vertex {0}", startVertex);
                return null;
            }

            vertexQueue.Enqueue(adjacencyList[startVertex]);
            //adjacencyList[startVertex].IsVisited = true;

            return BreadthFirstSearchRecursive(vertexQueue, list);

        }
        /// <summary>
        /// The recursive method for a BFS.
        /// </summary>
        /// <param name="queue">The list of direct neighbours to visit first</param>
        /// <param name="list">The list of already visited vertices</param>
        private List<int> BreadthFirstSearchRecursive(Queue<Vertex> queue, List<int> list)
        {
            if (queue.Count == 0)
            {
                Console.Write("Graph BFS visit order starting from {0}: ", list[0]);
                Console.WriteLine(string.Join(", ", list));
                return list;
            }
            Vertex currentVertex = queue.Dequeue();
            list.Add(currentVertex.value);
            currentVertex.IsVisited = true;

            foreach (KeyValuePair<int, Vertex> vertexKey in currentVertex.connections)
            {
                if (!vertexKey.Value.IsVisited)
                {
                    queue.Enqueue(vertexKey.Value); //BFS => Priority is the NEIGHBOURS (currentVertex.connections) !
                }
            }
            return BreadthFirstSearchRecursive(queue, list);
        }

        /// <summary>
        /// PostOrder Depth First Search: start from the leaf nodes from left to right then go up to a parenr
        /// </summary>
        /// <param name="startVertex">The starting vertex of the PostOrder DFS</param>
        /// <returns>The list of order of visit for a PostOrder DFS</returns>
        public List<int> DFSPostOrder(int startVertex)
        {
            // Initialize
            foreach (KeyValuePair<int, Vertex> vertexKey in this.adjacencyList)
            {
                vertexKey.Value.IsVisited = false;
            }

            if (!this.adjacencyList.ContainsKey(startVertex))
            {
                Console.WriteLine("DFS PostOrder Error : the graph doesn't contains the vertex {0}", startVertex);
                return null;
            }

            List<int> list = new List<int>();
            this.adjacencyList[startVertex].IsVisited = true;
            list = DFSPostOrderRecursive(this.adjacencyList[startVertex], list);

            Console.Write("Graph DFS PostOrder visit: ");
            Console.WriteLine(string.Join(", ", list));
            return list;

        }
        /// <summary>
        /// The recursive method for an PreOrder DFS.
        /// </summary>
        /// <param name="vertexQueue">The list of deepest vertices first</param>
        /// <param name="list">The list of already visited vertices</param>
        private List<int> DFSPostOrderRecursive(Vertex currentVertex, List<int> list)
        {
            currentVertex.IsVisited = true;

            //  Deepest vertex first
            foreach (KeyValuePair<int, Vertex> vertexKey in currentVertex.connections)
            {
                if (!vertexKey.Value.IsVisited)
                {
                    DFSPostOrderRecursive(vertexKey.Value, list); //DFS PostOrder => Priority is the deepest vertices and the neighbours (currentVertex.connections)

                }
            }

            // Leaf node?
            list.Add(currentVertex.value);

            return list;
        }

        /// <summary>
        /// InOrder Depth First Search: start from deeper then go up to a parent
        /// </summary>
        /// <param name="startVertex">The starting vertex of the InOrder DFS</param>
        /// <returns>The list of order of visit for an InOrder DFS</returns>
        public List<int> DFSInOrder(int startVertex)
        {
            // Initialize
            foreach (KeyValuePair<int, Vertex> vertexKey in this.adjacencyList)
            {
                vertexKey.Value.IsVisited = false;
            }

            if (!this.adjacencyList.ContainsKey(startVertex))
            {
                Console.WriteLine("DFS InOrder Error : the graph doesn't contains the vertex {0}", startVertex);
                return null;
            }

            List<int> list = new List<int>();
            //this.adjacencyList[startVertex].IsVisited = true;
            list = DFSInOrderRecursive(this.adjacencyList[startVertex], list);

            Console.Write("Graph DFS InOrder visit: ");
            Console.WriteLine(string.Join(", ", list));
            return list;

        }

        /// <summary>
        /// The recursive method for an InOrder DFS.
        /// </summary>
        private List<int> DFSInOrderRecursive(Vertex currentVertex, List<int> list)
        {
            currentVertex.IsVisited = true;

            //  Deepest vertex first then the parent 
            foreach (KeyValuePair<int, Vertex> vertexKey in currentVertex.connections)
            {
                if (!vertexKey.Value.IsVisited)
                {
                    DFSInOrderRecursive(vertexKey.Value, list); //DFS InOrder => Priority is the deepest vertices ...
                                                                // ... and the parent node (currentVertex.connections)

                    // At this point, we traversed one deepest path(ie all intermediate nodes have been visited)
                    // We need to add the vertexKey to the list first
                    //list.Add(vertexKey.Value.value);

                    //vertexKey.Value.IsVisited = true;
                    //break;
                    //list.Add(currentVertex.value);
                    //urrentVertex.IsVisited = true;
                }
               
            }
            // At this point, we traversed one deepest path(ie all intermediate nodes have been visited)
            // We need to add the vertexKey to the list first
            if (!list.Contains(currentVertex.value))
            {
                list.Add(currentVertex.value);
            }

            return list; //Then we return to the stack under (currentVertex) that will continue to check all the non visited vertices again
        }







        /// <summary>
        /// Print all the connections for this graph
        /// </summary>
        public void Print()
        {
            if (this.adjacencyList == null)
                return;

            Console.WriteLine("Graph connections: ");
            foreach (KeyValuePair<int, Vertex> vertex in adjacencyList)
            {
                Console.Write("Vertex {0} ---> ", vertex.Key);
                if (vertex.Value.connections.Count == 0)
                {
                    Console.Write("No connections");
                }
                else
                {
                    foreach (int connectedKey in vertex.Value.connections.Keys)
                    {
                        Console.Write(connectedKey + ", ");
                    }
                }
                Console.WriteLine("");
            }
        }
    }

    /// <summary>
    /// A vertex (Node) prototype for graphs that implement BFS and DFS
    /// </summary>

    public class Vertex
    {
        /// <summary>
        /// The value of this vertex
        /// </summary>
        public int value;
        /// <summary>
        /// The keyvalue pair int,Vertex connections of this vertex
        /// </summary>
        public Dictionary<int, Vertex> connections;
        /// <summary>
        /// Is this vertex already visited?
        /// </summary>
        public bool IsVisited { get; set; }


        public Vertex(int value)
        {
            this.value = value;
            this.IsVisited = false;
            this.connections = new Dictionary<int, Vertex>();
        }

        /// <summary>
        /// Add a connection using key value pair <int,Vertex> to the dictionary
        /// </summary>
        public void AddConn(int key, Vertex vertex)
        {
            connections.Add(key, vertex);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //   0      1
            //    \    /
            //      2-3
            //    /    \
            //   5      4
            Graph graph = new Graph();
            //int[] A = new int[5] { 0, 1, 2, 4, 5 };
            //int[] B = new int[5] { 2, 3, 3, 3, 2 };

            int[] A = new int[10] { 9, 9, 4, 4, 20, 20, 15, 15, 170, 170 };
            int[] B = new int[10] { 4, 20, 1, 6, 15, 170, 13, 19, 21, 1500 };

            for (int i = 0; i < A.Length; i++)
            {
                graph.AddVertex(A[i]);
            }
            for (int i = 0; i < B.Length; i++)
            {
                graph.AddVertex(B[i]);
                graph.AddEdge(A[i], B[i]);
            }

            //graph.AddEdge(0, 2);
            //graph.AddEdge(1, 3);
            //graph.AddEdge(2, 3);
            //graph.AddEdge(4, 3);
            //graph.AddEdge(5, 2);


            graph.Print();

            graph.BreadthFirstSearchR(0); //0,2,3,5, 1,4
            graph.BreadthFirstSearchR(3); //3,1,2,4, 0,5
            graph.BreadthFirstSearchR(5); //5,2,0, 3,1,4

            graph.DFSInOrder(9);
            graph.DFSPostOrder(9);

        }
    }
}
