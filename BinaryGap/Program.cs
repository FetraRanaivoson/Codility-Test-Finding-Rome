using System;
using System.Collections.Generic;

namespace BinaryGap
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 328;

            Console.WriteLine($"The binary gap of {N} is {Solution(N)}");
        }

        public static int Solution(int N)
        {
            int result = 0;
            List<int> tempResults = new List<int>();

            string binary = GetBinary(N);
            Console.WriteLine(binary);

            List<Char> chars = new List<char>();
            foreach (char item in binary)
            {
                //Console.WriteLine(item);
                chars.Add(item);
            }

            if(chars[0] =='0' /*|| chars[chars.Count-1] == '0'*/)
            {
                result = 0;
                return result;
            }

            for (int i = 0; i < chars.Count; i++)
            {
                if (i != 0 || i != chars.Count - 1)
                {
                    if(chars[i] == '0')
                    {
                        result++;
                        continue;
                    }
                    else if (chars[i] != '0')
                    {
                        tempResults.Add(result);
                        result = 0;
                        continue;
                    }
                }
                if (result == 0)
                {
                    return result;
                }
            }

            if(tempResults.Count > 1)
            {
                return Maximum(tempResults);
            }
            return result;
        }

        private static int Maximum(List<int> tempResults)
        {
            int maximum = 0;
            for (int i = 0; i < tempResults.Count; i++)
            {
                int value = tempResults[i];
                if(value > maximum)
                {
                    maximum = value;
                }
            }
            return maximum;
        }

        private static string GetBinary(int N)
        {
            return Convert.ToString(N, 2);
        }
    }
}
