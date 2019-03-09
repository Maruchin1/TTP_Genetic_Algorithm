using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Zad_1.Algorithm
{
    public class Item
    {
        public int Index { get; set; }
        public int Profit { get; set; }
        public int Weight { get; set; }
        public int CityNumber { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"Index = {Index}".PadRight(15, ' '));
            builder.Append(" | ");
            builder.Append($"Profit = {Profit}".PadRight(15, ' '));
            builder.Append(" | ");
            builder.Append($"Weight = {Weight}".PadRight(15, ' '));
            builder.Append(" | ");
            builder.Append($"CityNumber = {CityNumber}".PadRight(15, ' '));
            return builder.ToString();
        }
    }
}
