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
        public void AddVertex(int newVertexKey)
        {
            if (!this.adjacencyList.ContainsKey(newVertexKey))
            {
                this.adjacencyList.Add(newVertexKey, new Connection());
                this.nbNodes++;
            }
        }

        public void AddEdge()
        {

        }

        public void Print()
        {
            if (this.adjacencyList == null)
                return;

            foreach (KeyValuePair<int, Connection> vertex in adjacencyList)
            {
                Console.Write("Vertex {0}: ", vertex.Key);
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
        /// Add a connection using key value pair <int,int> to the dictionnary
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

            graph.Print();
        }
    }
}
