using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

        public string GetStats(Specimen[] population, int generation)
        {
            var ranking = population.OrderByDescending(specimen => specimen.Value).ToArray();
            var best = ranking.First().Value;
            var worst = ranking.Last().Value;
            var average = ranking.Average(specimen => specimen.Value);

//            return new List<string>{best.ToString(), average.ToString(), worst.ToString()};
            
//            var classifier = new SpecimenClassifier(_data);
//            WriteLine("best: " + classifier.TotalItemsWeight(ranking.First()));
            
            return $"{best};{average};{worst}";
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

        public Specimen[] RouletteSelection(Specimen[] population)
        {
            var specimensList = new List<Specimen>(population.Length);
            foreach (var specimen in population)
            {
                specimensList.Add(new Specimen(specimen.Data, specimen.CitiesIndexSequence)
                {
                    ItemsSequence = specimen.ItemsSequence,
                    KnapsackWeightSequence = specimen.KnapsackWeightSequence,
                    Value = specimen.Value
                });
            }
            var ranking = specimensList.OrderByDescending(specimen => specimen.Value);
            var worstValue = ranking.Last().Value;
            specimensList.ForEach(specimen => specimen.Value -= worstValue);

            var sumOfFitness = specimensList.Sum(specimen => specimen.Value);
            var maxProbList = new List<double>();
            foreach (var specimen in specimensList)
            {
                var previousProb = 0.0;
                if (maxProbList.Count != 0)
                {
                    previousProb = maxProbList.Last();
                }
                var specimenMaxProb = previousProb + (specimen.Value / sumOfFitness);
                maxProbList.Add(specimenMaxProb);
            }
            
            var selectedSpecimens = new Specimen[population.Length];

            for (var i = 0; i < selectedSpecimens.Length; i++)
            {
                var rand = new Random();
                var randomProb = rand.NextDouble();
                for (var j = 0; j < maxProbList.Count; j++)
                {
                    if (randomProb > maxProbList[j]) continue;
                    selectedSpecimens[i] = population[j];
                    break;
                }
            }

            return selectedSpecimens;
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
//                    var crossover = new CrossoverCX(parent1, parent2, _data);
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
            
            foreach (var specimen in population)
            {
                var rand = new Random();
                var randomNumber = rand.NextDouble();
                if (randomNumber <= Config.MutationProb)
                {
//                    specimen.Mutate();
                    specimen.Mutate2();
                }
            }
        }
        
        private Specimen[] GetRandomSpecimens(int count, IReadOnlyList<Specimen> population)
        {
            var randomSpecimens = new Specimen[count];
            var possibleSpecimens = population.ToList();
            for (var i = 0; i < count; i++)
            {
                var rand = new Random();
                var randomIndex = rand.Next(possibleSpecimens.Count);
                randomSpecimens[i] = possibleSpecimens[randomIndex];
                possibleSpecimens.RemoveAt(randomIndex);
            }

            return randomSpecimens;
        }
        
        private CultureInfo MakeCultureInfo()
        {
            var culture = CultureInfo.CurrentCulture.Clone() as CultureInfo;
            if (culture != null)
            {
                culture.NumberFormat.NumberDecimalSeparator = ".";
            }

            return culture;
        }
    }
}