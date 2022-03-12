using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> unsorted = new List<int>() { 0, 4, 3, -1, 3, 78, 5, 6, 2, -128, -44, 36, -4, -300 };

            //unsorted.Sort();
            //Console.WriteLine("Sorted array using built in tool : {0}", string.Join(", ",unsorted)); 

            //Console.WriteLine("Bubble sorted array : {0}", string.Join(", ", BubbleSort(unsorted)));
            //Console.WriteLine("Bubble sorted array inverse : {0}", string.Join(", ", BubbleSortInverse(unsorted)));
            //Console.WriteLine("Bubble sorted array recursive : {0}", string.Join(", ", BubbleSortRecursion(unsorted)));

            Console.WriteLine("Selection sorted array : {0}", string.Join(", ", SelectionSort(unsorted)));

        }

        private static List<int> SelectionSort(List<int> unsorted)
        {
            //  RED_INDEX = THE INDEX OF THE "SMALLER" VALUE (We suppose that the first index is the smaller value)
            //  BLUE_INDEX = THE INDEX OF THE VALUE NEXT TO THE RED INDEX (RED INDEX++)
            //  The idea is to find the SMALLEST VALUE after the RED_INDEX and INSERT it back at the beginning or after all
            //  the smaller values at the beginning

            int insertionIndex = 0;

            for (int RED_INDEX = 0; RED_INDEX < unsorted.Count; RED_INDEX++)
            {
                //  Always check from RED_INDEX up
                for (int BLUE_INDEX = RED_INDEX +1; BLUE_INDEX < unsorted.Count; BLUE_INDEX++)
                {
                    //  As soon as current value > next value, break this so that on the next outer iteration,
                    //  the RED_INDEX WILL BE THE BLUE INDEX
                    if (unsorted[RED_INDEX] > unsorted[BLUE_INDEX])
                    {
                        //Technically RED_INDEX = BLUE_INDEX but because the outer for loop will increment, we need to counter that!
                        RED_INDEX = BLUE_INDEX-1; 
                        break;
                    }

                    //  Else if current value < next, we need to BUBBLE UP OUR COMPARISON TILL THE END
                    else if(unsorted[RED_INDEX] < unsorted[BLUE_INDEX])
                    {
                        //  If we found out that the value on the RED_INDEX is SMALLER than all the others starting from [BLUE_INDEX +1]
                        if (BLUE_INDEX == unsorted.Count - 1)
                        {
                            //  INSERT that element at the beginning of the list
                            int toBubbleUp = unsorted[RED_INDEX];
                            unsorted.RemoveAt(RED_INDEX);

                            //  Insertion is after each smaller ones. We insert at 0 by default and increment the insertion index as we insert elements
                            unsorted.Insert(insertionIndex, toBubbleUp);

                            //  And the next RED_INDEX (default smaller value) will be at AT [INSERTION INDEX + 1]
                            RED_INDEX = insertionIndex; //Techincally [INSERTION INDEX + 1] but this will increment by 1 on the next outer loop!

                            //  Finally increment for the next INSERTION INDEX
                            insertionIndex++;
                        }
                    }
                }

                //  Particular case when we are comparing the last 2 values and the last element is less than the last element-1
                if (RED_INDEX == unsorted.Count-2 && unsorted[RED_INDEX] > unsorted[RED_INDEX + 1])
                {
                    int toBubbleUp = unsorted[RED_INDEX+1];
                    unsorted.RemoveAt(RED_INDEX+1);
                    unsorted.Insert(insertionIndex, toBubbleUp);
                    insertionIndex++;
                    RED_INDEX = insertionIndex - 1;
                }
            }

            return unsorted;
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
