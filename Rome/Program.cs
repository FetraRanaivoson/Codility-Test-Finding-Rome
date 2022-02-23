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
            for (int startIndex = 0; startIndex < cities.Length; startIndex++)
            {
                // Always initialize the path count to 0 because we are about to test another city
                path = 0;

                //Register the current city id
                int currentCityID = cities[startIndex].cityID;

                // Now loop for all the other cities except the current city
                for (int indexTarget = 0; indexTarget < cities.Length; indexTarget++)
                {
                    bool shouldStopTestingTarget = false;

                    if (startIndex != indexTarget)
                    {
                        //foreach (City startConnection in cities[start].connections)
                        //{
                        // Check direct connection
                        if (cities[startIndex].connections[0].cityID == cities[indexTarget].cityID)
                        {
                            path++;
                        }

                        // Check indirect connection
                        
                        else
                        {
                            City cityIntermediate = cities[startIndex].connections[0];
                            foreach (var otherCity in cities)
                            {
                                //      Do not test the city intermediate itself         nor         the start city (where we started)          nor    the target city itself
                                if (otherCity.cityID != cityIntermediate.cityID && otherCity.cityID != cities[startIndex].cityID && otherCity.cityID != cities[indexTarget].cityID)
                                {
                                    // if the other city has a connection with the intermediate city (the city linked to the start city)
                                    if (otherCity.connections[0].cityID == cityIntermediate.connections[0].cityID)
                                    {
                                                            // trio: other --------> Target <---------- start
                                        if (ThereIsPathFrom(otherCity, cities[indexTarget]))
                                        {
                                            path++;
                                        }
                                        else
                                        {
                                            // There's no need to check the other paths because there's no path passing to this city (otherCity) via the intermediate city
                                            shouldStopTestingTarget = true;
                                            break;
                                        }
                                    }
                                }

                            }
                        }
                        //}                 
                    }
                    if(shouldStopTestingTarget)
                    {
                        break;
                    }
                }
                if (startIndex == cities.Length - 1 && path == cities.Length - 1)
                {
                    romeCity = currentCityID;
                    break;
                }
            }

            return romeCity;
        }

        private static bool ThereIsPathFrom(City intermediate, City target)
        {
            return intermediate.connections[0].cityID == target.connections[0].cityID;
        }

    }
}
