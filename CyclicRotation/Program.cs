using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CyclicRotation
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = new int[] { 3, 8, 9, 7, 6 };
            int K = 3;

            //  Naive solution using 2 for loops by shifting each elements in the
            //  int the array K*Number of elements time => O(K*A)
            //Console.WriteLine(NaiveSolution(A, K));

            //  We can also use linked lists to solve this problem
            //  NB: Using linked lists is good except we will have to traverse all the elements
            //  to get the last one (O(N)) => O(K*A)
            Console.WriteLine(LinkedListSolution(A, K));
        }

        private static int[] LinkedListSolution(int[] A, int K)
        {
            LinkedList<int> linkedList = new LinkedList<int>(A); // O(N) space complexity

            for (int i = 0; i < K; i++) // O(K)
            {
                LinkedListNode<int> last = linkedList.Last; //O(A): traversing a linked list is an O(N) operation to get the last item
                linkedList.RemoveLast(); //O(A): traversing a linked list is an O(N) operation to get the last item
                linkedList.AddFirst(last);
            } // => O(k*A)

            return linkedList.ToArray<int>();// + O(A)
        }

        public static int[] NaiveSolution(int[] A, int K)
        {
            // Store the last element of the array
            int temp = A[A.Length - 1];

            // for K times
            for (int i = 0; i < K; i++)
            {
                // for each element of the array, move each element to right
                // except for the first element
                for (int index = A.Length - 1; index > 0; index--)
                {
                    A[index] = A[index - 1];
                }

                // Replace the first element by the stored element
                A[0] = temp;

                // Do not forget to reset the temp to the new last element of the array
                temp = A[A.Length - 1];
            }

            Console.Write("[");
            //for (int i = 0; i < A.Length; i++)
            //{
            //    if (i != A.Length - 1)
            //        Console.Write(A[i] + ", ");
            //    else
            //        Console.Write(A[i]);
            //}
            Console.Write(string.Join(", ", A));
            Console.WriteLine("]");


            return A;
        }
    }
}
