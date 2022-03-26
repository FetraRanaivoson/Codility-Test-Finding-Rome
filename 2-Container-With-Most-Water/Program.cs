using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Container_With_Most_Water
{
    class Program
    {
        static void Main(string[] args)
        {
            //You are given an array of positive integers
            //where each integer represents the height of a vertical
            //line on a chart.
            //Find two lines which together with the
            //x-axis forms a container that would hold the greatest
            //amount of water.
            //Return the area of water it would hold

            //     
            //     9
            //  8  |   8
            //  |**|   ^
            //  |6*|   |
            //  ||*|   |
            //  ||*|4  |
            //  ||*||  |
            //  ||2||  |
            // 1|||||  |
            // ||||||  |
            //  ->3
            // Area = 8 * 3 = 24

            //Does thickness of the lines affect the area? no, assume they take no space
            //Do the left and right sides of the  graph count as walls? no, the sides cannot be used to form a container
            //Does higher line inside the container affect our area? no, lines inside don't affect the area


            //Find the maximum index in x axis and maximum value in y axis
            //in a way that maximize diff(two extreme index) * A[extremityIndex], extremityIndex start with 0
            //if(A[maxIndex]>A[extremity] w= maxIndex - extremityIndex, h = A[extremityIndex] S= w*h
            //if(A[maxIndex]<A[extremity] w= maxIndex - extremityIndex, h = A[maxIndex] S = w*h

            //Extr = 0 MaxIndex = 5: 4>1 => w = 5-0 = 5, h= 1 => S= 5*1 = 5
            //Extr = 0 MaxIndex = 4: 9>1 => w = 4-0 = 4, h= 1 => S= 4*1 = 4
            //Extr = 0 MaxIndex = 3: 2>1 => w = 3-0 = 3, h= 1 => S= 3*1 = 3
            //Extr = 0 MaxIndex = 2: 6>1 => w = 2-0 = 2, h= 1 => S= 2*1 = 2
            //Extr = 0 MaxIndex = 1: 8>1 => w = 2-0 = 2, h= 1 => S= 2*1 = 2

            //Test cases
            //Best case: 8*3 = 24 index[3] = 9 > 8


            //int[] A = { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
            //int[] A = { 1, 1};
            //int[] A = { 2, 1 };
            //int[] A = { 1, 2, 4, 3 };
            //int[] A = { 2, 3, 4, 5, 18, 17, 6 };
            //int[] A = { 8, 20, 1, 2, 3, 4, 5, 6 };
            //int[] A = { 4, 4, 2, 11, 0, 11, 5, 11, 13, 8 };
            //int[] A = { 1, 3, 2, 5, 25, 24, 5 };
            //int[] A = { 1, 2, 3, 4, 5, 25, 24, 3, 4 };
            int[] A = { 3, 9, 3, 4, 7, 2, 12, 6 };

            //150000 ticks (19ms)
            var sw = Stopwatch.StartNew();
            Console.WriteLine("Greatest area of water (Bubble sort): {0}", MaxAreaContainer(A));
            Console.WriteLine(sw.ElapsedTicks);

            //2100 ticks (0ms)
            //var sw2 = Stopwatch.StartNew();
            //Console.WriteLine("Greatest area of water (O(n)): {0}", MaxAreaContainer2(A));
            //Console.WriteLine(sw2.ElapsedTicks);

            // ticks (ms)
            var sw3 = Stopwatch.StartNew();
            Console.WriteLine("Greatest area of water (): {0}", MaxAreaContainer3(A));
            Console.WriteLine(sw3.ElapsedTicks);

        }
         
        private static int MaxAreaContainer(int[] height) // O(n^2)
        {
            int maxArea = 0;
            if (height.Length < 2 || height.Length > 100000)
            {
                return maxArea;
            }

            List<int> sorted = new List<int>(height);
            sorted.Sort();
            for (int i = sorted.Count/2; i < sorted.Count; i++)
            {
                if (sorted[sorted.Count / 2] > 10000)
                    return maxArea;
                else
                    sorted.RemoveRange(0, sorted.Count / 2);
              
                if (sorted.Count == 1)
                    break;
            }


            for (int i = 0; i < height.Length; i++)
            {
                for (int j = 0; j < height.Length; j++)
                {
                    if (i != j)
                    {
                        int h = Math.Min(height[j], height[i]);
                        int w = Math.Abs(j - i);
                        maxArea = Math.Max(maxArea, h * w);
                    }
                }
            }
            return maxArea;
        }


        private static int MaxAreaContainer2(int[] height)
        {
            //     9
            //  8  |   8
            //  |**|   ^
            //  |6*|   |
            //  ||*|   |
            //  ||*|4  |
            //  ||*||  |
            //  ||2||  |
            // 1|||||  |
            // ||||||  |
            // 012345

            //  Initialize the max area
            //  While v[i] < v[next] result can grow
            //  Specifically, if v[i] < v[lastIndex], optimum result would be v[i], v[lastIndex], a = v[i] * lastIndex = 1*4 = 4

            int maxArea = -1;
            if (height[0] < height[height.Length - 1 - 0])
            {
                int h = height[0];
                int w = (height.Length - 1 - 0);
                maxArea = h * w;

            }
            else if (height[0] > height[height.Length - 1 - 0])
            {
                int h = height[height.Length - 1];
                int w = (height.Length - 1 - 0);
                maxArea = h * w;
            }
            else
            {
                maxArea = height[0] * (height.Length - 1 - 0);
            }

            int highestWallValue = -1;
            int highestWallIndex = -1;

            if (height[0] < height[height.Length - 1])
            {
                highestWallValue = height[height.Length - 1];
                highestWallIndex = height.Length - 1;
            }
            else
            {
                highestWallValue = height[0];
                highestWallIndex = 0;
            }

            for (int i = 1; i < height.Length; i++)
            {
                int previousHighestWallValue = highestWallValue;
                int previousHighestWallIndex = highestWallIndex;
                //  So I go to v[i+1]:8 (> 4 so can potentially improve the solution) so compare to the last one: v[i+1]= 8 > v[lastIndex] = 4 so a= v[lastIndex] * i+1 = 4* 4 = 16
                if (height[i] >= highestWallValue)
                {

                    int calculatedArea = highestWallValue * Math.Abs(highestWallIndex - i);
                    highestWallValue = height[i];
                    highestWallIndex = i;


                    if (calculatedArea > maxArea) //only change highest wall value if the area improves. Because if you don't, the width will shrink
                    {
                        highestWallValue = height[i];
                        highestWallIndex = i;
                        maxArea = calculatedArea;
                    }

                    else if (calculatedArea < maxArea)  //only change highest wall value if the area improves. Because if you don't, the width will shrink so revert if area doesn't improve
                    {
                        if (highestWallValue < previousHighestWallValue)//18 < 6? NO so don't change highestWallValue
                        {
                            highestWallValue = previousHighestWallValue;
                            //highestWallIndex = previousHighestWallIndex;
                        }
                        highestWallIndex = previousHighestWallIndex;
                    }
                    else
                    {
                        if (highestWallValue < previousHighestWallValue)
                        {
                            highestWallValue = previousHighestWallValue;
                            highestWallIndex = i;
                        }
                        else
                            highestWallIndex = previousHighestWallIndex;
                    }
                }
                else if (height[i] < highestWallValue)
                {
                    int calculatedArea = height[i] * Math.Abs(highestWallIndex - i); // 6* index(20) = 6 * 6 = 36
                    if (calculatedArea > maxArea)                               //but 6* index(8) = 6* 7 = 42;
                        maxArea = calculatedArea;
                }

                //  Now check v[i+1]: 6 (< 8 so cannot potentially improve the solution)
                //  So I compare to v[i+1]: 2 (< 8 so cannot potentially improve the solution)
                //  So I compare to v[i+1]: 9 (> 8 so can potentially improve the solutionà a = 8*indexof9 = 8* 4 = 24 > a
                //  So I compare tp v[i+1]: 4 (<9 so cannot potentially improve the solution)
            }
            return maxArea;
        }



        //int[] A = { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
        private static int MaxAreaContainer3(int[] height)
        {
            int maxArea = 0;

            if (height.Length < 2 || height.Length > 100000)
            {
                return maxArea;
            }

            List<int> sorted = new List<int>(height);
            sorted.Sort();
            for (int i = sorted.Count / 2; i < sorted.Count; i++)
            {
                if (sorted[sorted.Count / 2] > 10000)
                    return maxArea;
                else
                    sorted.RemoveRange(0, sorted.Count / 2);

                if (sorted.Count == 1)
                    break;
            }

            int firstIndex = 0;
            int lastIndex = height.Length-1;
            
            while(firstIndex < lastIndex)
            {
                int h = Math.Min(height[lastIndex], height[firstIndex]);
                int w = Math.Abs(lastIndex - firstIndex);
                maxArea = Math.Max(maxArea, h * w);

                if(height[firstIndex] <= height[lastIndex])
                {
                    firstIndex++;
                }
                else
                {
                    lastIndex--;
                }
            }
            //do
            //{
            //    firstIndex++;
            //    lastIndex--;

            //    if (height[firstIndex] <= height[lastIndex])
            //        maxArea = height[firstIndex] * Math.Abs(lastIndex - firstIndex);
            //    else
            //        maxArea = height[lastIndex] * Math.Abs(lastIndex - firstIndex);

            //    if (firstIndex == height.Length - 1 || lastIndex == 0)
            //        break;

            //} while (height[firstIndex + 1] >= height[firstIndex] || height[lastIndex - 1] >= height[lastIndex]);

            return maxArea;
        }

    }
}


