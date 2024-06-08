using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace McDonalds
{
    public partial class CalculateAverageElementRatio : Form
    {
        public CalculateAverageElementRatio()
        {
            InitializeComponent();
            comboElementBox.Items.Add("Жиры");   //5 столбец
            comboElementBox.Items.Add("Насыщенные жиры");  //7 столбец
            comboElementBox.Items.Add("Транс-жиры");  //9 столбец
            comboElementBox.Items.Add("Холестерин");   //10 столбец
            comboElementBox.Items.Add("Натрий"); //12 столбец
            comboElementBox.Items.Add("Углеводы"); //14 столбец
            comboElementBox.Items.Add("Пищевые волокна"); //16 столбец
            comboElementBox.Items.Add("Сахар"); //18 столбец
            comboElementBox.Items.Add("Белок"); //19 столбец
            comboElementBox.SelectedIndex = 2;

            comboCategoryBox.Items.Add("Breakfast");
            comboCategoryBox.Items.Add("Beef & Pork");
            comboCategoryBox.Items.Add("Chicken & Fish");
            comboCategoryBox.Items.Add("Salads");
            comboCategoryBox.Items.Add("Snacks & Sides");
            comboCategoryBox.Items.Add("Desserts");

            comboCategoryBox.SelectedIndex = 1;
        }

        private void Start_Search(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            int i = 0;
            string element = comboElementBox.SelectedItem.ToString();
            string category = comboCategoryBox.SelectedItem.ToString();
            string pattern = @"([0-9]+)\b";
            //string servingSize;


            dataGridView.Columns[2].HeaderText = "Средняя доля в: " + element;
            if (element == "Жиры")
                i = 5;
            if (element == "Насыщенные жиры")
                i = 7;
            if (element == "Транс-жиры")
                i = 9;
            if (element == "Холестерин")
                i = 10;
            if (element == "Натрий")
                i = 12;
            if (element == "Углеводы")
                i = 14;
            if (element == "Пищевые волокна")
                i = 16;
            if (element == "Сахар")
                i = 18;
            if (element == "Белок")
                i = 19;
            foreach (string[] word in SharedData.Data)
            {


                if (word[0] == category)
                {
                    if (Double.Parse(word[i], CultureInfo.InvariantCulture) != 0)
                    {
                        var servingSize = Regex.Match(word[2].Substring(word[2].IndexOf('(')), pattern).Value;
                        var averageRatio = Double.Parse(word[i], CultureInfo.InvariantCulture) / double.Parse(servingSize);
                        dataGridView.Rows.Add(word[0], word[1], averageRatio, servingSize);
                    }
                }
            }
        }


        /*private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged_1(object sender, EventArgs e)
        {

        }*/
    }
}
