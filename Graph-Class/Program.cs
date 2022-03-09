using System;
using System.Collections;
using System.Collections.Generic;

namespace Graph_Class
{
    class Graph
    {
        int nbNodes;
        Dictionary<int, Connection> adjacencyList;
        public Graph()
        {
            this.nbNodes = 0;
            this.adjacencyList = new Dictionary<int, Connection>();
        }

        /// <summary>
        /// Add a vertex or a node to this graph.The node will be stored as a Key in an hash table
        /// </summary>
        public void AddVertex(int newVertexKey)
        {
            if (!this.adjacencyList.ContainsKey(newVertexKey))
            {
                this.adjacencyList.Add(newVertexKey, new Connection());
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
            if (!this.adjacencyList.ContainsKey(vertexA))
            {
                Console.WriteLine("Vertex '{0}' doesn't exists on the graph!", vertexA);
                failedToAddEdgeCount++;
            }
            if (!this.adjacencyList.ContainsKey(vertexB))
            {
                Console.WriteLine("Vertex '{0}' doesn't exists on the graph!", vertexB);
                failedToAddEdgeCount++;
            }
            if (failedToAddEdgeCount > 0)
            {
                return;
            }

            //  Add edge operation
            this.adjacencyList[vertexA].AddConn(vertexB, vertexB); //Note that the connection key is the same as the value
            this.adjacencyList[vertexB].AddConn(vertexA, vertexA); //Note that the connection key is the same as the value

        }

        /// <summary>
        /// Print all the connections for this graph
        /// </summary>
        public void Print()
        {
            if (this.adjacencyList == null)
                return;

            Console.WriteLine("Graph connections: ");
            foreach (KeyValuePair<int, Connection> vertex in adjacencyList)
            {
                Console.Write("Vertex {0} ---> ", vertex.Key);
                if(vertex.Value.connections.Count == 0)
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

    internal class Connection
    {
        public Dictionary<int, int> connections;

        public Connection()
        {
            this.connections = new Dictionary<int, int>();
        }

        /// <summary>
        /// Add a connection using key value pair <int,int> to the dictionary
        /// </summary>
        public void AddConn(int key, int value)
        {
            connections.Add(key, value);
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
            int[] A = new int[5] { 0, 1, 2, 4, 5 };
            int[] B = new int[5] { 2, 3, 3, 3, 2 };

            for (int i = 0; i < A.Length; i++)
            {
                graph.AddVertex(A[i]);
            }
            for (int i = 0; i < B.Length; i++)
            {
                graph.AddVertex(B[i]);
            }

            graph.AddEdge(0, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            graph.AddEdge(4, 3);
            graph.AddEdge(5, 2);


            graph.Print();
        }
    }
}
