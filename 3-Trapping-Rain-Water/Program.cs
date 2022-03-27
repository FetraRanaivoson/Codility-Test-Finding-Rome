using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace _3_Trapping_Rain_Water
{
    class Program
    {
        static void Main(string[] args)
        {
            //Given an array of integers representing an
            //elevation map where the width of each bar is 1,
            //return how much rainwater can be trapped
            //int[] height = { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            //int[] height = {4,2,0,3,2,5 };
            int[] height = { 0, 7, 1, 4, 6, 4, 4 };

            var sw = Stopwatch.StartNew();
            Console.WriteLine("Total unit of rainwater (O(n^2)) is: {0}", RainWater(height));
            Console.WriteLine("Elapsed ticks: {0}", sw.ElapsedTicks); //300k-600k

            var sw2 = Stopwatch.StartNew();
            Console.WriteLine("Total unit of rainwater (O(n)) is: {0}", RainWater2(height));
            Console.WriteLine("Elapsed ticks: {0}", sw2.ElapsedTicks);
        }

        private static int RainWater(int[] height)
        {
            //            h = min(a, b)
            //* *************************************************************************
            //h += Difference between a maximum value and a current value
            //if (currentValue > maximumValue) then maximumValue = currentValue

            //          ||
            // ||* * * *||
            // ||* *|| *||
            // ||||*||||||
            // ||||*||||||
            // 4 2 0 3 2 5

            //0- cV >= mV => 4> 0 => mV = 4
            //h += mV - cV = 0 + 4 - 4 = 0
            //
            //1- cV >= mV, 2! >= 4
            ///*check ahead if there is item >=mV*/  true : 5
            //if true=> then h = 0 + 4 - 2 = 2
            //
            //2- cV >= mV, 0! > 4
            ///*check ahead if there is item >=mV*/  true : 5(repetitive)
            //if true=> then h = 2 + 4 - 0 = 6
            //
            //3- cV > mV, 3! > 4
            ///*check ahead if there is item >=mV*/  true : 5(repetitive)
            //if true=> then h = 6 + 4 - 3 = 7
            //
            //4- cV > mV, 2! > 4
            ///*check ahead if there is item >=mV*/  true : 5(repetitive)
            //if true=> then h = 7 + 4 - 2 = 9
            //
            //5- cV > mV, 5! > 5
            ///*check ahead if there is item >=mV*/  false
            ///*if false, can I be in the middle of before me and after me? */   b = 2 < me = 2 < a = nothing
            //End h=9

            int h = 0;
            var lmV = 0;
            var rmV = 0;

            //int l = 0;
            //int r = height.Length - 1;
            //var lmV = height[0];
            //var rmV = height[height.Length-1];
            //while (l < r)
            //{
            //    if (lmV <= height[l++])
            //    {
            //          var min = lmV;
            //          lmV = height[l++];
            //          h += lmV - min;
            //    }
            //    if (rmV < height[r-1])
            //    {
            //         var min = rmV;
            //         rmV = height[r-1];
            //         h+= Math.Abs(min - Math.Min(lmV, rmV));
            //    }
            //    l++;
            //    r--;
            //}


            //if (height.Length < 1 || height.Length > 20000)
            //{
            //    return h;
            //}

            //List<int> sorted = new List<int>(height);
            //sorted.Sort();
            //for (int i = sorted.Count / 2; i < sorted.Count; i++)
            //{
            //    if (sorted[sorted.Count / 2] > 100000)
            //        return h;
            //    else
            //        sorted.RemoveRange(0, sorted.Count / 2);
            //    if (sorted.Count == 1)
            //        break;
            //}

            for (int i = 0; i < height.Length; i++)
            {
                var cV = height[i];
                rmV = 0;
                if (cV >= lmV)
                {
                    lmV = cV;
                    // h += (lmV - cV); h += 0
                }
                else if (cV < lmV)
                {
                    if (i == height.Length - 1)
                        continue;

                    for (int j = i + 1; j < height.Length; j++)
                    {
                        if (/*height[j] >= cV && */height[j] > rmV)
                        {
                            rmV = height[j];
                        }
                    }

                    if (cV < rmV)
                    {
                        h += Math.Abs(cV - Math.Min(lmV, rmV));
                    }
                }
            }
            return h;

            #region txt
            //int [] height = {0,7,1,4,6}
            //   ||    
            //   ||*  *||
            //   ||*  *|| 
            //   ||* ||||||||   
            //   ||* ||||||||  
            //   ||* ||||||||
            //   ||||||||||||
            //  0 7 1 4 6 4 4
            //int[] height = { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            //0 - cV >= mV => mV = cV
            //h += mV - cV = 0
            //1 - cV >= mv, 1 >= 0 => mV = cV = 1
            //h += 1 - 1 = 0
            //2 - cV >= mV, 0! > 1
            //h += 1 - 0 = 1
            //3 - cV > mV, 2 > 1 mV = 2
            //h = 1 + 2 - 2 = 1
            //4 - cV > mV, 1! > 2
            //h = 1 + 2 - 1 = 2
            //5 - cV > mV, 0! > 2
            //h = 2 + 2 - 0 = 4
            //6 - cV > mV, 1! > 2
            //h = 4 + 2 - 1 = 5
            //7 - cV > mV, 3 > 2, mV = 3
            //h = 5 + 5 - 5 = 5
            //            8 - cV > mV, 2! > 3
            ////h = 5 + 3-2 = 6 /*not good*/
            //            /*check ahead if there is item >=mV*/
            //                                      false
            //if true=> then h = 5 + 3 - 2 = 6(BUT NOW h is STILL 5)
            ///*if false, can I be in the middle of before me and after me? */b = 3 < me = 2 < a = 1 =>false
            //If true => then h += min(b - me, a - me) // ie min(2-1,2-1) = 1 
            //9 - cV > mV, 1! > 3
            ///*check ahead if there is item >= mV*/ false
            //If true=>then h = 5 + min(beforeCurrent, AfterCurrent) = 5 + 1 = 6
            ///*if false, can I be in the middle of before me and after me? */   b = 2 < me = 1 < a = 2 =>true
            //If true => then h += min(b - me, a - me) // ie min(2-1,2-1) = 1 => h=6
            //10 - cV > mV 2! > 3
            ///*check ahead if there is item >= mV*/ false
            //if true=> then h = 6 + 3 - 2 = 7(BUT NOW h is STILL 6)
            ///*if false, can I be in the middle of before me and after me? */   b = 1 < me = 2 < a = 1 =>false
            //If true => then h += min(b - me, a - me)
            //11 - cV > mV 1! > 3
            ///*check ahead if there is item >= mV*/ false
            //if true=> then h = 6 + 3 - 1 = 8(BUT NOW h is STILL 6)
            ///*if false, can I be in the middle of before me and after me? */   b = 2 < me = 2 < a = nothing false
            #endregion
        }

        private static int RainWater2(int[] height)
        {
            int h = 0;

            int l = 0;
            int r = height.Length - 1;

            var lMax = height[0];
            //var rMax = height[height.Length - 1];
            var rMax = height[2];

            //The reason why I always reset the rMax is
            //because if I pass this value, I wouldn't have the ahead next rMax
            List<int> sorted = new List<int>(height);
            sorted.Sort();
            int max = sorted[sorted.Count - 1];
            //   ||    
            //   ||*  *||
            //   ||*  *|| 
            //   ||* ||||||||   
            //   ||* ||||||||  
            //   ||* ||||||||
            //   ||||||||||||
            //  0 7 1 4 6 4 4
            int lMaxIndex = 0;
            for (int i = 0; i < height.Length-1; i++)
            {
                if (height[i] >= lMax)
                {
                    lMax = height[i];
                    lMaxIndex = i;
                }

                if(height[i+1]< height[i])
                {
                    //Suppose that rMax = lMax
                    rMax = lMax;
                }
                else
                {
                    rMax = height[i + 1];
                }
                
                h += (i - lMaxIndex) * Math.Abs(height[i] - Math.Min(lMax, rMax));
                
            }
            return h;
        }
    }
}
