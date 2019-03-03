using System;
using System.Collections.Generic;
using System.Text;
using SI_Zad_1.DataLoading;
using static System.Console;

namespace SI_Zad_1.Algorithm
{
    class Test
    {
        public void start()
        {
            WriteLine("Program start");
            var loader = new Loader();
            var loadedData = loader.LoadData(FilesNames.Trivial0);
            var data = new Data(
                loadedData.MinSpeed,
                loadedData.MaxSpeed,
                loadedData.Cities.ToArray(),
                loadedData.Items.ToArray()
            );
            WriteLine($"data:\n{data}");

            var population = GeneratePopulation(10, data);
            WriteLine("population:");
            foreach (var specimen in population)
            {
                WriteLine($"specimen = {specimen}");
                WriteLine($"first = {specimen.CitiesSequence[0]}");
            }
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
