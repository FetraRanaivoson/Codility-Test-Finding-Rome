using System;
using System.Collections.Generic;
using System.IO;

namespace RomeFinding
{
    /// <summary>
    /// The city class
    /// </summary>
    class City
    {
        /// <summary>
        /// This city's number
        /// </summary>
        public int cityID;

        /// <summary>
        /// This city's list of connections
        /// </summary>
        public List<int> connections = new List<int>();

        /// <summary>
        /// Constructor of a city
        /// </summary>
        public City(int cityID)
        {
            this.cityID = cityID;
        }

        /// <summary>
        /// Method for adding connection to this city
        /// </summary>
        public void AddConnection(City newConnection)
        {
            if (!connections.Contains(newConnection.cityID))
            {
                connections.Add(newConnection.cityID);
            }
        }

        /// <summary>
        /// The number of connections this city have
        /// </summary>
        public int ConnectionCount()
        {
            return connections.Count;
        }

        /// <summary>
        /// Method to display all cities that are connected to this city
        /// </summary>
        public void DisplayConnectedCity()
        {
            Console.WriteLine("Connected cities: ");
            for (int i = 0; i < connections.Count; i++)
            {
                Console.Write(connections[i] + ", ");
            }
            Console.WriteLine("");
        }
    }

    /// <summary>
    /// The city pair class
    /// </summary>
    class CityPair
    {
        /// <summary>
        /// The first city in this pair
        /// </summary>
        public City cityA;

        /// <summary>
        /// The second city in this pair
        /// </summary>
        public City cityB;

        /// <summary>
        /// The list of cities in this pair
        /// </summary>
        public List<City> cities = new List<City>();

        /// <summary>
        /// City pair's constructor
        /// </summary>
        public CityPair(City cityA, City cityB)
        {
            this.cityA = cityA;
            this.cityB = cityB;

            cities.Add(cityA);
            cities.Add(cityB);

            cityA.AddConnection(cityB);
            cityB.AddConnection(cityA);
        }

        /// <summary>
        /// Method to display a city pair
        /// </summary>
        public void Display()
        {
            Console.WriteLine("(" + cityA.cityID + "," + cityB.cityID + ")");
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            // Test 1
            //int[] A = new int[3] { 1, 2, 3 };
            //int[] B = new int[3] { 0, 0, 0 };

            // Test 2
            int[] A = new int[5] { 0, 1, 2, 4, 5 };
            int[] B = new int[5] { 2, 3, 3, 3, 2 };


            Console.WriteLine("Rome city is: " + Solution(A, B));
        }

        /// <summary>
        /// Given an array A and B containing city numbers peer to peer connected and knowing that all paths leads to Rome, find Rome city number
        /// </summary>
        public static int Solution(int[] A, int[] B)
        {
            int romeCity = -1;

            List<CityPair> allCityPairs = new List<CityPair>();
            for (int j = 0; j < A.Length; j++)
            {
                CityPair newCityPair = new CityPair(new City(A[j]), new City(B[j]));
                newCityPair.Display();
                allCityPairs.Add(newCityPair);


                if (allCityPairs.Count == A.Length)
                {
                    for (int col = 0; col < A.Length; col++)
                    {
                        for (int index = 0; index < allCityPairs.Count; index++)
                        {
                            if (col != index)
                            {
                                if (allCityPairs[col].cityA.cityID == allCityPairs[index].cityA.cityID)
                                    allCityPairs[col].cityA.AddConnection(allCityPairs[index].cityA); //not city B
                                if (allCityPairs[col].cityA.cityID == allCityPairs[index].cityB.cityID)
                                    allCityPairs[col].cityA.AddConnection(allCityPairs[index].cityA); //not city A

                                if (allCityPairs[col].cityB.cityID == allCityPairs[index].cityA.cityID)
                                    allCityPairs[col].cityB.AddConnection(allCityPairs[index].cityB); //not city A
                                if (allCityPairs[col].cityB.cityID == allCityPairs[index].cityB.cityID)
                                    allCityPairs[col].cityB.AddConnection(allCityPairs[index].cityA); //not city B
                            }
                        }
                    }
                }
            }
            Console.WriteLine("=============================================");

            int maxPath = A.Length;


            if (allCityPairs.Count == A.Length)
            {
                for (int k = 0; k < A.Length; k++)
                {
                    for (int l = 0; l < 2; l++)
                    {
                        Console.WriteLine(allCityPairs[k].cities[l].cityID + "'s connection count = " + allCityPairs[k].cities[l].ConnectionCount());
                        allCityPairs[k].cities[l].DisplayConnectedCity();
                        Console.WriteLine("=============================================");

                        if(allCityPairs[k].cities[l].ConnectionCount() == maxPath)
                        {
                            romeCity = allCityPairs[k].cities[l].cityID;
                        }
                    }
                }
            }
            return romeCity;
        }
    }
}
