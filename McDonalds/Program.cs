using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace McDonalds
{
    public static class SharedData    //Открываем общий доступ к файлу Data 
    {
        public static List<string[]> Data { get; set; }
    }



    internal static class Program
    {

        static string[] SplitByCommaWithoutQuotes(string input)   //Пропускаем выражения в кавычках при разбиении на пункты
        {
            List<string> parts = new List<string>();
            bool insideQuotes = false;
            int startIndex = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '"')
                {
                    insideQuotes = !insideQuotes;
                }
                else if (input[i] == ',' && !insideQuotes)
                {
                    parts.Add(input.Substring(startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            parts.Add(input.Substring(startIndex));

            return parts.ToArray();
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<string[]> words = new List<string[]>();
            foreach (string line in File.ReadAllLines("Resources\\dataset - menu McDonalds.csv").Skip(1))
            {
                words.Add(Program.SplitByCommaWithoutQuotes(line));     //Загружаем файл в коллекцию массивов строк 
            }
            SharedData.Data = words;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
