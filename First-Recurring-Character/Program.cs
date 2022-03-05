using System;
using System.Collections;
using System.Collections.Generic;

namespace First_Recurring_Character
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[] { 2, 5, 1, 2, 3, 5, 1, 2, 4 };

            Console.WriteLine("The first recurring character is: {0}", Solution(array));
        }

        private static int Solution(int[] array)
        {

            //  1-  First  naive solution is to create a class Visited that we can add 
            //  a visited item and
            //  have a method inside this class that loops through all the elements
            //  If the elements exists, then that is the first naive method
            //Visited visit = new Visited();
            //for (int i = 0; i < array.Length; i++) // O(N)
            //{
            //    if (visit.HasAlreadyVisited(array[i])) // O(N)
            //    {
            //        return array[i];
            //    }
            //}
            // ==> O(N^2): expensive if we increase the array's elements


            //  2- The second solution is to add a hash table for each element we iterate of
            //  If throughout the iteration O(N), we find out that the key already exists then that means
            //  that the value associated with that key has already been visited
            //  To make our life easier we will create a key value pair of int,int: we know exaclty
            //  that the key (number) is the value (number)
            //  Hash table => O(1): look up: values in memory are accessible directly via unique keys
            //  ==> O(N+1) equivalent to O(N) which is MUCH BETTER than O(N^2)
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < array.Length; i++) // O(N)
            {
                if (hashtable.ContainsKey(array[i])){ // O(1)
                    return array[i];
                }
                hashtable.Add(array[i], array[i]); // O(1)
            }
            //  ==> O(N*(1+1)) ==> O(N)
            return -1;
        }

        class Visited
        {
            List<int> visited;
            public Visited()
            {
                visited = new List<int>();
            }
            public bool HasAlreadyVisited(int toAdd)
            {
                if (visited.Contains(toAdd)) //O(N)
                {
                    return true;
                }
                visited.Add(toAdd);
                return false;
            }
        }
    }
}
