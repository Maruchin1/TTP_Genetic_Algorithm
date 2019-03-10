using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_Zad_1.Algorithm;
using static System.Console;

namespace SI_Zad_1.Operators
{
    public class CrossoverPmx
    {
        private Data Data;
        private int[] Parent1;
        private int[] Parent2;
        private int[] Child1;
        private int[] Child2;

        public CrossoverPmx(Specimen parent1, Specimen parent2, Data data)
        {
            Data = data;
            Parent1 = parent1.CitiesIndexSequence;
            Parent2 = parent2.CitiesIndexSequence;
            Child1 = MakeCopy(Parent1);
            Child2 = MakeCopy(Parent2);
            var (startIndex, endIndex) = CutAndSwapRandomPart();
            var relationMap = CreateRelationMap(startIndex, endIndex);
            FillChildren(relationMap, startIndex, endIndex);
        }

        public Tuple<Specimen, Specimen> GetChildren()
        {
            var childSpecimen1 = new Specimen(Data, Child1);
            var childSpecimen2 = new Specimen(Data, Child2);
            return Tuple.Create(childSpecimen1, childSpecimen2);
        }

        private int[] MakeCopy(int[] array)
        {
            var list = new List<int>(array);
            return list.ToArray();
        }
        
        private (int, int) CutAndSwapRandomPart()
        {
            var parentLength = Parent1.Length;
            var rand1 = new Random();
            var rand2 = new Random();

            var randomStartIndex = rand1.Next(parentLength-1);
            int randomEndIndex;
            do
            {
                randomEndIndex = rand2.Next(parentLength-1);
            } while (randomEndIndex == randomStartIndex);

            if (randomStartIndex > randomEndIndex)
            {
                var temp = randomStartIndex;
                randomStartIndex = randomEndIndex;
                randomEndIndex = temp;
            }

            for (var i = randomStartIndex; i <= randomEndIndex; i++)
            {
                Child1[i] = Parent2[i];
                Child2[i] = Parent1[i];
            }
            
            return (randomStartIndex, randomEndIndex);
        }
        
        public List<Tuple<int, int>> CreateRelationMap(int startIndex, int endIndex)
        {
            var tuplesList = new List<Tuple<int, int>>();
            for (var i = startIndex; i <= endIndex; i++)
            {
                var tuple = Tuple.Create(Child1[i], Child2[i]);
                tuplesList.Add(tuple);
            }

            tuplesList.RemoveAll(tuple => tuple.Item1.Equals(tuple.Item2));
            
            for (var i = 0; i < tuplesList.Count; i++)
            {
                var currTuple = tuplesList[i];
                Tuple<int, int> nextWithSameItem;
                do
                {
                    nextWithSameItem = tuplesList.FirstOrDefault(tuple =>
                        !tuple.Equals(currTuple) &&
                        (tuple.Item1.Equals(currTuple.Item2) || tuple.Item2.Equals(currTuple.Item1)));
                    if (nextWithSameItem != null)
                    {
                        if (currTuple.Item1.Equals(nextWithSameItem.Item2) && currTuple.Item2.Equals(nextWithSameItem.Item1))
                        {
                            tuplesList.Remove(currTuple);
                            tuplesList.Remove(nextWithSameItem);
                            if (tuplesList.Count > i)
                            {
                                currTuple = tuplesList[i];
                            }
                        }
                        else
                        {
                            currTuple = JoinTuples(currTuple, nextWithSameItem);
                            tuplesList[i] = currTuple;
                            tuplesList.Remove(nextWithSameItem);
                        }
                    }
                } while (nextWithSameItem != null);
            } 
            
            return tuplesList;
        }

        private Tuple<int, int> JoinTuples(Tuple<int, int> tuple1, Tuple<int, int> tuple2)
        {
            Tuple<int, int> newTuple;
            if (tuple1.Item1.Equals(tuple2.Item2) && tuple1.Item2.Equals(tuple2.Item1))
            {
                newTuple = tuple1;
            }
            else if (tuple1.Item1.Equals(tuple2.Item2))
            {
                newTuple = new Tuple<int, int>(tuple2.Item1, tuple1.Item2);
            }
            else 
            {
                newTuple = new Tuple<int, int>(tuple1.Item1, tuple2.Item2);
            }

            return newTuple;
        }

        private void FillChildren(List<Tuple<int, int>> relationMap, int startIndex, int endIndex)
        {
//            WriteLine($"Child1 = {ArrayToString(Child1)}");
//            WriteLine($"Child2 = {ArrayToString(Child2)}");
//            WriteLine($"startIndex = {startIndex}");
//            WriteLine($"endIndex = {endIndex}");
//            WriteLine($"relationMap = \n{RelationMapToString(relationMap)}");
            foreach (var relation in relationMap)
            {
                var (item1, item2) = relation;
//                WriteLine($"tuple = {item1}|{item2}");
                var firstItemIndex = FindItemIndex(item1, Parent1, startIndex, endIndex);
                if (firstItemIndex != -1)
                {
                    var secondItemIndex = FindItemIndex(item2, Parent2, startIndex, endIndex);
//                    WriteLine($"first index = {firstItemIndex}");
//                    WriteLine($"second index = {secondItemIndex}");
                    Child1[firstItemIndex] = Parent2[secondItemIndex];
                    Child2[secondItemIndex] = Parent1[firstItemIndex];
                }
                else
                {
                    firstItemIndex = FindItemIndex(item1, Parent2, startIndex, endIndex);
                    var secondItemIndex = FindItemIndex(item2, Parent1, startIndex, endIndex);
                    
//                    WriteLine($"first index = {firstItemIndex}");
//                    WriteLine($"second index = {secondItemIndex}");
                    Child1[secondItemIndex] = Parent2[firstItemIndex];
                    Child2[firstItemIndex] = Parent1[secondItemIndex];
                }
            }
        }

        private int FindItemIndex(int item, int[] array, int startIndex, int endIndex)
        {
            var index = -1;
            for (var i = 0; i < startIndex && index == -1; i++)
            {
                var currItem = array[i];
                if (currItem.Equals(item))
                {
                    index = i;
                }
            }

            for (var i = endIndex+1; i < array.Length && index == -1; i++)
            {
                var currItem = array[i];
                if (currItem.Equals(item))
                {
                    index = i;
                }
            }

            return index;
        }

        private string ArrayToString(int[] array)
        {
            var builder = new StringBuilder();
            builder.Append("|");
            foreach (var item in array)
            {
                builder.Append(item);
                builder.Append("|");
            }

            return builder.ToString();
        }

        private string RelationMapToString(List<Tuple<int, int>> map)
        {
            var builder = new StringBuilder();
            foreach (var tuple in map)
            {
                builder.Append(tuple.Item1);
                builder.Append("|");
                builder.Append(tuple.Item2);
                builder.Append("\n");
            }

            return builder.ToString();
        }
    }
}