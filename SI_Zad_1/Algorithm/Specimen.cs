using System;
using System.Collections;
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

        public int[] KnapsackWeightSequence { get; set; }

        public double Value { get; set; }

        public Specimen(Data data, int[] citiesIndexSequence)
        {
            Data = data;
            CitiesIndexSequence = citiesIndexSequence;
            ItemsSequence = new Item[CitiesIndexSequence.Length];
            CheckIsSpecimenCorrect();
            KnapsackWeightSequence = new int[CitiesIndexSequence.Length];
            MakeItemsSequence();
        }

        private void CheckIsSpecimenCorrect()
        {
            foreach (var index in CitiesIndexSequence)
            {
                var itemCount = CitiesIndexSequence.Count(otherIndex => otherIndex == index);
                if (itemCount > 1)
                {
                    throw new Exception("CitiesIndexSequence contains duplicates");
                }
            }
        }
        
        public void Mutate()
        {
            var possibleValues = Enumerable.Range(0, CitiesIndexSequence.Length).ToList();
            var firstIndexToSwap = GetRandomValue(possibleValues);
            var secondIndexToSwap = GetRandomValue(possibleValues);

            var temp = CitiesIndexSequence[firstIndexToSwap];
            CitiesIndexSequence[firstIndexToSwap] = CitiesIndexSequence[secondIndexToSwap];
            CitiesIndexSequence[secondIndexToSwap] = temp;
        }

        private int GetRandomValue(IList<int> possibleValues)
        {
            var rand = new Random();
            var randomIndex = rand.Next(possibleValues.Count);
            var randomValue = possibleValues[randomIndex];
            possibleValues.RemoveAt(randomIndex);
            return randomValue;
        }
        
        private void MakeItemsSequence()
        {
            var knapsackItemsWeight = 0;
            for (var i = 0; i < CitiesIndexSequence.Length; i++)
            {
                var cityIndex = CitiesIndexSequence[i];
                var itemsInCity = Data.Items.Where(item => item.CityNumber == cityIndex).ToArray();
                if (itemsInCity.Length > 0)
                {
                    var pickedItem = FindBiggestValueItem(itemsInCity, knapsackItemsWeight);
                    if (pickedItem != null)
                    {
                        ItemsSequence[i] = pickedItem;
                        knapsackItemsWeight += pickedItem.Weight;
                    }
                }
                KnapsackWeightSequence[i] = knapsackItemsWeight;
            }
        }

        private Item FindBiggestValueItem(Item[] items, int knapsackItemsWeight)
        {
            Item pickedItem = null;
            var stack = new Stack<Item>(items.OrderByDescending(item => item.Profit).ToArray());
            while (stack.Count > 0 && pickedItem == null)
            {
                var nextItem = stack.Pop();
                if (knapsackItemsWeight + nextItem.Weight <= Data.CapacityOfKnapsack)
                {
                    pickedItem = nextItem;
                }
            }
            
            return pickedItem;
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