using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Console;

namespace SI_Zad_1.DataLoading
{
    class FileReader
    {
        public string LoadFromFile(string fileName)
        {
            StreamReader sr = null;
            string data = null;
            try
            {
                sr = new StreamReader(fileName);
                data = sr.ReadToEnd();
            }
            catch (Exception e)
            {
                WriteLine(e);
                throw;
            }
            finally
            {
                sr?.Close();
            }
            WriteLine($"data:\n{data}");
            return data;
        }
    }
}
