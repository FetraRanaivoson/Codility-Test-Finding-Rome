using System;
using System.Collections;
using System.Collections.Generic;

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

            //  Hashtable
            //Hashtable cities = new Hashtable();
            //cities.Add(1, true);
            //cities.Add(2, false);
            //cities.Add(3, false);
            //cities.Add(4, false);

            //cities[3] = true;

            //foreach (DictionaryEntry item in cities)
            //{
            //    Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            //}
            //foreach (DictionaryEntry item in cities)
            //{
            //    Console.WriteLine("There is a key: {0}", item.Key);
            //}

            //  Dictionnary
            //Dictionary<int, bool> citiesDico = new Dictionary<int, bool>();
            //citiesDico.Add(1, true);
            //citiesDico.Add(2, false);
            //citiesDico.Add(3, false);
            //citiesDico.Add(4, false);

            //citiesDico[4] = true;

            //foreach (KeyValuePair<int,bool> item in citiesDico)
            //{
            //    Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            //}

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

            //for (int i = 0; i < B.Length; i++)
            //{
            //    mergedArray[A.Length + i] = B[i];
            //}
            // OR
            B.CopyTo(mergedArray, A.Length);

            //  1st method
            for (int i = 0; i < mergedArray.Length; i++)
            {
                for (int test = 0; test < mergedArray.Length; test++)
                {
                    if (mergedArray[i] < mergedArray[test])
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
