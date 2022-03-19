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

            int N = 40;

            // The recursive runtine last around 11000000 ticks and can get bigger if N increase, worst can freeze
            var sw = Stopwatch.StartNew();
            Console.WriteLine("The index {0} in the Fibonacci sequence using Recursion is of value {1}",
               N, FibonacciRecursive(N));
            Console.WriteLine("Recursion time: {0}", sw.ElapsedTicks);
            Console.WriteLine("======================================");

            //  The iterative runtine last around 2000 ticks
            var sw2 = Stopwatch.StartNew();
            Console.WriteLine("The index {0} in the Fibonacci sequence using Iteration is of value {1}",
            N, FibonacciIterative(N));
            Console.WriteLine("Iterative time: {0}", sw2.ElapsedTicks);
            Console.WriteLine("======================================");

            // Always constant time around: 50000-70000 ticks even if N increases and never freeze: VERY FAST
            var sw3 = Stopwatch.StartNew();
            Console.WriteLine("The index {0} in the Fibonacci sequence before caching is of value {1} (Memoization method)",
            N, FibonacciMemoization(N));
            Console.WriteLine("Memoization time BEFORE CACHING is: {0}", sw3.ElapsedTicks);
            Console.WriteLine("======================================");

            // Retrieving the cached value: we always get around 500-1000 ticks, better than the iterative time if
            // we plan to calculate any already cached number in the future
            N = 40;
            var sw4 = Stopwatch.StartNew();
            Console.WriteLine("The index {0} in the Fibonacci sequence after caching is of value {1} (Memoization method)",
            N, FibonacciMemoization(N));
            Console.WriteLine("Memoization time AFTER CACHING is: {0}", sw4.ElapsedTicks);
            Console.WriteLine("======================================");
        }

        /// <summary>
        ///  O(n) time complexity but increased space complexity O(n)
        /// </summary>
        private static int FibonacciMemoization(int n)
        {
            //  USING Memoizer class and a for loop
            //Memoizer memoizer = new Memoizer();
            //for (int i = 0; i <= n; i++)
            //{
            //    if (memoizer.Contains(n))
            //    {
            //        return memoizer.ValueOf(n);
            //    }

            //    if (i < 2)
            //    {
            //        memoizer.CreateValueOf(i, i);
            //    }

            //    if (memoizer.Contains(i - 1) && memoizer.Contains(i - 2))
            //    {
            //        /// Long time code here ///
            //        memoizer.CreateValueOf(i, memoizer.ValueOf(i - 1) + memoizer.ValueOf(i - 2));
            //    }
            //}
            //return memoizer.ValueOf(n);

            //  USING Memoizer class and a recursion inside a closure with a lambda function
            //Memoizer memoizer = new Memoizer();
            //Func<int, int> fib = null;
            //fib = delegate (int n)
            //{
            //    if (memoizer.Contains(n))
            //    {
            //        return memoizer.ValueOf(n);
            //    }
            //    else
            //    {
            //        if (n < 2)
            //        {
            //            memoizer.CreateValueOf(n, n);
            //            return memoizer.ValueOf(n);
            //        }
            //        else
            //        {
            //            memoizer.SetValueOf(n, fib(n - 1) + fib(n - 2));
            //            return memoizer.ValueOf(n);
            //        }
            //    }
            //};
            //return fib.Invoke(n);

            //  USING directly a dictionnary and a recursion inside a closure with a lambda function
            Dictionary<int, int> memoizer = new Dictionary<int, int>();
            Func<int, int> fib = null;
            fib = delegate (int n)
            {
                if (memoizer.ContainsKey(n))
                {
                    return memoizer[n];
                }
                else
                {
                    if (n < 2)
                    {
                        return n;
                    }
                    else
                    {
                        memoizer[n] = fib(n - 1) + fib(n - 2);
                        return memoizer[n];
                    }
                }
            };
            return fib.Invoke(n);
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

        /// <summary>
        /// O(2^n) time complexity 
        /// </summary>
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
