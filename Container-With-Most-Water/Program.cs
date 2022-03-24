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


            int[] A = { 1, 8, 6, 2, 9, 4 };

            //200000 ticks
            var sw = Stopwatch.StartNew();
            Console.WriteLine("Greatest area of water (Bubble sort): {0}", MaxAreaContainer(A));
            Console.WriteLine(sw.ElapsedTicks);

        }

        private static int MaxAreaContainer(int[] a) // O(N^2)
        {
            List<int> areaList = new List<int>();//=> Duplicate values

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    if (i != j)
                    {
                        if (a[j] > a[i])
                        {
                            int w = Math.Abs(j - i);
                            int h = a[i];
                            if (!areaList.Contains(w * h))
                                areaList.Add(w * h);
                        }
                        else if (a[j] < a[i])
                        {
                            int w = Math.Abs(j - i);
                            int h = a[j];
                            if (!areaList.Contains(w * h))
                                areaList.Add(w * h);
                        }
                    }
                }
            }
            areaList.Sort();
            return areaList[areaList.Count - 1];
        }
    }
}
