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
            var totalTime = CalculateTotalTime(specimen);

            return totalProfit - totalTime;
        }
        
        private long CalculateTotalItemsProfit(IEnumerable<Item> itemsSequence)
        {
            return itemsSequence.Where(item => item != null).Sum(item => item.Profit);
        }
        
        private double CalculateTotalTime(Specimen specimen)
        {
            var citiesIndexSequence = specimen.CitiesIndexSequence;
            var knapsackWeightSequence = specimen.KnapsackWeightSequence;
            var totalTime = 0d;

            for (var i = 0; i < citiesIndexSequence.Length - 1; i++)
            {
                var currCityIndex = citiesIndexSequence[i];
                var nextCityIndex = citiesIndexSequence[i + 1];
                var currKnapsackWeight = knapsackWeightSequence[i];

                var time = CalculateTime(currCityIndex, nextCityIndex, currKnapsackWeight);
                totalTime += time;
            }

            var lastCityIndex = citiesIndexSequence[citiesIndexSequence.Length - 1];
            var firstCityIndex = citiesIndexSequence[0];
            var finalKnapsackWeight = knapsackWeightSequence[citiesIndexSequence.Length - 1];
            var returnTime = CalculateTime(lastCityIndex, firstCityIndex, finalKnapsackWeight);
            totalTime += returnTime;

            return totalTime;
        }
        
        private double CalculateTime(int firstCityIndex, int secondCityIndex, int currKnapsackWeight)
        {
            var distance = Data.DistanceMatrix[firstCityIndex - 1, secondCityIndex - 1];
            var speed = CalculateSpeed(firstCityIndex, currKnapsackWeight);
            return distance / speed;
        }
        
        private double CalculateSpeed(int cityIndex, int currKnapsackWeight)
        {
            return Data.MaxSpeed - currKnapsackWeight * ((Data.MaxSpeed - Data.MinSpeed) / Data.CapacityOfKnapsack);
        }
    }
}