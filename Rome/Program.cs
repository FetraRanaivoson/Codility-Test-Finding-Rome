using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Graph_Class;

namespace RomeFinding
{
    class Program
    {
        /// <summary>
        /// You are given a map of the Roman Empire. There are
        /// N+1 cities (numbered from 0 to N) and N directed
        /// roads between them. The road network is connected; that
        /// is, ignoring the directions of roads, there is a route
        /// between each pair of cities.
        /// 
        /// The capital of the Roman Empire is Rome. We all know that
        /// all roads lead to Rome. This means that there is a route
        /// from each city to Rome. Your task is to find Rome on the map,
        /// or decide that it is not there.
        /// 
        /// The roads are described by two arrays A and B of N integers each.
        /// For each integer K(0 lessOrEqual than K lessOrEqual than N), there exists a road from city
        /// A[K] to city B[K].
        /// 
        /// Write a function solution (A,B) that given two arrays A and B,
        /// returns the number of city which is Rome (the city that can be reached
        /// from all other cities). If no such city exists, your function should
        /// return -1
        /// </summary>

        static void Main(string[] args)
        {
            // 1st case: should returns 0 (0 is Rome)
            //  2
            //   \
            //    ->0<-3
            //   /
            //  1
            //int[] A = new int[3] { 1, 2, 3 };
            //int[] B = new int[3] { 0, 0, 0 };

            // 2nd case: should returns 3 (3 is Rome)
            //   0           4
            //    \         /
            //      ->2->3<-
            //    /         \
            //   5           1
            //int[] A = new int[5] { 0, 1, 2, 4, 5 };
            //int[] B = new int[5] { 2, 3, 3, 3, 2 };

            //  3rd case: should returns -1 (There is no Rome on the map)
            //  2->1<-3->0<-4
            //int[] A = new int[4] { 2, 3, 3, 4 };
            //int[] B = new int[4] { 1, 1, 0, 0 };

            //  If I were to make 1 as Rome, I need to connect 0 to 1
            //  2-> 1 <- 3     
            //      ^   / 
            //       \ /
            //        0<-4
            int[] A = new int[5] { 2, 3, 3, 4, 0 };
            int[] B = new int[5] { 1, 1, 0, 0, 1 };

            Console.WriteLine("Rome city is: " + Solution(A, B));
        }

        public static int Solution(int[] A, int[] B)
        {
            Graph graph = new Graph();

            for (int i = 0; i < A.Length; i++)
            {
                graph.AddVertex(A[i]);
            }
            for (int i = 0; i < B.Length; i++)
            {
                graph.AddVertex(B[i]);
                graph.AddEdge(A[i], B[i], true);
            }

            graph.Print();

            //  For each of a BFS parkour (starting from vertex0 and Rome include so to A.Length parkours),
            //  if we always ended up on the same vertex AT THE END, then that vertex is Rome 
            //  OR for each DFS POST ORDER parkour, if we always have the same vertex AT THE BEGINNING,
            //  then that vertex is ROME because there is a path and we always begin with that deep vertex
            List<int> parkoured = new List<int>();
            int parkourLength = A.Length;
            int vertex = 0;
            return DeepestCity(graph, vertex, parkoured, parkourLength);
        }

        /// <summary>
        /// Give the vertex value that can be reached by all the other vertices in the specified graph
        /// </summary>
        /// <param name="parkourLength">The max limit of the parkour recursion</param>
        /// <param name="parkoured">List of parkoured vertices from a vertex using a DFS PostOrder</param>
        /// <param name="graph">The graph that we work on</param>
        /// <param name="vertex">the vertex to start the DFS PostOrder algorithm</param>
        /// <returns>The Rome of the graph: the vertex value that can be reached by all the other vertices</returns>
        public static int DeepestCity(Graph graph, int vertex, List<int> parkoured, int parkourLength)
        {
            if (vertex >= parkourLength)
            {
                return parkoured[0];
            }

            parkoured = graph.DFSPostOrder(vertex);
            vertex++;

            if (parkoured[0] != DeepestCity(graph, vertex, parkoured, parkourLength))
            {
                return -1;
            }

            return parkoured[0];
        }
    }
}
