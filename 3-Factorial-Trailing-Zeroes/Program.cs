using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _3_Factorial_Trailing_Zeroes
{
    class Program
    {
        static void Main(string[] args)
        {
            //Given an integer n, return the 
            //number of trailing zeroes in n!
            //Solve it in O(log(n))

            int n = 30;

            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine("Number of trailing zeros of {0}!: {1}", n, TrailingZeroes(n));
            Console.WriteLine("Elapsed: {0}", sw.ElapsedTicks);
        }

        private static int TrailingZeroes(int n)
        {
            int tz = 0;

            if (n == 0)
                return tz;

            //Queue<int> fiveQ = new Queue<int>();
            //Queue<int> twoQ = new Queue<int>();

            while (n > 0)
            {
                n /= 5;
                tz += n;
                //if (n % 5 == 0 /*&& n % 2 == 0*/)
                //{
                //    fiveQ.Enqueue(5);
                //    //twoQ.Enqueue(2);
                //}
                ////else if (n % 5 == 0)
                //    //fiveQ.Enqueue(5);
                ////else if (n % 2 == 0)
                //    //twoQ.Enqueue(2);
                //
                //if(fiveQ.Count >= 1 /*&& twoQ.Count >= 1*/)
                //{
                //    fiveQ.Dequeue();
                //    //twoQ.Dequeue();
                //    tz++;
                //}
                //n--;
            }

            return tz;
        }
    }
}
