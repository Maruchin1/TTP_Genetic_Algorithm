using System;
using System.Collections.Generic;
using System.Text;
using SI_Zad_1.Algorithm;

namespace SI_Zad_1.DataLoading
{
    class LoadedData
    {
        public string ProblemName { get; set; }
        public string KnapsackDataType { get; set; }
        public int Dimension { get; set; }
        public int NumberOfItems { get; set; }
        public int CapacityOfKnapsack { get; set; }
        public double MinSpeed { get; set; }
        public double MaxSpeed { get; set; }
        public double RentingRatio { get; set; }
        public string EdgeWeightType { get; set; }
        public List<City> Cities { get; set; }
        public List<Item> Items { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"ProblemName = {ProblemName}");
            builder.AppendLine($"KnapsackDataType = {KnapsackDataType}");
            builder.AppendLine($"Dimension = {Dimension}");
            builder.AppendLine($"NumberOfItems = {NumberOfItems}");
            builder.AppendLine($"CapacityOfKnapsack = {CapacityOfKnapsack}");
            builder.AppendLine($"MinSpeed = {MinSpeed}");
            builder.AppendLine($"MaxSpeed = {MaxSpeed}");
            builder.AppendLine($"RentingRatio = {RentingRatio}");
            builder.AppendLine($"EdgeWeightType = {EdgeWeightType}");
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

            return builder.ToString();
        }
    }
}
