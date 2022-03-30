using System;
using System.Collections.Generic;
using Graph_Class;
using Binary_Tree;

namespace Sorted_Array_To_BST
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Console.WriteLine("Constructed BST from: {0} ", String.Join(",", array));

            SortedArrayToBST(array);
        }

        private static void SortedArrayToBST(int[] array)
        {
            if (array.Length == 0)
                return ;
            BinarySearchTree tree = new BinarySearchTree();
            CreateNode(tree, array, 0, array.Length - 1);
            tree.Print();
        }

        private static Node CreateNode(BinarySearchTree tree, int[] array, int low, int high)
        {
            if (low > high)
                return null;
            int mid = low + (high - low) / 2;

            Node node = new Node(array[mid]);
            tree.Insert(node.value);
            /*tree.Lookup(node.value).left = */CreateNode(tree, array, low, mid - 1);
           /* tree.Lookup(node.value).right = */CreateNode(tree, array, mid+1, high);

            return node;
        }

        private static void ArrayToBST(int[] array)
        {
            if (array.Length == 0)
                return;

            Graph graph = new Graph();
            BinarySearch(array, graph);

            graph.Print();
            graph.BreadthFirstSearchR(array[(array.Length - 1) / 2]);
        }

        private static int BinarySearch(int[] array, Graph graph)
        {
            int midIndex = (array.Length) / 2;

            int mid = array[midIndex];
            graph.AddVertex(mid);

            if (array.Length == 1)
                return mid;

            int lSize = midIndex;
            int rSize = array.Length - (midIndex + 1);

            int[] left = new int[0];
            int[] right = new int[0];

            if (lSize > -1)
                left = new int[lSize];
            if (rSize > -1)
                right = new int[rSize];
            if (lSize == -1)
                lSize = 0;
            if (rSize == 1)
                rSize = 0;

            Array.Copy(array, 0, left, 0, lSize);
            Array.Copy(array, midIndex + 1, right, 0, rSize);

            graph.AddEdge(mid, BinarySearch(left, graph), true);
            graph.AddEdge(mid, BinarySearch(right, graph), true);

            return mid;
        }
    }
}
