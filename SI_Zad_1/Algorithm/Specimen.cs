using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_Zad_1.Algorithm
{
    public class Specimen
    {
        public Data Data { get; set; }
        public City[] CitiesSequence { get; set; }

        public Item[] ItemsSequence { get; set; }

        public double Value { get; set; }

        public Specimen(Data data, City[] citiesSequence)
        {
            Data = data;
            CitiesSequence = citiesSequence;
            ItemsSequence = MakeItemsSequence();
        } 
        
        private Item[] MakeItemsSequence()
        {
            var itemsSequence = new Item[CitiesSequence.Length];
            var knapsackItemsWeight = 0;
            for (var i = 0; i < CitiesSequence.Length; i++)
            {
                var cityIndex = CitiesSequence[i].Index;
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
            var citiesSequence = new City[sequenceLength];
            var possibleValues = data.Cities.ToList();
            for (var i = 0; i < sequenceLength; i++)
            {
                var rand = new Random();
                var randomIndex = rand.Next(0, possibleValues.Count);
                citiesSequence[i] = possibleValues[randomIndex];
                possibleValues.RemoveAt(randomIndex);
            }

            return new Specimen(data, citiesSequence);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("Cities sequence:");
            builder.Append("\n|");
            foreach (var city in CitiesSequence)
            {
                builder.Append(city.Index);
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