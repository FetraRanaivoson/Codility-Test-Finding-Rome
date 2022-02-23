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
        public List<City> connections = new List<City>();

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
            if (!connections.Contains(newConnection))
            {
                connections.Add(newConnection);
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
                Console.Write(connections[i].cityID + ", ");
            }
            Console.WriteLine("");
        }
    }

    /*
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
    */

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
            // The Rome city represented by its ID. -1= no Rome city found
            int romeCity = -1;

            // Create the city objects for A and B
            City[] citiesA = new City[A.Length];
            City[] citiesB = new City[B.Length];
            for (int i = 0; i < citiesA.Length; i++)
            {
                // Add to the list and pass their ID(= city name)
                citiesA[i] = new City(A[i]);
            }
            for (int i = 0; i < citiesB.Length; i++)
            {
                // Add to the list and pass their ID(= city name)
                citiesB[i] = new City(B[i]);
            }

            // Establish the connections
            for (int i = 0; i < A.Length; i++)
            {
                citiesA[i].AddConnection(citiesB[i]);
                citiesB[i].AddConnection(citiesA[i]);
            }

            // Join the cities arrays for easier to loop later
            City[] cities = new City[citiesA.Length + citiesB.Length];
            citiesA.CopyTo(cities, 0);
            citiesB.CopyTo(cities, citiesA.Length);
            //foreach (var item in cities)
            //{
            //    Console.WriteLine(item.cityID);
            //}

            // We will increment this variable if we found that we can go from a specific city to another city
            // If throughout that iteration, this variable value is the size of the citites - 1 (combined), 
            // Then that city is Rome.
            int path = 0;

            // What we are going to test here is the number of path for each city to the other cities 
            // (including their connections)
            for (int start = 0; start < cities.Length; start++)
            {
                // Always initialize the path count to 0 because we are about to test another city
                path = 0;

                //Register the current city id
                int currentCityID = cities[start].cityID;

                // Now loop for all the other cities except the current city
                for (int indexTarget = 0; indexTarget < cities.Length; indexTarget++)
                {
                    if(start != indexTarget)
                    {
                        foreach (City startConnection in cities[start].connections)
                        {
                            // Check direct connection
                            if (startConnection.cityID == cities[indexTarget].cityID)
                            {
                                path++;
                            }

                            // Check indirect connection
                            else
                            {
                                foreach (var otherCity in cities)
                                {
                                    if (otherCity.cityID != cities[start].cityID && otherCity.cityID != cities[indexTarget].cityID)
                                    {
                                                            // start     //intermediate city  //target
                                        if (ThereIsPathFrom(cities[start], otherCity, cities[indexTarget]))
                                        {
                                            path++;
                                        }
                                    }
                                }
                                
                            }
                        }                 
                    }
                }
                if (start == cities.Length - 1 && path == cities.Length - 1)
                {
                    romeCity = currentCityID;
                    break;
                }
            }

           


            return romeCity;
        }

        //public bool IsRome(City city, List<CityPair> allCityPairs)
        //{
        //    // Everytime checking a city, put all city pairs into this temp pair so that we can remove 
        //    List<CityPair> tempCityPairs = allCityPairs;

        //    for (int i = 0; i < city.connections.Count; i++)
        //    {
        //        // If the neighbours' number of connection is 1, that means that the only connection for that neighbour is 1: direct access
        //        if(city.connections[i].connections.Count == 1)
        //        {
        //            //tempCityPairs.Remove(city.connections[i])
        //        }

        //        // Else we need to check all the neighbour if we can indireclty access
        //        if(city.connections[i].connections.Count > 1) { }
        //        {
        //            for (int k = 0; k < city.connections[i].connections.Count; k++)
        //            {
        //                if (city.connections[i].connections[k].connections.Count == 1)
        //                {

        //                }
        //                if (city.connections[i].connections[k].connections.Count > 1) { }
        //                {

        //                }
        //            }
        //        }
        //            //City cityToTest = city.connections[i];

        //            // For each of these steps, remove what we saw from the temp cityPair
        //            // If the temp city pair count == 0, that means we can go to the current city from any cities
                    
        //    }
        //    return false;
        //}
    }
}
