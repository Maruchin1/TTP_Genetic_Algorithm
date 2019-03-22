using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_Zad_1.Operators;
using static System.Console;

namespace SI_Zad_1.Algorithm
{
    public class GeneticOperators
    {
        private readonly Data _data;

        public GeneticOperators(Data data)
        {
            _data = data;
        }
        
        public void PrintStats(Specimen[] population, int generation)
        {
            var ranking = population.OrderByDescending(specimen => specimen.Value).ToArray();
            var best = ranking.First().Value;
            var worst = ranking.Last().Value;
            var average = ranking.Average(specimen => specimen.Value);
            
            var builder = new StringBuilder();
            builder.Append("Gen: ");
            builder.Append(generation);
            builder.Append(" | Best: ");
            builder.Append(best.ToString("0.##"));
            builder.Append(" | Avg: ");
            builder.Append(average.ToString("0.##"));
            builder.Append(" | Worst: ");
            builder.Append(worst.ToString("0.##"));
            
            WriteLine(builder.ToString());
        }
        
        public void PrintBest(Specimen[] population)
        {
            var best = population.OrderByDescending(specimen => specimen.Value).First();
            WriteLine($"Best = {best.Value}");
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
                var randomSpecimens = GetRandomSpecimens(Config.TourSize, population);
                var bestSpecimen = randomSpecimens.OrderByDescending(specimen => specimen.Value).First();
                selectedSpecimens[i] = bestSpecimen;
            }

            return selectedSpecimens;
        }

        public Specimen[] Crossover(Specimen[] population)
        {
            var newPopulation = new List<Specimen>();
            var stack = new Stack<Specimen>(population);
            while (stack.Count > 0)
            {
                var parent1 = stack.Pop();
                var parent2 = stack.Pop();
                var rand = new Random();
                var randomNumber = rand.NextDouble();
                if (randomNumber <= Config.CrossProb)
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
            var rand = new Random();
            foreach (var specimen in population)
            {
                var randomNumber = rand.NextDouble();
                if (randomNumber <= Config.MutationProb)
                {
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