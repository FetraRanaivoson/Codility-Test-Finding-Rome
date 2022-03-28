using System;

namespace _3_Trapping_Rain_Water_II
{
    class Program
    {
        static void Main(string[] args)
        {
            //Given an m x n integer matrix heightMap representing the height of each unit cell in a 
            //2D elevation map, return the volume of water it can trap after raining.

            //int[,] heightMap = new int[3, 6] { { 1, 4, 3, 1, 3, 2 }, { 3, 2, 1, 3, 2, 4 }, { 2, 3, 3, 2, 3, 1 } };
            int[][] heightMap = new int[3][];
            heightMap[0] = new int [6];
            heightMap[1] = new int [6];
            heightMap[2] = new int [6];


            heightMap[0][0] = 1;
            heightMap[0][1] = 4;
            heightMap[0][2] = 3;
            heightMap[0][3] = 1;
            heightMap[0][4] = 3;
            heightMap[0][5] = 2;

            heightMap[1][0] = 3;
            heightMap[1][1] = 2;
            heightMap[1][2] = 1;
            heightMap[1][3] = 3;
            heightMap[1][4] = 2;
            heightMap[1][5] = 4;

            heightMap[2][0] = 2;
            heightMap[2][1] = 3;
            heightMap[2][2] = 3;
            heightMap[2][3] = 2;
            heightMap[2][4] = 3;
            heightMap[2][5] = 1;


            Console.WriteLine("Hello World!");
        }
    }
}
