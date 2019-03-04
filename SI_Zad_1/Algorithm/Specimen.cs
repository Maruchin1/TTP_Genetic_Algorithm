using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_Zad_1.Algorithm
{
    class Specimen
    {
        public Data Data { get; set; }
        public int[] CitiesSequence { get; set; }

        public Specimen(Data data)
        {
            Data = data;
            CitiesSequence = new int[Data.Cities.Length];
        }

        public double CalculateTotalTime()
        {
            var totalTime = 0d;

            for (var i = 0; i < CitiesSequence.Length-1; i++)
            {
                var currCityIndex = CitiesSequence[i];
                var nextCityIndex = CitiesSequence[i + 1];

                var time = CalculateTime(currCityIndex, nextCityIndex);
                totalTime += time;
            }

            var lastCityIndex = CitiesSequence[CitiesSequence.Length - 1];
            var firstCityIndex = CitiesSequence[0];
            var returnTime = CalculateTime(lastCityIndex, firstCityIndex);
            totalTime += returnTime;

            return totalTime;
        }

        private double CalculateTime(int firstCityIndex, int secondCityIndex)
        {
            var distance = Data.DistanceMatrix[firstCityIndex-1, secondCityIndex-1];
            //todo prędkość ma być potem wyliczana na bierząco 
            var speed = Data.MaxSpeed;
            return distance / speed;
        }

        public static Specimen GenerateRandom(Data data)
        {
            var specimen = new Specimen(data);
            var sequenceLength = specimen.CitiesSequence.Length;
            var possibleValues = Enumerable.Range(1, sequenceLength).ToList();

            for (var i = 0; i < sequenceLength; i++)
            {
                var rand = new Random();
                var randomIndex = rand.Next(0, possibleValues.Count);
                specimen.CitiesSequence[i] = possibleValues[randomIndex];
                possibleValues.RemoveAt(randomIndex);
            }

            return specimen;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("|");
            foreach (var cityIndex in CitiesSequence)
            {
                builder.Append("|");
                builder.Append(cityIndex);
            }
            builder.Append("|");

            return builder.ToString();
        }
    }
}
