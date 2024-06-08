namespace McDonalds
{
    partial class CalculateAverageElementRatio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dish = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Element = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startSearch = new System.Windows.Forms.Button();
            this.comboCategoryBox = new System.Windows.Forms.ComboBox();
            this.comboElementBox = new System.Windows.Forms.ComboBox();
            this.headerTextBox = new System.Windows.Forms.TextBox();
            this.categoryTextBox = new System.Windows.Forms.TextBox();
            this.elementTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Category,
            this.Dish,
            this.Element,
            this.Size});
            this.dataGridView.Location = new System.Drawing.Point(8, 192);
            this.dataGridView.Name = "dataGridView1";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(583, 246);
            this.dataGridView.TabIndex = 0;
            // 
            // Category
            // 
            this.Category.HeaderText = "Категория";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            this.Category.Width = 110;
            // 
            // Dish
            // 
            this.Dish.HeaderText = "Название блюда";
            this.Dish.Name = "Bludo";
            this.Dish.ReadOnly = true;
            this.Dish.Width = 200;
            // 
            // Element
            // 
            this.Element.HeaderText = "Средняя доля";
            this.Element.Name = "ViborElem";
            this.Element.ReadOnly = true;
            this.Element.Width = 130;
            // 
            // Size
            // 
            this.Size.HeaderText = "Размер порции (гр.)";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            this.Size.Width = 120;
            // 
            // startSearch
            // 
            this.startSearch.Location = new System.Drawing.Point(328, 81);
            this.startSearch.Name = "Zapusk";
            this.startSearch.Size = new System.Drawing.Size(146, 86);
            this.startSearch.TabIndex = 1;
            this.startSearch.Text = "Показать список ";
            this.startSearch.UseVisualStyleBackColor = true;
            this.startSearch.Click += new System.EventHandler(this.Start_Search);
            // 
            // comboCategoryBox
            // 
            this.comboCategoryBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCategoryBox.FormattingEnabled = true;
            this.comboCategoryBox.Location = new System.Drawing.Point(8, 112);
            this.comboCategoryBox.Name = "comboCategoryBox";
            this.comboCategoryBox.Size = new System.Drawing.Size(146, 21);
            this.comboCategoryBox.TabIndex = 2;
            // 
            // comboElementBox
            // 
            this.comboElementBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboElementBox.FormattingEnabled = true;
            this.comboElementBox.Location = new System.Drawing.Point(168, 112);
            this.comboElementBox.Name = "comboElementBox";
            this.comboElementBox.Size = new System.Drawing.Size(146, 21);
            this.comboElementBox.TabIndex = 3;
            // 
            // headerTextBox
            // 
            this.headerTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.headerTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerTextBox.Location = new System.Drawing.Point(44, 12);
            this.headerTextBox.Name = "headerTextBox";
            this.headerTextBox.Size = new System.Drawing.Size(539, 29);
            this.headerTextBox.TabIndex = 6;
            this.headerTextBox.Text = "Средняя доля микроэлементов для выбранной категории";
            // 
            // categoryTextBox
            // 
            this.categoryTextBox.Location = new System.Drawing.Point(8, 88);
            this.categoryTextBox.Name = "categoryTextBox";
            this.categoryTextBox.Size = new System.Drawing.Size(146, 20);
            this.categoryTextBox.TabIndex = 4;
            this.categoryTextBox.Text = "Выберите категорию:";
            this.categoryTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // elementTextBox
            // 
            this.elementTextBox.Location = new System.Drawing.Point(168, 88);
            this.elementTextBox.Name = "elementTextBox";
            this.elementTextBox.Size = new System.Drawing.Size(146, 20);
            this.elementTextBox.TabIndex = 5;
            this.elementTextBox.Text = "Выберите элемент:";
            this.elementTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CalculateAverageElementRatio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.headerTextBox);
            this.Controls.Add(this.categoryTextBox);
            this.Controls.Add(this.elementTextBox);
            this.Controls.Add(this.comboCategoryBox);
            this.Controls.Add(this.comboElementBox);
            this.Controls.Add(this.startSearch);
            this.Controls.Add(this.dataGridView);
            this.Name = "CalculateAverageElementRatio";
            this.Text = "CalculateAverageElementRatio";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox headerTextBox;
        private System.Windows.Forms.TextBox elementTextBox;
        private System.Windows.Forms.ComboBox comboElementBox;
        private System.Windows.Forms.Button startSearch;
        private System.Windows.Forms.TextBox categoryTextBox;
        private System.Windows.Forms.ComboBox comboCategoryBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dish;
        private System.Windows.Forms.DataGridViewTextBoxColumn Element;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
    }
}