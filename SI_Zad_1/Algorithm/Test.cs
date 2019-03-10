using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SI_Zad_1.DataLoading;
using SI_Zad_1.Operators;
using static System.Console;

namespace SI_Zad_1.Algorithm
{
    class Test
    {
        private const string FileName = FilesNames.Trivial0;
        private const int PopulationSize = 6;
        private const int GenerationsNumber = 1;

        public void Start()
        {
            var data = LoadData();
            WriteLine($"data:\n{data}");
            
            var operators = new GeneticOperators(data);
            var population = operators.GeneratePopulation(PopulationSize);
            operators.CalculateSpecimensValues(population);
            operators.PrintPopulation(population);
            
            for (var i = 0; i < GenerationsNumber; i++)
            {
                var selectedSpecimens = operators.TourSelection(population);
                var newPopulation = operators.Crossover(selectedSpecimens);
                operators.Mutation(newPopulation);
                operators.CalculateSpecimensValues(newPopulation);
                WriteLine($"Generation = {i}");
                operators.PrintPopulation(newPopulation);
            }
        }

        private Data LoadData()
        {
            var loader = new Loader();
            var loadedData = loader.LoadData(FileName);
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