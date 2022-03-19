using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Recursive_Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            double number = 50;

            //  100000 - 300000 ticks
            var sw = Stopwatch.StartNew();
            Console.WriteLine("Factorial of {0} is: {1}", number, FactorialRecursive(number));
            Console.WriteLine("Recursion time: {0}", sw.ElapsedTicks);
            Console.WriteLine("======================================");

            //  1400-1700 ticks
            var sw1 = Stopwatch.StartNew();
            Console.WriteLine("Factorial of {0} is: {1}", number, FactorialRecursiveSimpleAndClean(number));
            Console.WriteLine("Recursive simple and clean time: {0}", sw1.ElapsedTicks);
            Console.WriteLine("======================================");

            //  900 - 1500 ticks
            var sw2 = Stopwatch.StartNew();
            Console.WriteLine("Factorial of {0} is: {1}", number, FactorialIterative(number));
            Console.WriteLine("Iterative time: {0}", sw2.ElapsedTicks);
            Console.WriteLine("======================================");

            // 80000 - 90000 ticks
            var sw3 = Stopwatch.StartNew();
            Console.WriteLine("Factorial of {0} is: {1}", number, FactorialMemoization(number));
            Console.WriteLine("Memoization time: {0}", sw3.ElapsedTicks);
            Console.WriteLine("======================================");

            // 700 - 1000
            var sw4 = Stopwatch.StartNew();
            Console.WriteLine("Factorial of {0} is: {1}", number, FactorialMemoization(number));
            Console.WriteLine("Memoization time after caching: {0}", sw4.ElapsedTicks);
            Console.WriteLine("======================================");
        }

        private static double FactorialMemoization(double number)
        {
            Dictionary<double, double> memoizer = new Dictionary<double, double>();

            Func<double, double> factorial = null;
            factorial = delegate (double number)
            {
                if (memoizer.ContainsKey(number))
                {
                    return memoizer[number];
                }
                else
                {
                    if (number < 2)
                    {
                        return number;
                    }
                    else
                    {
                        memoizer[number] = number * factorial(number - 1);
                        return memoizer[number];
                    }
                }
            };
            return factorial.Invoke(number);
        }



        private static double FactorialIterative(double number) // O(N)
        {
            double result = 1;
            //double counter = number;
            //for (int i = 1; i <= counter; i++)
            for (double i = number; i > 1; i--)
            {
                //result = result * number;
                result = result * i;
                //if (i == counter)
                //{
                    //return result;
                //}
                //number--;
            }
            return result;
        }

        private static double FactorialRecursive(double number)
        {
            double result = 1;
            double counter = number;
            return Factorial(number, counter, result);
        }

        private static double Factorial(double number, double counter, double result) //O(N) 
        {
            if (counter == 0)
            {
                return result;
            }

            result = result * number;
            counter--;
            number--;
            return Factorial(number, counter, result);
        }

        private static double FactorialRecursiveSimpleAndClean(double number) //O(N)
        {
            //  Base case: the stop
            if(number < 2)
            {
                return 1;
            }

            //  Recursive case: the recall
            return number * FactorialRecursiveSimpleAndClean(number - 1);
        }
    }
}
