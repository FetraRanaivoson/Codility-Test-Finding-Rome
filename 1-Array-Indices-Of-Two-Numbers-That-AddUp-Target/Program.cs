using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _1_Array_Indices_Of_Two_Numbers_That_AddUp_Target
{
    class Program
    {
        static void Main(string[] args)
        {
            //Given an array of integers, return the indices
            //of the two numbers that add up to a given target

            int[] A = { 1, 3, 7, 9, 2 };
            int target = 16;


            //1-Constaints:
            //Integers: positive
            //No duplicate
            //There may not always be a solution: empty array ... -> return null
            //Mutliple pair? only one pair add up to the target

            //2-Test cases:
            //Best case test case: t= 11 => 9+2 = 11 => [3,4]
            //int[] A = { 1, 3, 7, 9, 2 } t = 25 => null
            //int[] A = { } t = 1 => null
            //int[] A = { 5} t = 5 => null
            //int[] A = { 1,6} t = 6 => [1,2]

            //3-Figure out solution without code logically and critically:
            //int[] A = { 1, 3, 7, 9, 2 } t = 11: 9 + 2 = 11 => [3,4]
            //Try all possible pairs for each item
            //numberToFind = target - A[P1]
            //Eg: 11 - 1 = 10
            //P2 starting at index 1 will try to find 10 now by scanning all the rest of the array
            //If we do not find 10, there is no possibility that 1 is part of the answer

            //178000 ticks
            var sw = Stopwatch.StartNew();
            int[] solution = Solution(A, target);
            if (solution == null)
                Console.WriteLine("Null");
            else
                Console.WriteLine("With Naive solution: " + String.Join(",", solution));
            Console.WriteLine(sw.ElapsedTicks);

            //50000 ticks
            var sw2 = Stopwatch.StartNew();
            int[] solution2 = Solution2(A, target);
            if (solution2 == null)
                Console.WriteLine("Null");
            else
                Console.WriteLine("Hash table solution: " + String.Join(",", solution2));
            Console.WriteLine(sw2.ElapsedTicks);



        }

        private static int[] Solution(int[] a, int target) // O(n^2) time complexity
        {
            for (int i = 0; i < a.Length; i++)
            {
                int numberToFind = target - a[i];

                for (int j = 1; j < a.Length; j++)
                {
                    if (a[j] == numberToFind)
                        return new int[] { i, j };
                }
            }
            return null;
        }

       
        private static int[] Solution2(int[]a, int target) // O(n) time complexity
        {
            Dictionary<int, int> dico = new Dictionary<int, int>(); //At the cost of O(n) space complexity
            for (int i = 0; i < a.Length; i++)
            {
                dico.Add(a[i], i); //Where the Key is the ITEM VALUE and the value is the INDEX
            }

            //0+11:  i + (target - a[i]
            //1+10:  i + (target - a[i]
            //2+9: etc
            //3+8
            //4+7
            //5+6
            for (int i = 0; i < a.Length; i++)
            {
                if(dico.ContainsKey(target - a[i]))
                {
                    //return new int[] {a[i], dico[target - a[i]] };
                    return new int[] {i, dico[target - a[i]] }; //Remember to return the index
                }
            }
            return null;
        }
    }
}
