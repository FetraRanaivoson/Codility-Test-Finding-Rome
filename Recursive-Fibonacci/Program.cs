using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Recursive_Fibonacci
{
    /// <summary>
    /// Memoization is a technique for improving performance 
    /// by caching the return values of expensive function calls. 
    /// </summary>
    public class Memoizer
    {
        Dictionary<int, int> memoizeValues;
        public Memoizer()
        {
            memoizeValues = new Dictionary<int, int>();
        }
        /// <summary>
        /// Returns true if the key n has a cache value in this Memoizer.
        /// </summary>
        public bool Contains(int n)
        {
            return memoizeValues.ContainsKey(n);
        }

        /// <summary>
        /// Returns the cached value of the key n is this Memoizer.
        /// </summary>
        public int ValueOf(int n)
        {
            return memoizeValues[n];
        }

        /// <summary>
        /// Create the value of the given key n and assign its value in this Memoizer.
        /// </summary>
        public void CreateValueOf(int n, int value)
        {
            memoizeValues.Add(n, value);
        }

        /// <summary>
        /// Set the value of the given key n in this Memoizer.
        /// </summary>
        public void SetValueOf(int n, int value)
        {
            memoizeValues[n] = value;
        }
    }

    class Program
    {
        public delegate int Memoization(int n);

        static void Main(string[] args)
        {
            //  The first 20 Fibonacci sequence
            DisplayParameterFirstFibonacciSequence(20);

            //  Given an index N return the value in the
            //  Fibonacci sequence associated with that index:
            //v 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, ...
            //N 0, 1, 2, 3, 4, 5, 6,  7,  8,  9, 10, 11, 12, ...                
            //  =>Each value is the sum of the two previous values

            int N = 12;

            var sw = Stopwatch.StartNew();
            Console.WriteLine("The index {0} in the Fibonacci sequence using Recursion is of value {1}",
               N, FibonacciRecursive(N));
            Console.WriteLine("Recursion time: {0}", sw.ElapsedTicks);
            Console.WriteLine("======================================");

            var sw2 = Stopwatch.StartNew();
            Console.WriteLine("The index {0} in the Fibonacci sequence using Iteration is of value {1}",
            N, FibonacciIterative(N));
            Console.WriteLine("Iterative time: {0}", sw2.ElapsedTicks);
            Console.WriteLine("======================================");

            var sw3 = Stopwatch.StartNew();
            Console.WriteLine("The index {0} in the Fibonacci sequence before caching is of value {1} (Memoization method)",
            N, FibonacciMemoization(N));
            Console.WriteLine("Memoization time BEFORE CACHING is: {0}", sw3.ElapsedTicks);
            Console.WriteLine("======================================");

            N = 12;
            var sw4 = Stopwatch.StartNew();
            Console.WriteLine("The index {0} in the Fibonacci sequence after caching is of value {1} (Memoization method)",
            N, FibonacciMemoization(N));
            Console.WriteLine("Memoization time AFTER CACHING is: {0}", sw4.ElapsedTicks);
            Console.WriteLine("======================================");
        }

        private static int FibonacciMemoization(int n)
        {
            Memoizer memoizer = new Memoizer();

            for (int i = 0; i <= n; i++)
            {
                if (memoizer.Contains(n))
                {
                    return memoizer.ValueOf(n);
                }

                if (i < 2)
                {
                    memoizer.CreateValueOf(i, i);
                }

                if(memoizer.Contains(i-1) && memoizer.Contains(i - 2))
                {
                    /// Long time code here ///
                    memoizer.CreateValueOf(i, memoizer.ValueOf(i - 1) + memoizer.ValueOf(i - 2));
                }
            }
            return memoizer.ValueOf(n);
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
