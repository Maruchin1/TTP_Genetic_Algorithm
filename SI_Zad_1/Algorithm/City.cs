using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Zad_1.Algorithm
{
    class City
    {
        public int Index { get; set; }
        public double CoordX { get; set; }
        public double CoordY { get; set; }

        public override string ToString()
        {
            return $"Index = {Index} | CoordX = {CoordX} | CoordY = {CoordY}";
        }
    }
}
