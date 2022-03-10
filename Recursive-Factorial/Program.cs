using System;

namespace Recursive_Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            double number = 50;
            Console.WriteLine("Factorial of {0} is: {1}", number, FactorialRecursive(number));
            Console.WriteLine("Factorial of {0} is: {1}", number, FactorialIterative(number));
        }

        private static double FactorialIterative(double number)
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

        private static double Factorial(double number, double counter, double result)
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
    }
}
