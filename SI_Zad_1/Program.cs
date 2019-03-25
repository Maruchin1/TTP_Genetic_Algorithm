using System;
using System.IO;
using System.Text;
using SI_Zad_1.Algorithm;
using static System.Console;

namespace SI_Zad_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var groupedResult = new string[Config.GenerationsNumber];
            for (var i = 0; i < Config.Repeats; i++)
            {
                WriteLine($"Test {i+1}");
                var test = new Test();
                var result = test.Start();
//                var result = test.StartRandom();
//                var result = test.StartGreedy();

                for (var j = 0; j < result.Length; j++)
                {
                    groupedResult[j] = groupedResult[j] + ";" + result[j];
                }
            }
            
            var csv = new StringBuilder();
            foreach (var line in groupedResult)
            {
                csv.AppendLine(line);
            }
            SaveData(csv.ToString());
            
            ReadLine();
        }
        
        private static void SaveData(string data)
        {
            var outputDir = Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\outputData\");
            var filesNum = outputDir.GetFiles().Length;
            var newFileName = $"file{filesNum+1}.csv";
            var newFilePath = Directory.GetCurrentDirectory() + @"\outputData\" + newFileName;
            File.WriteAllText(newFilePath, data);
        }
    }
}
