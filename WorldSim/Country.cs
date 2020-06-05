using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace WorldSim
{
    class Country
    {
        public List<Town> Towns;
        private Random Rand;

        public Country()
        {
            Towns = new List<Town>();
            Rand = new Random();
        }

        public void GenerateTowns()
        {
            const int initialCities = 20;
            const int neighbours = 30;
            const uint minTownPop = 10_000;
            const uint maxTownPop = 500_000;
            const uint minCityPop = 500_000;
            const uint maxCityPop = 10_000_000;

            const int radius = 1000;
            const int minNeighbourRadius = 30;
            const int neighbourRadius = 250;
            const int minDistance = 200;
            const int minTownDistance = 15;
            const int maxIterations = 100;

            for (int i = 0; i < initialCities; i++)
            {
                Vector2 pos = new Vector2(0);
                float distance = 0;
                int iteration = 0;

                while (distance < minDistance || iteration < maxIterations)
                {
                    pos = GetRandomPosition(radius);
                    distance = GetDistanceToNearestTown(pos);
                    iteration++;
                }

                Town town = new Town
                {
                    Population = (uint)Rand.Next((int) minCityPop, (int) maxCityPop),
                    Type = Town.TownType.City,
                    Position = pos
                };

                Towns.Add(town);
                Console.WriteLine(town.ToString());
            }

            // Generate neighboring towns
            for (int t = 0; t < initialCities; t++)
            {
                Town city = Towns[t];

                for (int i = 0; i < neighbours; i++)
                {
                    Vector2 pos = new Vector2(0);
                    float distance = 0;
                    float cityDistance = 0;
                    int iteration = 0;

                    while (distance < minTownDistance || cityDistance < minNeighbourRadius || iteration < maxIterations)
                    {
                        pos = GetRandomPositionAroundPoint(city.Position, neighbourRadius);
                        distance = GetDistanceToNearestTown(pos);
                        cityDistance = GetDistanceToNearestCity(pos);
                        iteration++;
                    }

                    Town neighbour = new Town
                    {
                        Type = Town.TownType.Town,
                        Population = (uint)Rand.Next((int) minTownPop, (int) maxTownPop),
                        Position = pos
                    };

                    Towns.Add(neighbour);
                    Console.WriteLine(neighbour.ToString());
                }
            }
        }

        Vector2 GetRandomPositionAroundPoint(Vector2 root, float radius)
        {
            double angle = Rand.NextDouble() * 360;
            double distance = Rand.NextDouble() * radius;

            float x = (float)(Math.Sin(angle) * distance);
            float y = (float)(Math.Cos(angle) * distance);

            return Vector2.Add(root, new Vector2(x, y));
        }

        Vector2 GetRandomPosition(float radius)
        {
            float x = (float)Math.Round(Rand.NextDouble() * radius, 3) - radius / 2;
            float y = (float)Math.Round(Rand.NextDouble() * radius, 3) - radius / 2;

            return new Vector2(x, y);
        }

        public float GetDistanceToNearestTown(Vector2 position)
        {
            float nearest = Single.PositiveInfinity;

            foreach (Town town in Towns)
            {
                float dist = town.DistanceTo(position);

                if (dist < nearest)
                {
                    nearest = dist;
                }
            }

            return nearest;
        }

        public float GetDistanceToNearestCity(Vector2 position)
        {
            float nearest = Single.PositiveInfinity;

            foreach (Town town in Towns)
            {
                if (town.Type == Town.TownType.City)
                {
                    float dist = town.DistanceTo(position);

                    if (dist < nearest)
                    {
                        nearest = dist;
                    }
                }
            }

            return nearest;
        }
    }
}
