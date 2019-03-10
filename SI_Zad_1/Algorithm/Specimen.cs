using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace SI_Zad_1.Algorithm
{
    public class Specimen
    {
        public Data Data { get; set; }
        public int[] CitiesIndexSequence { get; set; }

        public Item[] ItemsSequence { get; set; }

        public double Value { get; set; }

        public Specimen(Data data, int[] citiesIndexSequence)
        {
            Data = data;
            CitiesIndexSequence = citiesIndexSequence;
            ItemsSequence = MakeItemsSequence();
        }

        public void Mutate()
        {
            var possibleValues = Enumerable.Range(0, CitiesIndexSequence.Length).ToList();
            var firstIndexToSwap = GetRandomValue(possibleValues);
            var secondIndexToSwap = GetRandomValue(possibleValues);

            var temp = CitiesIndexSequence[firstIndexToSwap];
            CitiesIndexSequence[firstIndexToSwap] = CitiesIndexSequence[secondIndexToSwap];
            CitiesIndexSequence[secondIndexToSwap] = temp;
            
            WriteLine("Specimen after mutation:");
            WriteLine(ToString());
        }

        private int GetRandomValue(IList<int> possibleValues)
        {
            var rand = new Random();
            var randomIndex = rand.Next(possibleValues.Count);
            var randomValue = possibleValues[randomIndex];
            possibleValues.RemoveAt(randomIndex);
            return randomValue;
        }
        
        private Item[] MakeItemsSequence()
        {
            var itemsSequence = new Item[CitiesIndexSequence.Length];
            var knapsackItemsWeight = 0;
            for (var i = 0; i < CitiesIndexSequence.Length; i++)
            {
                var cityIndex = CitiesIndexSequence[i];
                var itemsInCity = Data.Items.Where(item => item.CityNumber == cityIndex).ToList();
                if (itemsInCity.Count > 0)
                {
                    var pickedItem = FindBiggestValueItem(itemsInCity);
                    if (knapsackItemsWeight + pickedItem.Weight <= Data.CapacityOfKnapsack)
                    {
                        itemsSequence[i] = pickedItem;
                        knapsackItemsWeight += pickedItem.Weight;
                    }   
                }
            }

            return itemsSequence;
        }

        private Item FindBiggestValueItem(IEnumerable<Item> items)
        {
            return items.OrderByDescending(item => item.Profit).First();
        }

        private Item FindSmallestWeightItem(IEnumerable<Item> items)
        {
            return items.OrderBy(item => item.Weight).First();
        }

        public static Specimen GenerateRandom(Data data)
        {
            var sequenceLength = data.Cities.Length;
            var citiesIndexSequence = new int[sequenceLength];
            var possibleValues = data.Cities.ToList();
            for (var i = 0; i < sequenceLength; i++)
            {
                var rand = new Random();
                var randomIndex = rand.Next(0, possibleValues.Count);
                citiesIndexSequence[i] = possibleValues[randomIndex].Index;
                possibleValues.RemoveAt(randomIndex);
            }

            return new Specimen(data, citiesIndexSequence);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("Cities sequence:");
            builder.Append("\n|");
            foreach (var cityIndex in CitiesIndexSequence)
            {
                builder.Append(cityIndex);
                builder.Append("|");
            }
            
            builder.Append("\nItems sequence:");
            builder.Append("\n|");
            foreach (var item in ItemsSequence)
            {
                if (item != null)
                {
                    builder.Append(item.Index);
                }
                else
                {
                    builder.Append("null");
                }

                builder.Append("|");
            }

            builder.Append("\nValue = ");
            builder.Append(Value);
            
            return builder.ToString();
        }
    }
}