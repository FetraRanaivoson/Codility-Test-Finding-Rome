using System;
using System.Collections.Generic;

namespace Recursive_Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            //  The first 20 Fibonacci seq uence
            DisplayParameterFirstFibonacciSequence(20);

            //  Given an index N return the value in the
            //  Fibonacci sequence associated with that index:
            //v 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, ...
            //N 0, 1, 2, 3, 4, 5, 6,  7,  8,  9, 10, 11, 12, ...                
            //  =>Each value is the sum of the two previous values

            int N =3;

            Console.WriteLine("The index {0} in the Fibonacci sequence is of value {1}",
               N, FibonacciRecursive(N));
            Console.WriteLine("The index {0} in the Fibonacci sequence is of value {1}",
            N, FibonacciIterative(N));


        }

        private static int FibonacciIterative(int n)
        {
            if (n < 2)
            {
                return n;
            }
            else
            {
                int beforePrevious = 0;
                int previous = 1;

                for (int i = 2; i < n + 1; i++)
                {
                    int next = beforePrevious + previous;
                    if (i == n)
                        return next;
                    else
                    {
                        beforePrevious = previous;
                        previous = next;
                    }
                }
            }
            return -1;
        }


        private static int FibonacciRecursive(int n)
        {
            if (n < 2)
            {
                return n;
            }
            return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
        }

        private static void DisplayParameterFirstFibonacciSequence(int limit)
        {
            int beforePrevious = 0;
            int previous = 1;
            for (int i = 0; i < limit; i++)
            {
                if (i == 0)
                    Console.Write("{0}, ", beforePrevious);
                else if (i == 1)
                    Console.Write("{0}, ", previous);
                else
                {
                    int next = beforePrevious + previous;

                    if (i == limit - 1)
                    {
                        Console.Write("{0}", next);
                    }
                    else
                    {
                        Console.Write("{0}, ", next);
                    }

                    beforePrevious = previous;
                    previous = next;
                }
            }
            Console.WriteLine("");
        }
    }
}
