using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SI_Zad_1.DataLoading;
using static System.Console;

namespace SI_Zad_1.Algorithm
{
    class Test
    {
        private const string FileName = FilesNames.Trivial0;
        private const int PopulationSize = 10;

        public void Start()
        {
            var data = LoadData();
            WriteLine($"data:\n{data}");
            var population = GeneratePopulation(PopulationSize, data);
            CalculateSpecimensValues(population, data);
            population[population.Length-1].Mutate();
        }

        private void CalculateSpecimensValues(IEnumerable<Specimen> population, Data data)
        {
            var classifier = new SpecimenClassifier(data);
            foreach (var specimen in population)
            {
                specimen.Value = classifier.CalculateSpecimenValue(specimen);
                WriteLine($"Specimen ------------------------------------------");
                WriteLine(specimen.ToString());
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

        private Specimen[] GeneratePopulation(int size, Data data)
        {
            var population = new Specimen[size];
            for (var i = 0; i < population.Length; i++)
            {
                population[i] = Specimen.GenerateRandom(data);
            }

            return population;
        }
    }
}