using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Zad_1.Algorithm
{
    public class City
    {
        public int Index { get; set; }
        public double CoordX { get; set; }
        public double CoordY { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"Index = {Index}".PadRight(15, ' '));
            builder.Append(" | ");
            builder.Append($"CoordX = {CoordX}".PadRight(15, ' '));
            builder.Append(" | ");
            builder.Append($"CoordY = {CoordY}".PadRight(15, ' '));
            return builder.ToString();
        }
    }
}
