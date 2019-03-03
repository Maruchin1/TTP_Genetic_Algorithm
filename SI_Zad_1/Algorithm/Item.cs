using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Zad_1.Algorithm
{
    class Item
    {
        public int Index { get; set; }
        public int Profit { get; set; }
        public int Weight { get; set; }
        public int CityNumber { get; set; }

        public override string ToString()
        {
            return $"Index = {Index} | Profit = {Profit} | Weight = {Weight} | CityNumber = {CityNumber}";
        }
    }
}
