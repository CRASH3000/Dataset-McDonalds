using System;
using System.Drawing;
using System.Windows.Forms;

namespace McDonalds
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false; // Отключение кнопки увеличения экрана
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Запрет изменения размера окна
            SetupForm();
        }

        private void SetupForm()
        {            
           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.TopLevel = false;
            form2.FormBorderStyle = FormBorderStyle.None;
            form2.Dock = DockStyle.Fill;
            this.Controls.Add(form2);
            form2.BringToFront();
            form2.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
