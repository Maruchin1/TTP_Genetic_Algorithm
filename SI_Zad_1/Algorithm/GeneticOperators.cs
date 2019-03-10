using System;
using System.Collections.Generic;
using System.Linq;
using SI_Zad_1.Operators;
using static System.Console;

namespace SI_Zad_1.Algorithm
{
    public class GeneticOperators
    {
        private const double CrossProb = 0.7;
        private const double MutationProb = 0.01;
        private const int TourSize = 5;
        private readonly Data _data;

        public GeneticOperators(Data data)
        {
            _data = data;
        }

        public void PrintPopulation(Specimen[] population)
        {
            WriteLine($"Population -----------------------------------------------");
            foreach (var specimen in population)
            {
                WriteLine($"Specimen --------------");
                WriteLine(specimen.ToString());
            }
        }
        public Specimen[] GeneratePopulation(int size)
        {
            var population = new Specimen[size];
            for (var i = 0; i < population.Length; i++)
            {
                population[i] = Specimen.GenerateRandom(_data);
            }

            return population;
        }
        
        public void CalculateSpecimensValues(Specimen[] population)
        {
            var classifier = new SpecimenClassifier(_data);
            foreach (var specimen in population)
            {
                specimen.Value = classifier.CalculateSpecimenValue(specimen);
            }
        }
        
        public Specimen[] TourSelection(Specimen[] population)
        {
            var selectedSpecimens = new Specimen[population.Length];

            for (var i = 0; i < selectedSpecimens.Length; i++)
            {
                var randomSpecimens = GetRandomSpecimens(TourSize, population);
                var bestSpecimen = randomSpecimens.OrderByDescending(specimen => specimen.Value).First();
                selectedSpecimens[i] = bestSpecimen;
            }

            return selectedSpecimens;
        }

        public Specimen[] Crossover(Specimen[] population)
        {
            var stack = new Stack<Specimen>(population);
            var newPopulation = new List<Specimen>();
            while (stack.Count > 0)
            {
                var parent1 = stack.Pop();
                var parent2 = stack.Pop();
                var rand = new Random();
                var randomNumber = rand.NextDouble();
                if (randomNumber <= CrossProb)
                {
                    var crossover = new CrossoverPmx(parent1, parent2, _data);
                    var (child1, child2) = crossover.GetChildren();
                    newPopulation.Add(child1);
                    newPopulation.Add(child2);
                }
                else
                {
                    newPopulation.Add(parent1);
                    newPopulation.Add(parent2);
                }
            }

            return newPopulation.ToArray();
        }
        
        public void Mutation(Specimen[] population)
        {
            WriteLine("Mutation ----------------------------------------------");
            var rand = new Random();
            foreach (var specimen in population)
            {
                var randomNumber = rand.NextDouble();
                if (randomNumber <= MutationProb)
                {
                    WriteLine("Mutation");
                    specimen.Mutate();
                }
            }
        }
        
        private Specimen[] GetRandomSpecimens(int count, IReadOnlyList<Specimen> population)
        {
            var randomSpecimens = new Specimen[count];
            for (var i = 0; i < count; i++)
            {
                var rand = new Random();
                var randomIndex = rand.Next(population.Count);
                randomSpecimens[i] = population[randomIndex];
            }

            return randomSpecimens;
        }
    }
}