using System;
using System.Collections.Generic;
using System.Linq;
using SI_Zad_1.Algorithm;

namespace SI_Zad_1.Operators
{
    public class CrossoverCX
    {
        private Data Data;
        private List<int> Parent1;
        private List<int> Parent2;
        private int[] Child1;
        private int[] Child2;

        public CrossoverCX(Specimen parent1, Specimen parent2, Data data)
        {
            Data = data;
            Parent1 = parent1.CitiesIndexSequence.ToList();
            Parent2 = parent2.CitiesIndexSequence.ToList();
            Child1 = new int[Parent1.Count];
            Child2 = new int[Parent2.Count];
            var cycle = MakeCycle();
            MakeChildren(cycle);
        }

        public Tuple<Specimen, Specimen> GetChildren()
        {
            var childSpecimen1 = new Specimen(Data, Child1);
            var childSpecimen2 = new Specimen(Data, Child2);
            return Tuple.Create(childSpecimen1, childSpecimen2);
        }
        
        private List<int> MakeCycle()
        {
            var cycleList = new List<int> {Parent1[0]};

            int nextElem;
            do
            {
                var lastElemPosition = Parent1.IndexOf(cycleList.Last());
                nextElem = Parent2[lastElemPosition];
                if (nextElem != cycleList[0])
                {
                    cycleList.Add(nextElem);
                }
            } while (nextElem != cycleList[0]);

            return cycleList;
        }

        private void MakeChildren(List<int> cycle)
        {
            foreach (var elem in cycle)
            {
                var indexInFirstParent = Parent1.IndexOf(elem);
                Child1[indexInFirstParent] = elem;
            }

            for (var i = 0; i < Child1.Length; i++)
            {
                if (Child1[i] == 0)
                {
                    Child1[i] = Parent2[i];
                }
            }
            
            foreach (var elem in cycle)
            {
                var indexInSecondParent = Parent2.IndexOf(elem);
                Child2[indexInSecondParent] = elem;
            }

            for (var i = 0; i < Child2.Length; i++)
            {
                if (Child2[i] == 0)
                {
                    Child2[i] = Parent1[i];
                }
            }
        }
    }
}