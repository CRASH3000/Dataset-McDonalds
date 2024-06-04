using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace McDonalds
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            comboBox1.Items.Add("Жиры");   //5     добавляем значения в раскрывающийся список 
            comboBox1.Items.Add("Насыщенные жиры");  //7
            comboBox1.Items.Add("Транс-жиры");  //9
            comboBox1.Items.Add("Холестерин");   //10
            comboBox1.Items.Add("Натрий"); //12
            comboBox1.Items.Add("Углеводы"); //14
            comboBox1.Items.Add("Пищевые волокна"); //16
            comboBox1.Items.Add("Сахар"); //18
            comboBox1.Items.Add("Белок"); //19

            comboBox1.SelectedIndex = 0;
        }

        private void Zapusk_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            string change = comboBox1.SelectedItem.ToString();    // считываем значения из раскрывающегося списка и выясняем номер столбца в data
            dataGridView1.Columns[2].HeaderText = change;
            if (change == "Жиры")
                i = 5;
            if (change == "Насыщенные жиры")
                i = 7;
            if (change == "Транс-жиры")
                i = 9;
            if (change == "Холестерин")
                i = 10;
            if (change == "Натрий")
                i = 12;
            if (change == "Углеводы")
                i = 14;
            if (change == "Пищевые волокна")
                i = 16;
            if (change == "Сахар")
                i = 18;
            if (change == "Белок")
                i = 19;
            foreach (string[] word in SharedData.Data)                                       // добавляем в таблицу строки, соотвествующеие критерию 
                if (Double.Parse(word[i], CultureInfo.InvariantCulture) != 0 )
                {
                    dataGridView1.Rows.Add(word[0], word[1], word[i], word[2]);
                }
        }

        private void textBox2_TextChanged(object sender, EventArgs e) //лишнее
        {

        }
    }
}
