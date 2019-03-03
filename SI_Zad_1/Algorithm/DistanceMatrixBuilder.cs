using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Zad_1.Algorithm
{
    class DistanceMatrixBuilder
    {
        public double[,] MakeMatrix(City[] cities)
        {
            var citiesCount = cities.Length;
            var matrix = new double[citiesCount, citiesCount];

            for (var i = 0; i < citiesCount; i++)
            {
                for (var j = 0; j < citiesCount; j++)
                {
                    matrix[i, j] = CalculateDistance(cities[i], cities[j]);
                }
            }

            return matrix;
        }

        public static string MatrixToString(double[,] matrix)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                builder.Append("|");
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    var value = matrix[i, j];
                    var valueString = TruncateString(value.ToString(), 6);
                    builder.Append(valueString);
                    builder.Append("|");
                }

                builder.Append("\n");
            }

            return builder.ToString();
        }

        private double CalculateDistance(City firstCity, City secondCity)
        {
            var firstPow = Math.Pow(secondCity.CoordX - firstCity.CoordX, 2);
            var secondPow = Math.Pow(secondCity.CoordY - firstCity.CoordY, 2);
            var distance = Math.Sqrt(firstPow + secondPow);
            return distance;
        }

        private static string TruncateString(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Length <= maxLength ? value.PadRight(maxLength, ' ') : value.Substring(0, maxLength);
        }
    }
}
