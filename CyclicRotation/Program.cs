using System;
using System.Linq;

namespace CyclicRotation
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = new int[] { 3, 8, 9, 7, 6 };
            int K = 3;

            Console.WriteLine(solution(A, K));
        }

        public static int[] solution(int[] A, int K)
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
