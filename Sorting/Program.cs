using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> unsorted = new List<int>() { 0, 4, 3, -1, 3, 78, 5, 6, 2, -44, 36 };

            //unsorted.Sort();
            //Console.WriteLine("Sorted array using built in tool : {0}", string.Join(", ",unsorted)); 
            //Console.WriteLine("Bubble sorted array : {0}", string.Join(", ", BubbleSort(unsorted)));
            //Console.WriteLine("Bubble sorted array inverse : {0}", string.Join(", ", BubbleSortInverse(unsorted)));
            Console.WriteLine("Bubble sorted array recursive : {0}", string.Join(", ", BubbleSortRecursion(unsorted)));

        }
        private static List<int> BubbleSort(List<int> unsorted)
        {
            for (int i = 0; i < unsorted.Count; i++)
            {
                for (int k = 0; k < unsorted.Count; k++)
                {
                    if (k != unsorted.Count - 1)
                    {
                        int first = unsorted[k];  //this is the current item
                        int second = unsorted[k + 1];
                        if (first > second)
                        {
                            unsorted[k] = second;
                            unsorted[k + 1] = first;   //the current item is moving
                        }
                    }
                }//At the end of this loop, we should have resolve the case of 'first'which is always moving (bubbling)
            }//At the end of this should resolve all cases
            return unsorted;
        }
        private static List<int> BubbleSortInverse(List<int> unsorted)
        {
            for (int i = 0; i < unsorted.Count; i++)
            {
                for (int k = 0; k < unsorted.Count; k++)
                {
                    if (k != unsorted.Count - 1)
                    {
                        int first = unsorted[k];  //this is the current item
                        int second = unsorted[k + 1];
                        if (first < second)
                        {
                            unsorted[k] = second;
                            unsorted[k + 1] = first;   //the current item is moving because it becomes in k+1 index and so we always test for it in the k for loop
                        }
                    }
                }//At the end of this loop, we should have resolve the case of 'first'which is always moving (bubbling)
            }//At the end of this should resolve all cases
            return unsorted;
        }

        private static List<int> BubbleSortRecursion(List<int> unsorted)
        {
            if (unsorted.Count <= 1)
            {
                return unsorted;
            }

            int first = unsorted[unsorted.Count-1];
            int second = unsorted[unsorted.Count-2];
            List<int> append = new List<int>();
            if (first > second)
            {
                unsorted[unsorted.Count-1] = second;
                unsorted[unsorted.Count-2] = first;
                append.Add(unsorted[unsorted.Count-1]);
                unsorted.RemoveAt(unsorted.Count-1);
            }
            else
            {
                append.Add(first);
                unsorted.RemoveAt(unsorted.Count-1);
            }

            

            return  BubbleSortRecursion(unsorted);
        }

    }
}
