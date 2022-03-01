using System;

namespace TimeComplexity_FrogJump
{
    class Program
    {
        static void Main(string[] args)
        {
            int X = 10;
            int Y = 85;
            int D = 30;
            solution(X, Y, D);
        }

        public static int solution(int X, int Y, int D)
        {
            float jumpCount = 0;

            if (X > Y)
            {
                return (int)jumpCount;
            }

            //  This will run more often as Y - X increases AND as D is little
            //  But it is not effecient O(n) because we are relying on the while loop
            //while (X < Y)
            //{
            //    X += D;
            //    jumpCount++;
            //    if (X >= Y)
            //    {
            //        break;
            //    }
            //}

            //  What if we just calculate the number of jumps needed to reach Y
            //  More efficient because only one operation: O(1): X + jumpCount * D = Y ?
            jumpCount = (float)(Y - X) / D;
            jumpCount = MathF.Ceiling(jumpCount);

            Console.WriteLine(jumpCount);

            return (int)jumpCount;
        }

    }
}
