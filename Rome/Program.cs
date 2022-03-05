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

            //Console.WriteLine("Rome city is: " + Solution(A, B));
            Console.WriteLine("Rome city is: " + Solution2(A, B));
        }

        public static int Solution2(int[] A, int[] B)
        {
            int romeCity = -1;

            //Dictionary<int, int> connections = new Dictionary<int, int>();
            //for (int i = 0; i < A.Length; i++)
            //{
            //    connections.Add(A[i], B[i]);
            //    //connections.Add(B[i], A[i]);
            //}

            //foreach (KeyValuePair<int,int> conn in connections)
            //{
            //    Console.WriteLine("City: {0} connected TO City {1}", conn.Key, conn.Value);
            //}


            //  Because each key is a city that has a value that is also a city
            //  The link is 2 way. We probably need a linked list
            LinkedList<int>[] LinkedCities = new LinkedList<int>[A.Length];

            //  Create association
            for (int i = 0; i < A.Length; i++)
            {
                LinkedCities[i] = new LinkedList<int>();
                LinkedCities[i].AddLast(A[i]);
                LinkedCities[i].AddLast(B[i]);   
            }

            //  Find other association
            int[] reference = new int[A.Length + B.Length];
            A.CopyTo(reference, 0);
            B.CopyTo(reference, A.Length);

            for (int refLinkedCityIndex = 0; refLinkedCityIndex < LinkedCities.Length; refLinkedCityIndex++)
            {
                //int cityToSearch = reference[i];
                for (int parkourIndex = 0; parkourIndex < LinkedCities.Length; parkourIndex++)
                {
                    if (refLinkedCityIndex != parkourIndex)
                    {
                        for (int k = 0; k < LinkedCities[parkourIndex].Count; k++)
                        {
                                                                                // the two of the current parkour
                            //if (LinkedCities[refLinkedCityIndex].Contains(LinkedCities[parkourIndex].Find(41)))
                            //{
                                //lc[j].;
                            //}
                        }
                        
                    }
                }
            }





            Console.WriteLine("City connected: ");
            foreach (LinkedList<int> linkedCity in LinkedCities)
            {
                foreach (var city in linkedCity)
                {
                    Console.Write(city);
                    Console.Write(", ");
                }
                Console.WriteLine("");
            }


            return romeCity;
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
                    if (shouldStopTestingTarget)
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
