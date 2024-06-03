using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace McDonalds
{
    public partial class Form2 : Form
    {
        private DataTable menuData; // Таблица данных для хранения меню
        public Form2()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Загрузка данных из CSV файла в DataTable
            string filePath = @"Resources\dataset - menu McDonalds.csv";
            menuData = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                // Чтение заголовков столбцов
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    menuData.Columns.Add(header);
                }

                // Чтение строк данных
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = menuData.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    menuData.Rows.Add(dr);
                }
            }
        }

        // Метод, который вызывается при загрузке формы
        private void Form2_Load(object sender, EventArgs e)
        {
            string[] buttonNames = { "Все блюда", "Блюда 0 ккал", "McChiken и железо", "Сахар и Каллории", "Блюда с транс-жирами", "Средння доля Beef & Pork" };
            Image[] buttonImages = {
                Properties.Resources.AllMenuIcon,
                Properties.Resources.Water_Icon,
                Properties.Resources.How_Many_Macchiquins_Icon,
                Properties.Resources.SugarIcon,
                Properties.Resources.FatsIcon,
                Properties.Resources.BeefPorkIcon,
            };

            int buttonWidth = 120;
            int buttonHeight = 140;
            int buttonSpacing = 10;
            Font buttonFont = new Font("Arial", 10, FontStyle.Bold);

            // Создание кнопок и праметры кнопок 
            for (int i = 0; i < buttonNames.Length; i++)
            {
                Button button = new Button();
                button.Text = buttonNames[i];
                button.Size = new Size(buttonWidth, buttonHeight);
                button.Location = new Point(10, i * (buttonHeight + buttonSpacing));
                button.BackgroundImage = buttonImages[i];
                button.BackgroundImageLayout = ImageLayout.Stretch;
                button.Padding = new Padding(0, 80, 0, 0);
                button.ImageAlign = ContentAlignment.TopCenter;
                button.TextAlign = ContentAlignment.BottomCenter;
                button.Font = buttonFont;
                button.Click += Button_Click;
                this.buttonPanel.Controls.Add(button);
            }
        }

        // Метод для прокрутки панели с кнопками
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.buttonPanel.AutoScrollPosition = new Point(0, e.NewValue);
        }

        // Обработчик событий для кнопок
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null && clickedButton.Text == "Все блюда")
            {
                DisplayRightPanelContent();
            }
        }

        // Метод для отображения контента в правой панели
        private void DisplayRightPanelContent()
        {
            // Очистка правой панели
            this.rightPanel.Controls.Clear();

            int xOffset = 10;
            int yOffset = 10;
            int itemsPerRow = 3;
            int currentItem = 0;

            // Создание панелей для каждого продукта
            foreach (DataRow row in menuData.Rows)
            {
                Panel squarePanel = new Panel();
                squarePanel.Size = new Size(200, 200);
                squarePanel.Location = new Point(xOffset, yOffset);
                squarePanel.BorderStyle = BorderStyle.FixedSingle;

                Label label = new Label();
                label.Text = row["Item"].ToString();
                label.AutoSize = true;
                label.Location = new Point(10, 10);
                squarePanel.Controls.Add(label);

                Button placeholderButton = new Button();
                placeholderButton.Text = "⚠ О продукте";
                placeholderButton.Size = new Size(100, 50);
                placeholderButton.Location = new Point(10, 150);
                placeholderButton.Tag = row; 
                placeholderButton.Click += PlaceholderButton_Click; 
                squarePanel.Controls.Add(placeholderButton);

                this.rightPanel.Controls.Add(squarePanel);

                currentItem++;
                if (currentItem % itemsPerRow == 0)
                {
                    xOffset = 10; // Начало новой строки
                    yOffset += 210; // Увеличиваем yOffset для новой строки
                }
                else
                {
                    xOffset += 210; // Сдвигаем xOffset для следующего элемента в строке
                }
            }
            // Настройка прокрутки
            this.rightPanel.AutoScroll = true;
            this.rightPanel.HorizontalScroll.Enabled = false;
            this.rightPanel.HorizontalScroll.Visible = false;
            this.rightPanel.VerticalScroll.Enabled = true;
            this.rightPanel.VerticalScroll.Visible = true;
        }

        // Обработчик событий для кнопок "Заглушка"
        private void PlaceholderButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                DataRow productData = clickedButton.Tag as DataRow;
                if (productData != null)
                {
                    ShowProductInfo(productData); // Показать информацию о продукте
                }
            }
        }

        // Метод для отображения информации о продукте в новом окне
        private void ShowProductInfo(DataRow productData)
        {
            Form productInfoForm = new Form();
            productInfoForm.Text = "Product Information";
            productInfoForm.Size = new Size(400, 800);
            productInfoForm.StartPosition = FormStartPosition.CenterParent;

            int yOffset = 20;

            foreach (DataColumn column in productData.Table.Columns)
            {
                Label label = new Label();
                label.Text = $"{column.ColumnName}: {productData[column].ToString()}";
                label.AutoSize = true;
                label.Location = new Point(10, yOffset);
                productInfoForm.Controls.Add(label);
                yOffset += 30;
            }

            productInfoForm.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
