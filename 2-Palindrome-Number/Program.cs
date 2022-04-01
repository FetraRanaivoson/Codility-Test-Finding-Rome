using System;
using System.Collections.Generic;

namespace _2_Palindrome_Number
{
    class Program
    {
        static void Main(string[] args)
        {
            //Determine whether an integer is a palindrome
            //An integer is a palindrome when it reads the same
            //Backward and forward
            //Eg: 121 is a palindrome but -121 is not a palindrome

            int N = 1032301;

            Console.WriteLine("Is {0} a palindrome? {1}", N, IsPalindrome(N));

        }

        private static bool IsPalindrome(int n)
        {
            string list = n.ToString();
            int left = 0;
            int right = list.Length - 1;

            while (left <= right)
            {
                if (list[left] != list[right])
                    return false;
                else
                {
                    left++;
                    right--;
                }
            }
            return true;
        }
    }
}
