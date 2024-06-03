using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace McDonalds
{
    public partial class Form3 : Form
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



        public Form3()
        {
            List<string[]> words = new List<string[]>();
            foreach (string line in File.ReadAllLines("Resources\\dataset - menu McDonalds.csv").Skip(1))
            {
                words.Add(SplitByCommaWithoutQuotes(line));     //Загружаем файл в коллекцию массивов строк 
            }
            SharedData.Data = words;
            InitializeComponent();
            comboBox1.Items.Add("Холестерин");   //10     добавляем значения в раскрывающийся список 
            comboBox1.Items.Add("Натрий");  //12
            comboBox1.Items.Add("Углеводы");  //14
            comboBox1.Items.Add("Сахар");   //18
            comboBox1.Items.Add("Белок"); //19
            comboBox1.SelectedIndex = 0;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }  //лишнее

        private void Zapusk_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            string change = comboBox1.SelectedItem.ToString();    // считываем значения из раскрывающегося списка и выясняем номер столбца в data
            if (change == "Холестерин")
                i = 10;
            if (change == "Натрий")
                i = 12;
            if (change == "Углеводы")
                i = 14;
            if (change == "Сахар")
                i = 18;
            if (change == "Белок")
                i = 19;

            if (double.TryParse(MinAtextBox1.Text, out double elemMin) &
                double.TryParse(MaxAtextBox2.Text, out double elemMax) &
                double.TryParse(textBox1.Text, out double kalorMin) &
                double.TryParse(textBox2.Text, out double kalorMax))
            {
                foreach (string[] word in SharedData.Data)                                       // добавляем в таблицу строки, соотвествующеие критерию 
                    if ((Double.Parse(word[i]) < elemMax & Double.Parse(word[i]) > elemMin)
                       & (Double.Parse(word[3]) < kalorMax & Double.Parse(word[3]) > kalorMin))
                    {
                        dataGridView1.Rows.Add(word[0], word[1], word[i], word[3], word[2]);
                    }
            }
            else
            {
                MessageBox.Show("Введено некорректное значение. Попробуйте ещё раз!");
                MinAtextBox1.Clear();
                MaxAtextBox2.Clear();
                textBox2.Clear();
                textBox1.Clear();
            }
        }
    }
    public static class SharedData    //Открываем общий доступ к файлу Data 
    {
        public static List<string[]> Data { get; set; }
    }


}
