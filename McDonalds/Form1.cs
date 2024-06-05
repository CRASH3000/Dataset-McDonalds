﻿using System;
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

        private void linkLabelHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string message = "=== Добро пожаловать в Dataset-McDonalds! ===\n" +
                             "Это приложение для работы с файлом данных \"dataset – menu McDonalds.csv\".\n" +
                             "Этот набор данных предоставляет анализ питания для каждого пункта меню McDonald's в США, включая:\n" +
                             "- Завтраки\n" +
                             "- Бургеры из говядины\n" +
                             "- Сэндвичи с курицей и рыбой\n" +
                             "- Картофель фри\n" +
                             "- Салаты\n" +
                             "- Газированные напитки\n" +
                             "- Кофе и чай\n" +
                             "- Молочные коктейли и десерты\n" +
                             "\n" +
                             "Для начала работы, пожалуйста, нажмите кнопку \"Начать\" на главном экране.\n" +
                             "\n" +
                             "После нажатия на кнопку \"Начать\" вы попадете на главный экран приложения, где увидите основной блок с проекциями и кнопки с категориями.\n" +
                             "\n" +
                             "Кнопка 1: \"Все блюда\" - отображает названия всех продуктов из файла с кнопкой \"Подробнее\" для просмотра полной информации.\n" +
                             "\n" +
                             "Кнопка 2: \"Содержание БЖУ блюд\" - открывает окно сравнения суточной нормы БЖУ и БЖУ выбранного блюда. Введите название блюда и выберите его из списка.\n" +
                             "\n" +
                             "Кнопка 3: \"Распределение блюд по порции и калориям\" - отображает график распределения блюд по порции и калориям.\n" +
                             "\n" +
                             "Кнопка 4: \"Блюда 0 ккал\" - отображает названия продуктов с нулевым содержанием калорий с кнопкой \"Подробнее\" для просмотра полной информации.\n" +
                             "\n" +
                             "Кнопка 5: \"Железо в порциях\" - открывает окно расчета порций для суточной нормы железа. Введите название продукта и подтвердите выбор.\n" +
                             "\n" +
                             "Кнопка 6: \"Сахар и Каллории\" - открывает таблицу \"Вывод блюд по указанному содержанию компонента и калорийности\". Выберите компонент, укажите минимальное и максимальное значение компонента, а также минимальное и максимальное значение калорий. Нажмите кнопку \"Обновить таблицу\" для отображения результатов.\n" +
                             "\n" +
                             "Кнопка 7: \"Блюда с транс-жирами\" - открывает таблицу \"Вывод всех блюд с содержанием выбранного компонента\". Выберите интересующий элемент и нажмите кнопку \"Показать список\" для отображения результатов в таблице.\n" +
                             "\n" +
                             "Приятного использования Dataset-McDonalds!";
            string caption = "Помощь";
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
