using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SI_Zad_1.Algorithm;

namespace SI_Zad_1.DataLoading
{
    class Loader
    {
        private const string ProblemName = "PROBLEM NAME";
        private const string KnapsackDataType = "KNAPSACK DATA TYPE";
        private const string Dimension = "DIMENSION";
        private const string NumberOfItems = "NUMBER OF ITEMS";
        private const string CapacityOfKnapsack = "CAPACITY OF KNAPSACK";
        private const string MinSpeed = "MIN SPEED";
        private const string MaxSpeed = "MAX SPEED";
        private const string RentingRatio = "RENTING RATIO";
        private const string EdgeWeightType = "EDGE_WEIGHT_TYPE";
        private const string NodeCoordSection = "NODE_COORD_SECTION	(INDEX, X, Y)";
        private const string ItemsSection = "ITEMS SECTION	(INDEX, PROFIT, WEIGHT, ASSIGNED NODE NUMBER)";

        private readonly LoadedData _loadedData;

        public Loader()
        {
            _loadedData = new LoadedData();
        }

        public LoadedData LoadData(string fileName)
        {
            var fileReader = new FileReader();
            var dataString = fileReader.LoadFromFile(fileName);

            var dataLines = dataString.Split(Environment.NewLine.ToCharArray());
            var notEmptyDataLines = dataLines.Where(line => !string.IsNullOrEmpty(line));
            IterateOverLines(notEmptyDataLines.ToList());
            return _loadedData;
        }

        private void IterateOverLines(List<string> dataLines)
        {
            dataLines.Reverse();
            var linesStack = new Stack<string>(dataLines);

            while (linesStack.Count > 0)
            {
                var line = linesStack.Pop();
                var keyValue = line.Split(":");
                var key = keyValue[0].Trim();
                var value = keyValue[1].Trim();
                MatchKeyValue(key, value, linesStack);
            }
        }

        private void MatchKeyValue(string key, string value, Stack<string> linesStack)
        {
            switch (key)
            {
                case ProblemName:
                    _loadedData.ProblemName = value;
                    break;
                case KnapsackDataType:
                    _loadedData.KnapsackDataType = value;
                    break;
                case Dimension:
                    _loadedData.Dimension = int.Parse(value);
                    break;
                case NumberOfItems:
                    _loadedData.NumberOfItems = int.Parse(value);
                    break;
                case CapacityOfKnapsack:
                    _loadedData.CapacityOfKnapsack = int.Parse(value);
                    break;
                case MinSpeed:
                    _loadedData.MinSpeed = double.Parse(value, MakeCultureInfo());
                    break;
                case MaxSpeed:
                    _loadedData.MaxSpeed = double.Parse(value, MakeCultureInfo());
                    break;
                case RentingRatio:
                    _loadedData.RentingRatio = double.Parse(value, MakeCultureInfo());
                    break;
                case EdgeWeightType:
                    _loadedData.EdgeWeightType = value;
                    break;
                case NodeCoordSection:
                    LoadNodeCoordData(linesStack);
                    break;
                case ItemsSection:
                    LoadItemsData(linesStack);
                    break;
            }
        }

        private void LoadNodeCoordData(Stack<string> linesStack)
        {
            var citiesCount = _loadedData.Dimension;
            _loadedData.Cities = new List<City>(citiesCount);

            for (var i = 0; i < citiesCount; i++)
            {
                var line = linesStack.Pop();
                var inLineData = line.Split("\t");
                var city = new City
                {
                    Index = int.Parse(inLineData[0]),
                    CoordX = float.Parse(inLineData[1], MakeCultureInfo()),
                    CoordY = float.Parse(inLineData[2], MakeCultureInfo())
                };
                _loadedData.Cities.Add(city);
            }
        }

        private void LoadItemsData(Stack<string> linesStack)
        {
            var itemsCount = _loadedData.NumberOfItems;
            _loadedData.Items = new List<Item>(itemsCount);

            for (var i = 0; i < itemsCount; i++)
            {
                var line = linesStack.Pop();
                var inLineData = line.Split("\t");
                var item = new Item
                {
                    Index = int.Parse(inLineData[0]),
                    Profit = int.Parse(inLineData[1]),
                    Weight = int.Parse(inLineData[2]),
                    CityNumber = int.Parse(inLineData[3])
                };
                _loadedData.Items.Add(item);
            }
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
