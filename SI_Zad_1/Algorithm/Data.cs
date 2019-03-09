using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Zad_1.Algorithm
{
    public class Data
    {
        public int CapacityOfKnapsack { get; set; }
        public double MinSpeed { get; set; }
        public double MaxSpeed { get; set; }
        public City[] Cities { get; set; }
        public Item[] Items { get; set; }
        public double[,] DistanceMatrix { get; set; }

        public Data(int capacityOfKnapsack, double minSpeed, double maxSpeed, City[] cities, Item[] items)
        {
            CapacityOfKnapsack = capacityOfKnapsack;
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
            Cities = cities;
            Items = items;
            var builder = new DistanceMatrixBuilder();
            DistanceMatrix = builder.MakeMatrix(Cities);

        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Cities:");
            foreach (var city in Cities)
            {
                builder.AppendLine($"- {city}");
            }
            builder.AppendLine("Items:");
            foreach (var item in Items)
            {
                builder.AppendLine($"- {item}");
            }
            builder.Append("Distance matrix:\n");
            builder.Append(DistanceMatrixBuilder.MatrixToString(DistanceMatrix));

            return builder.ToString();
        }
    }
}
