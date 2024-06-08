using System;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.WindowsForms;


namespace McDonalds
{
    public partial class Form2 : Form
    {
        private DataTable menuData; // Таблица данных для хранения меню
        private OxyPlot.WindowsForms.PlotView plotView = new OxyPlot.WindowsForms.PlotView(); // График для отображения БЖУ
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
            string[] buttonNames = { "Все блюда", 
                "Содержание БЖУ блюд", 
                "Распределение блюд по порции и калориям", 
                "Блюда 0 ккал", 
                "Железо в порциях", 
                "Сахар и Каллории", 
                "Блюда с транс-жирами", 
                "Средння доля Beef & Pork" };
            Image[] buttonImages = {
                Properties.Resources.AllMenuIcon,
                Properties.Resources.ИконкаБЖУ,
                Properties.Resources.РаспределениеБлюдИконка,
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
            if (clickedButton != null)
            {
                
                string text = clickedButton.Text;
                switch (text)
                {
                    case "Все блюда":
                        DisplayRightPanelContent();
                        break;
                    case "Содержание БЖУ блюд":
                        DisplaySearchProductContent();
                        break;
                    case "Распределение блюд по порции и калориям":
                        DisplayScatterPlot();
                        break;
                    case "Железо в порциях":
                        DisplayIronRequirementContent();
                        break;
                    case "Блюда 0 ккал":
                        DisplayZeroCalorieDishes();
                        break;

                }
                if (clickedButton != null && clickedButton.Text == "Сахар и Каллории")
                {
                    Form3 form3 = new Form3();
                    form3.TopLevel = false;
                    form3.FormBorderStyle = FormBorderStyle.None;
                    form3.Dock = DockStyle.Fill; // Form3 заполнит весь rightPanel

                    // Очистка Panel от предыдущих форм
                    rightPanel.Controls.Clear();

                    // Добавление Form3 в rightPanel
                    rightPanel.Controls.Add(form3);
                    form3.Show();
                }
                if (clickedButton != null && clickedButton.Text == "Блюда с транс-жирами")
                {
                    Form4 form4 = new Form4();
                    form4.TopLevel = false;
                    form4.FormBorderStyle = FormBorderStyle.None;
                    form4.Dock = DockStyle.Fill; // Form3 заполнит весь rightPanel

                    // Очистка Panel от предыдущих форм
                    rightPanel.Controls.Clear();

                    // Добавление Form3 в rightPanel
                    rightPanel.Controls.Add(form4);
                    form4.Show();
                }
                if (clickedButton != null && clickedButton.Text == "Средння доля Beef & Pork")
                {
                    CalculateAverageElementRatio calculateAverageElementRatio = new CalculateAverageElementRatio();
                    calculateAverageElementRatio.TopLevel = false;
                    calculateAverageElementRatio.FormBorderStyle = FormBorderStyle.None;
                    calculateAverageElementRatio.Dock = DockStyle.Fill;
                    // Очистка Panel от предыдущих форм
                    rightPanel.Controls.Clear();

                    rightPanel.Controls.Add(calculateAverageElementRatio);
                    calculateAverageElementRatio.Show();
                }
            }
        }
        private void DisplaySearchProductContent()
        {
            // Очистка правой панели
            this.rightPanel.Controls.Clear();

            // Создание заголовка
            Label titleLabel = new Label();
            titleLabel.Text = "Сравнение суточной нормы БЖУ и БЖУ блюда";
            titleLabel.Location = new Point(10, 10);
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font(titleLabel.Font.FontFamily, 16, FontStyle.Bold);
            this.rightPanel.Controls.Add(titleLabel);

            // Создание метки для комбинированного ввода и списка
            Label searchLabel = new Label();
            searchLabel.Text = "Введите название блюда";
            searchLabel.Location = new Point(10, 40);
            searchLabel.Size = new Size(200, 20);
            this.rightPanel.Controls.Add(searchLabel);

            // Создание ComboBox для ввода и списка блюд
            ComboBox dishComboBox = new ComboBox();
            dishComboBox.Location = new Point(10, 70);
            dishComboBox.Size = new Size(200, 20);
            dishComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            this.rightPanel.Controls.Add(dishComboBox);

            // Настройка поиска блюд
            dishComboBox.TextChanged += (sender, e) =>
            {
                string query = dishComboBox.Text.ToLower();
                var filteredDishes = menuData.AsEnumerable()
                    .Where(row => row.Field<string>("Item").ToLower().Contains(query))
                    .Select(row => row.Field<string>("Item"))
                    .ToList();

                dishComboBox.Items.Clear();
                dishComboBox.Items.AddRange(filteredDishes.ToArray());
                dishComboBox.DroppedDown = true; // Открыть выпадающий список
                dishComboBox.SelectionStart = query.Length; // Установить курсор в конец текста
                dishComboBox.SelectionLength = 0; // Убрать выделение
            };

            // Настройка действия при выборе блюда из списка
            dishComboBox.SelectedIndexChanged += (sender, e) =>
            {
                if (dishComboBox.SelectedItem != null)
                {
                    string selectedDish = dishComboBox.SelectedItem.ToString();
                    var selectedRow = menuData.AsEnumerable()
                        .FirstOrDefault(row => row.Field<string>("Item") == selectedDish);

                    if (selectedRow != null)
                    {
                        ShowProductBJU(selectedRow);
                    }
                }
            };

            // Настройка прокрутки
            this.rightPanel.AutoScroll = true;
            this.rightPanel.HorizontalScroll.Enabled = false;
            this.rightPanel.HorizontalScroll.Visible = false;
            this.rightPanel.VerticalScroll.Enabled = true;
            this.rightPanel.VerticalScroll.Visible = true;
        }



        // Метод для отображения контента в правой панели
        private void DisplayRightPanelContent()
        {
            // Очистка правой панели
            this.rightPanel.Controls.Clear();

            Label headerLabel = new Label();
            headerLabel.Text = "Все Блюда";
            headerLabel.Font = new Font(headerLabel.Font.FontFamily, 16, FontStyle.Bold);
            headerLabel.AutoSize = true;
            headerLabel.Location = new Point(10, 10);
            this.rightPanel.Controls.Add(headerLabel);

            int xOffset = 10;
            int yOffset = 40;
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

        private void ShowProductBJU(DataRow productData)
        {

            this.rightPanel.Controls.Remove(plotView);
            Dish dish = new Dish();

            foreach (DataColumn column in productData.Table.Columns)
            {
             
                // Заполняем свойства объекта Dish
                switch (column.ColumnName.ToLower())
                {
                    case "item":
                        dish.Name = productData[column].ToString();
                        break;
                    case "protein":
                        dish.Protein = Convert.ToDouble(productData[column]);
                        break;
                    case "total fat":
                        dish.Fat = Convert.ToDouble(productData[column]);
                        break;
                    case "carbohydrates":
                        dish.Carbohydrates = Convert.ToDouble(productData[column]);
                        break;
                }
            }

            plotView.Location = new System.Drawing.Point(100, 100);
            plotView.Name = "plotView";
            plotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            plotView.Size = new System.Drawing.Size(500, 350);
            plotView.TabIndex = 3;
            plotView.Text = "plotView";
            plotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            plotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            plotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;

            plotView.Model = GetPlotModel2(dish);

            this.rightPanel.Controls.Add(plotView);

            // Настройка прокрутки
            this.rightPanel.AutoScroll = true;
            this.rightPanel.HorizontalScroll.Enabled = false;
            this.rightPanel.HorizontalScroll.Visible = false;
            this.rightPanel.VerticalScroll.Enabled = true;
            this.rightPanel.VerticalScroll.Visible = true;
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

        private PlotModel GetPlotModel2(Dish dish)
        {
            var model = new PlotModel { Title = $"График БЖУ для {dish.Name}" };

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
            categoryAxis.Labels.Add("Белки");
            categoryAxis.Labels.Add("Жиры");
            categoryAxis.Labels.Add("Углеводы");
            model.Axes.Add(categoryAxis);

            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 350 };
            model.Axes.Add(valueAxis);

            var series1 = new BarSeries { Title = "Суточная норма (мужчины)", FillColor = OxyColor.FromRgb(0, 0, 255) };
            series1.Items.Add(new BarItem { Value = 50 });
            series1.Items.Add(new BarItem { Value = 70 });
            series1.Items.Add(new BarItem { Value = 300 });
            model.Series.Add(series1);

            var series2 = new BarSeries { Title = "Суточная норма (женщины)", FillColor = OxyColor.FromRgb(255, 0, 0) };
            series2.Items.Add(new BarItem { Value = 40 });
            series2.Items.Add(new BarItem { Value = 60 });
            series2.Items.Add(new BarItem { Value = 250 });
            model.Series.Add(series2);

            var series3 = new BarSeries { Title = $"{dish.Name}", FillColor = OxyColor.FromRgb(0, 255, 0) };
            series3.Items.Add(new BarItem { Value = dish.Protein });
            series3.Items.Add(new BarItem { Value = dish.Fat });
            series3.Items.Add(new BarItem { Value = dish.Carbohydrates });
            model.Series.Add(series3);

            model.IsLegendVisible = true;

            var legend = new Legend
            {
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendPlacement = LegendPlacement.Outside,
                LegendBorder = OxyColors.Black
            };

            model.Legends.Add(legend);
          
            return model;
        }

        private void DisplayScatterPlot()
        {
            var model = new PlotModel { Title = "Распределение блюд по порции и калориям" };

            // Создаем ось X (калории)
            var caloriesAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Калории"
            };
            model.Axes.Add(caloriesAxis);

            // Создаем ось Y (углеводы)
            var carbohydratesAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Размер порции"
            };
            model.Axes.Add(carbohydratesAxis);

            // Создаем серию точек (для каждого блюда)
            var scatterSeries = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 5,
                MarkerFill = OxyColors.Blue,
                MarkerStroke = OxyColors.Black,
                MarkerStrokeThickness = 1.5,
                TrackerFormatString = "Блюдо: {Tag}\nКалории: {X}\nРазмер порции: {Y}"
            };

            // Добавляем точки для каждого блюда из DataTable
            foreach (DataRow row in menuData.Rows)
            {
                string servingSize = row["Serving Size"].ToString();
                Console.WriteLine(servingSize);

                // Извлекаем числовое значение из строки размера порции
                int startIndex = servingSize.IndexOf('(') + 1;
                int endIndex = servingSize.IndexOf('g', startIndex);

                string servingSizeGrams;

                if (startIndex > 0 && endIndex > startIndex)
                {
                    servingSizeGrams = servingSize.Substring(startIndex, endIndex - startIndex).Trim();
                }
                else
                {
                    // Если размер порции указан в унциях, конвертируем в граммы
                    startIndex = servingSize.IndexOf('(') + 1;
                    endIndex = servingSize.IndexOf("oz", startIndex);

                    if (startIndex > 0 && endIndex > startIndex)
                    {
                        string servingSizeOz = servingSize.Substring(startIndex, endIndex - startIndex).Trim();
                        double ounces = Convert.ToDouble(servingSizeOz, CultureInfo.InvariantCulture);
                        double grams = ounces * 28.35; // 1 унция = 28.35 грамм
                        servingSizeGrams = grams.ToString();
                    }
                    else
                    {
                        // Если размер порции указан без обозначения грамм или унций
                        servingSizeGrams = "Не указано";
                    }
                }

                double portion;
                if (!double.TryParse(servingSizeGrams, out portion))
                {
                    continue; // Пропускаем текущее блюдо, если не удалось сконвертировать
                }

                double calories;
                if (!double.TryParse(row["Calories"].ToString(), out calories))
                {
                    continue; // Пропускаем текущее блюдо, если не удалось сконвертировать
                }

                string dishName = row["Item"].ToString();

                var point = new ScatterPoint(calories, portion)
                {
                    Tag = dishName
                };

                scatterSeries.Points.Add(point);
            }

            model.Series.Add(scatterSeries);

            // Создаем PlotView для отображения графика
            var plotView = new PlotView
            {
                Dock = DockStyle.Fill,
                Model = model
            };

            // Очистка правой панели
            this.rightPanel.Controls.Clear();

            // Добавляем PlotView на rightPanel
            this.rightPanel.Controls.Add(plotView);
        }

        private void DisplayIronRequirementContent()
        {
            // Очистка правой панели
            this.rightPanel.Controls.Clear();

            // Создание заголовка
            Label titleLabel = new Label();
            titleLabel.Text = "Расчет порций для суточной нормы железа";
            titleLabel.Location = new Point(10, 10);
            titleLabel.Size = new Size(300, 20);
            titleLabel.Font = new Font(titleLabel.Font, FontStyle.Bold);
            this.rightPanel.Controls.Add(titleLabel);

            // Создание метки для комбинированного ввода и списка
            Label searchLabel = new Label();
            searchLabel.Text = "Введите название блюда";
            searchLabel.Location = new Point(10, 40);
            searchLabel.Size = new Size(200, 20);
            this.rightPanel.Controls.Add(searchLabel);

            // Создание ComboBox для ввода и списка блюд
            ComboBox dishComboBox = new ComboBox();
            dishComboBox.Location = new Point(10, 70);
            dishComboBox.Size = new Size(200, 20);
            dishComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            this.rightPanel.Controls.Add(dishComboBox);

            // Создание метки для вывода результата
            Label resultLabel = new Label();
            resultLabel.Location = new Point(10, 100);
            resultLabel.Size = new Size(700, 20);
            this.rightPanel.Controls.Add(resultLabel);

            // Настройка поиска блюд
            dishComboBox.TextChanged += (sender, e) =>
            {
                string query = dishComboBox.Text.ToLower();
                var filteredDishes = menuData.AsEnumerable()
                    .Where(row => row.Field<string>("Item").ToLower().Contains(query))
                    .Select(row => row.Field<string>("Item"))
                    .ToList();

                dishComboBox.Items.Clear();
                dishComboBox.Items.AddRange(filteredDishes.ToArray());
                dishComboBox.DroppedDown = true; // Открыть выпадающий список
                dishComboBox.SelectionStart = query.Length; // Установить курсор в конец текста
                dishComboBox.SelectionLength = 0; // Убрать выделение
            };

            // Настройка действия при выборе блюда из списка
            dishComboBox.SelectedIndexChanged += (sender, e) =>
            {
                if (dishComboBox.SelectedItem != null)
                {
                    string selectedDish = dishComboBox.SelectedItem.ToString();
                    var selectedRow = menuData.AsEnumerable()
                        .FirstOrDefault(row => row.Field<string>("Item") == selectedDish);

                    if (selectedRow != null)
                    {
                        double ironContent;
                        if (double.TryParse(selectedRow["Iron (% Daily Value)"].ToString(), out ironContent) && ironContent > 0)
                        {
                     
                            double portionsRequired = 100 / ironContent;
                            int wholePortionsRequired = (int)Math.Ceiling(portionsRequired);
                            resultLabel.Text = $"Для покрытия суточной нормы железа требуется {wholePortionsRequired} порций блюда '{selectedDish}'.";
                        }
                        else
                        {
                            resultLabel.Text = $"Информация о содержании железа в блюде '{selectedDish}' недоступна.";
                        }
                    }
                }
            };

            // Настройка прокрутки
            this.rightPanel.AutoScroll = true;
            this.rightPanel.HorizontalScroll.Enabled = false;
            this.rightPanel.HorizontalScroll.Visible = false;
            this.rightPanel.VerticalScroll.Enabled = true;
            this.rightPanel.VerticalScroll.Visible = true;
        }

        private void DisplayZeroCalorieDishes()
        {
            //Очистка правой панели
            this.rightPanel.Controls.Clear();

            // Создание заголовка
            Label headerLabel = new Label();
            headerLabel.Text = "Блюда 0 ккал";
            headerLabel.Font = new Font(headerLabel.Font.FontFamily, 16, FontStyle.Bold);
            headerLabel.AutoSize = true;
            headerLabel.Location = new Point(10, 10);
            this.rightPanel.Controls.Add(headerLabel);

            int xOffset = 10;
            int yOffset = 40;
            int itemsPerRow = 3;
            int currentItem = 0;


            Image commonImage = Properties.Resources.Water_Icon;

            //Создание панелей для каждого продукта с 0 калориями
            foreach (DataRow row in menuData.Rows)
            {
                //Проверяем значение калорий для текущего продукта
                double calories;
                if (double.TryParse(row["Calories"].ToString(), out calories) && calories == 0)
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

                    //Добавление изображения продукта
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Size = new Size(100, 100);
                    pictureBox.Location = new Point(10, 40);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Image = commonImage;
                    squarePanel.Controls.Add(pictureBox);

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
                        xOffset = 10;
                        yOffset += 210;
                    }
                    else
                    {
                        xOffset += 210;
                    }
                }
            }
            this.rightPanel.AutoScroll = true;
            this.rightPanel.HorizontalScroll.Enabled = false;
            this.rightPanel.HorizontalScroll.Visible = false;
            this.rightPanel.VerticalScroll.Enabled = true;
            this.rightPanel.VerticalScroll.Visible = true;
        }

     
    }

}
public class Dish
{
    public string Name { get; set; }
    public double Protein { get; set; }
    public double Fat { get; set; }
    public double Carbohydrates { get; set; }
}
