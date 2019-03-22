using SI_Zad_1.DataLoading;
using static System.Console;

namespace SI_Zad_1.Algorithm
{
    class Test
    {
        public void Start()
        {
            var data = LoadData();
            var operators = new GeneticOperators(data);
            var population = operators.GeneratePopulation(Config.PopulationSize);
            operators.CalculateSpecimensValues(population);
            
            for (var i = 0; i < Config.GenerationsNumber; i++)
            {
                var selectedSpecimens = operators.TourSelection(population);
                var newPopulation = operators.Crossover(selectedSpecimens);
                operators.Mutation(newPopulation);
                operators.CalculateSpecimensValues(newPopulation);
                operators.PrintStats(newPopulation, i);
                population = newPopulation;
            }
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