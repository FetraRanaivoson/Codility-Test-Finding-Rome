using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Graph_Class;

namespace RomeFinding
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test 1
            //int[] A = new int[3] { 1, 2, 3 };
            //int[] B = new int[3] { 0, 0, 0 };

            // Test 2
            int[] A = new int[5] { 0, 1, 2, 4, 5 };
            int[] B = new int[5] { 2, 3, 3, 3, 2 };

            Console.WriteLine("Rome city is: " + Solution(A, B));
        }

        public static int Solution(int[] A, int[] B)
        {
            int romeCity = -1;

            //   0      1
            //    \    /
            //      2-3
            //    /    \
            //   5      4
            Graph graph = new Graph();

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

            return graph.Center();
        }


    }
}
