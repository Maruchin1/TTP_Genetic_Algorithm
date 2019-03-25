using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SI_Zad_1.DataLoading;
using static System.Console;

namespace SI_Zad_1.Algorithm
{
    class Test
    {
        public string[] Start()
        {
            var data = LoadData();
            var result = new string[Config.GenerationsNumber];
//            var stringBuilder = new StringBuilder();
            var operators = new GeneticOperators(data);
            var population = operators.GeneratePopulation(Config.PopulationSize);
            operators.CalculateSpecimensValues(population);
            
            WriteLine("Capacity of knapsack = " + data.CapacityOfKnapsack);
            
            for (var i = 0; i < Config.GenerationsNumber; i++)
            {
                var selectedSpecimens = operators.TourSelection(population);
//                var selectedSpecimens = operators.RouletteSelection(population);
                var newPopulation = operators.Crossover(selectedSpecimens);
                operators.Mutation(newPopulation);
                operators.CalculateSpecimensValues(newPopulation);
                var stats = operators.GetStats(newPopulation, i);
                WriteLine($"{i}   {stats} ");
                result[i] = stats;
//                stringBuilder.AppendLine(string.Join(";", stats));
                population = newPopulation;
            }

            return result;
//            SaveData(stringBuilder.ToString());
        }

        public string[] StartRandom()
        {
            var data = LoadData();
            var result = new string[Config.GenerationsNumber];
            var operators = new GeneticOperators(data);

            var bestValue = double.NegativeInfinity;
            
            for (var i = 0; i < Config.GenerationsNumber; i++)
            {
                var randomPopulation = operators.GeneratePopulation(Config.PopulationSize);
                operators.CalculateSpecimensValues(randomPopulation);
                var ranking = randomPopulation.OrderByDescending(specimen => specimen.Value);
                var currBestValue = ranking.First().Value;
                if (currBestValue > bestValue)
                {
                    bestValue = currBestValue;
                }

                WriteLine($"{i}   {bestValue} ");
                result[i] = bestValue.ToString();
            }

            return result;
        }

        public string[] StartGreedy()
        {
            var data = LoadData();
            var distanceMatrix = data.DistanceMatrix;
            DistanceMatrixBuilder.MatrixToString(data.DistanceMatrix);
            var result = new string[Config.GenerationsNumber];

            var bestValue = double.NegativeInfinity;

            var allCities = data.Cities.ToArray();

            for (var j = 0; j < allCities.Length; j++)
            {
                var city = allCities[j];
                var citiesSequence = new List<City>(allCities.Length);
                var nonVisitedCities = new List<City>(allCities);
                
                citiesSequence.Add(city);
                nonVisitedCities.Remove(city);

                while (nonVisitedCities.Count != 0)
                {
                    var lastCity = citiesSequence.Last();
                    var otherCities = nonVisitedCities.Where(otherCity => otherCity.Index != lastCity.Index).ToArray();
                    var shortestDistance = otherCities.Min(otherCity => 
                        distanceMatrix[lastCity.Index - 1, otherCity.Index - 1]);
                    var closestCity = otherCities.First(otherCity =>
                        distanceMatrix[otherCity.Index - 1, lastCity.Index - 1].Equals(shortestDistance));
                    
                    citiesSequence.Add(closestCity);
                    nonVisitedCities.Remove(closestCity);
                }

                var citiesIndexSequence = new int[citiesSequence.Count];
                for (var i = 0; i < citiesSequence.Count; i++)
                {
                    citiesIndexSequence[i] = citiesSequence[i].Index;
                }
                
                var specimen = new Specimen(data, citiesIndexSequence);
                
                var classifier = new SpecimenClassifier(data);
                specimen.Value = classifier.CalculateSpecimenValue(specimen);
                if (specimen.Value > bestValue)
                {
                    bestValue = specimen.Value;
                }

                WriteLine($"{j}   {bestValue} ");
                result[j] = bestValue.ToString();
            }

            return result;
        }
        
        private Data LoadData()
        {
            var loader = new Loader();
            var loadedData = loader.LoadData(Config.FileName);
            var data = new Data(
                loadedData.CapacityOfKnapsack,
                loadedData.MinSpeed,
                loadedData.MaxSpeed,
                loadedData.Cities.ToArray(),
                loadedData.Items.ToArray()
            );
            return data;
        }
    }
}