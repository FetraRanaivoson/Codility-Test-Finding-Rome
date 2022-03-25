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

            //2100 ticks
            var sw2 = Stopwatch.StartNew();
            Console.WriteLine("Greatest area of water (O(n)): {0}", MaxAreaContainer2(A));
            Console.WriteLine(sw2.ElapsedTicks);

        }

        private static int MaxAreaContainer(int[] a) // O(n^2)
        {
            List<int> areaList = new List<int>();//=> Duplicate values

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    if (i != j)
                    {
                        if (a[j] == a[i])
                        {
                            int w = Math.Abs(j - i);
                            int h = a[i]; // or a[j]
                            if (!areaList.Contains(w * h))
                                areaList.Add(w * h);
                        }
                        else if (a[j] > a[i])
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


        private static int MaxAreaContainer2(int[] v)
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
            //   => So I don't need to check the in between: I know that I need to compare to a
            int maxArea = v[0] * (v.Length - 1 - 0);
            int highestWallValue = -1;
            int highestWallIndex = -1;

            if (v[0] < v[v.Length - 1])
            {
                highestWallValue = v[v.Length - 1];
                highestWallIndex = v.Length - 1;
            }
            else
            {
                highestWallValue = v[0];
                highestWallIndex = 0;
            }

            for (int i = 1; i < v.Length; i++)
            {
                //  So I go to v[i+1]:8 (> 4 so can potentially improve the solution) so compare to the last one: v[i+1]= 8 > v[lastIndex] = 4 so a= v[lastIndex] * i+1 = 4* 4 = 16
                if (v[i] >= highestWallValue)
                {
                    maxArea = highestWallValue * Math.Abs(highestWallIndex - i);
                    highestWallValue = v[i];
                    highestWallIndex = i;
                }

                //  Now check v[i+1]: 6 (< 8 so cannot potentially improve the solution)
                //  So I compare to v[i+1]: 2 (< 8 so cannot potentially improve the solution)
                //  So I compare to v[i+1]: 9 (> 8 so can potentially improve the solutionà a = 8*indexof9 = 8* 4 = 24 > a
                //  So I compare tp v[i+1]: 4 (<9 so cannot potentially improve the solution)
            }
            return maxArea;
        }


    }
}


