using System;

namespace Recursive_Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            double number = 12;
            Console.WriteLine("Factorial of {0} is: {1}", number, FactorialRecursive(number));
            //Console.WriteLine("Factorial of {0} is: {1}", number, FactorialIterative(number));
        }

        private static int FactorialIterative(double number)
        {
            throw new NotImplementedException();
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

            result =  result * number;
            counter--;
            number--;
            return Factorial(number, counter, result);
        }
    }
}
