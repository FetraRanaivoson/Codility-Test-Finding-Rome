using System;
using System.Collections.Generic;

namespace _1_FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            //Write a program that outputs the string 
            //representation of numbers from 1 to n.
            //But for multiples of three it should print
            //"Fizz" and for the multiples of five print
            //"Buzz". For multiples of both three and five print
            //"FizzBuzz"

            int N = 15;

            Console.WriteLine(String.Join(", ", FizzBuzz(N)));
        }

        private static List<string> FizzBuzz(int n)
        {
            List<string> result = new List<string>();

            for (int i = 1; i <= n; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                    result.Add("FizzBuzz");
                else if (i % 3 == 0)
                    result.Add("Fizz");
                else if (i % 5 == 0)
                    result.Add("Buzz");
                else
                    result.Add(i.ToString());
                
            }


            return result;
        }
    }
}
