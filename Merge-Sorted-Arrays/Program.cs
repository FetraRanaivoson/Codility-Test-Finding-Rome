using System;

namespace Merge_Sorted_Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = new int[] { 0, 3, 4, 31 };
            int[] B = new int[] { 4, 6, 30 };

            Console.WriteLine("Merged sorted array is: "
                + String.Join(", ", MergedSortedArray(A, B)));
        }

        static int[] MergedSortedArray(int[] A, int[] B)
        {
            int[] mergedArray = new int[A.Length + B.Length];

            //  Copy array A to the new array
            //  N[0, 3, 4, 31, 0, 0, 0]
            A.CopyTo(mergedArray, 0);
            //for (int i = 0; i < mergedArray.Length; i++)
            //{
            //    Console.WriteLine(mergedArray[i]);
            //}

            for (int i = 0; i < B.Length; i++)
            {
                mergedArray[A.Length + i] = B[i];
            }

            for (int i = 0; i < mergedArray.Length; i++)
            {
                for (int test = 0; test < mergedArray.Length; test++)
                {
                    if (mergedArray[i]< mergedArray[test])
                    {
                        int temp = mergedArray[test];
                        mergedArray[test] = mergedArray[i];
                        mergedArray[i] = temp;
                    }
                }
            }
            // => O(n^2)

            return mergedArray;
        }
    }
}
