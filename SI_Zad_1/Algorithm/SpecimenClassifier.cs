using System.Collections.Generic;
using System.Linq;

namespace SI_Zad_1.Algorithm
{
    public class SpecimenClassifier
    {
        private Data Data;

        public SpecimenClassifier(Data data)
        {
            Data = data;
        }
        
        public double CalculateSpecimenValue(Specimen specimen)
        {
            var totalProfit = CalculateTotalItemsProfit(specimen.ItemsSequence);
            var totalTime = CalculateTotalTime(specimen.CitiesSequence, specimen.ItemsSequence);

            return totalProfit - totalTime;
        }
        
        private long CalculateTotalItemsProfit(IEnumerable<Item> itemsSequence)
        {
            return itemsSequence.Where(item => item != null).Sum(item => item.Profit);
        }
        
        private double CalculateTotalTime(IReadOnlyList<City> citiesSequence, IReadOnlyList<Item> itemsSequence)
        {
            var totalTime = 0d;

            for (var i = 0; i < citiesSequence.Count - 1; i++)
            {
                var currCityIndex = citiesSequence[i].Index;
                var nextCityIndex = citiesSequence[i + 1].Index;

                var time = CalculateTime(currCityIndex, nextCityIndex, itemsSequence);
                totalTime += time;
            }

            var lastCityIndex = citiesSequence[citiesSequence.Count - 1].Index;
            var firstCityIndex = citiesSequence[0].Index;
            var returnTime = CalculateTime(lastCityIndex, firstCityIndex, itemsSequence);
            totalTime += returnTime;

            return totalTime;
        }
        
        private double CalculateTime(int firstCityIndex, int secondCityIndex, IReadOnlyList<Item> itemsSequence)
        {
            var distance = Data.DistanceMatrix[firstCityIndex - 1, secondCityIndex - 1];
            var speed = CalculateSpeed(firstCityIndex, itemsSequence);
            return distance / speed;
        }
        
        private double CalculateSpeed(int cityIndex, IReadOnlyList<Item> itemSequence)
        {
            var currItemsWeight = 0;
            for (var i = 0; i < cityIndex; i++)
            {
                var item = itemSequence[i];
                if (item != null)
                {
                    currItemsWeight += item.Weight;
                }
            }

            return Data.MaxSpeed - currItemsWeight * ((Data.MaxSpeed - Data.MinSpeed) / Data.CapacityOfKnapsack);
        }
    }
}