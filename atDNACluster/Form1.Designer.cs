namespace atDNACluster
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.кластерыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.количествоКластеровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обработкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.типВыводаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.центровкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.стандартизацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обработатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.кластеризацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обработатьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.выделениеЦветомToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.красныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зеленыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.черныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обработатьToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.eNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.кластерыToolStripMenuItem,
            this.обработкаToolStripMenuItem,
            this.кластеризацияToolStripMenuItem,
            this.выделениеЦветомToolStripMenuItem,
            this.eNGToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // кластерыToolStripMenuItem
            // 
            this.кластерыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.количествоКластеровToolStripMenuItem});
            this.кластерыToolStripMenuItem.Name = "кластерыToolStripMenuItem";
            this.кластерыToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.кластерыToolStripMenuItem.Text = "Кластеры";
            // 
            // количествоКластеровToolStripMenuItem
            // 
            this.количествоКластеровToolStripMenuItem.Name = "количествоКластеровToolStripMenuItem";
            this.количествоКластеровToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.количествоКластеровToolStripMenuItem.Text = "Количество кластеров";
            // 
            // обработкаToolStripMenuItem
            // 
            this.обработкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.типВыводаToolStripMenuItem,
            this.обработатьToolStripMenuItem});
            this.обработкаToolStripMenuItem.Name = "обработкаToolStripMenuItem";
            this.обработкаToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.обработкаToolStripMenuItem.Text = "Обработка МГК";
            // 
            // типВыводаToolStripMenuItem
            // 
            this.типВыводаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.центровкаToolStripMenuItem,
            this.стандартизацияToolStripMenuItem});
            this.типВыводаToolStripMenuItem.Name = "типВыводаToolStripMenuItem";
            this.типВыводаToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.типВыводаToolStripMenuItem.Text = "Тип вывода";
            // 
            // центровкаToolStripMenuItem
            // 
            this.центровкаToolStripMenuItem.CheckOnClick = true;
            this.центровкаToolStripMenuItem.Name = "центровкаToolStripMenuItem";
            this.центровкаToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.центровкаToolStripMenuItem.Text = "Центровка";
            this.центровкаToolStripMenuItem.Click += new System.EventHandler(this.центровкаToolStripMenuItem_Click);
            // 
            // стандартизацияToolStripMenuItem
            // 
            this.стандартизацияToolStripMenuItem.Checked = true;
            this.стандартизацияToolStripMenuItem.CheckOnClick = true;
            this.стандартизацияToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.стандартизацияToolStripMenuItem.Name = "стандартизацияToolStripMenuItem";
            this.стандартизацияToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.стандартизацияToolStripMenuItem.Text = "Стандартизация";
            this.стандартизацияToolStripMenuItem.Click += new System.EventHandler(this.стандартизацияToolStripMenuItem_Click);
            // 
            // обработатьToolStripMenuItem
            // 
            this.обработатьToolStripMenuItem.Name = "обработатьToolStripMenuItem";
            this.обработатьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.обработатьToolStripMenuItem.Text = "Обработать";
            this.обработатьToolStripMenuItem.Click += new System.EventHandler(this.обработатьToolStripMenuItem_Click);
            // 
            // кластеризацияToolStripMenuItem
            // 
            this.кластеризацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обработатьToolStripMenuItem1});
            this.кластеризацияToolStripMenuItem.Name = "кластеризацияToolStripMenuItem";
            this.кластеризацияToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.кластеризацияToolStripMenuItem.Text = "Кластеризация";
            // 
            // обработатьToolStripMenuItem1
            // 
            this.обработатьToolStripMenuItem1.Name = "обработатьToolStripMenuItem1";
            this.обработатьToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.обработатьToolStripMenuItem1.Text = "Обработать";
            this.обработатьToolStripMenuItem1.Click += new System.EventHandler(this.обработатьToolStripMenuItem1_Click);
            // 
            // выделениеЦветомToolStripMenuItem
            // 
            this.выделениеЦветомToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.красныйToolStripMenuItem,
            this.зеленыйToolStripMenuItem,
            this.черныйToolStripMenuItem,
            this.обработатьToolStripMenuItem2});
            this.выделениеЦветомToolStripMenuItem.Name = "выделениеЦветомToolStripMenuItem";
            this.выделениеЦветомToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
            this.выделениеЦветомToolStripMenuItem.Text = "Выделение цветом";
            // 
            // красныйToolStripMenuItem
            // 
            this.красныйToolStripMenuItem.Name = "красныйToolStripMenuItem";
            this.красныйToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.красныйToolStripMenuItem.Text = "1 - Красный";
            this.красныйToolStripMenuItem.Click += new System.EventHandler(this.красныйToolStripMenuItem_Click);
            // 
            // зеленыйToolStripMenuItem
            // 
            this.зеленыйToolStripMenuItem.Name = "зеленыйToolStripMenuItem";
            this.зеленыйToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.зеленыйToolStripMenuItem.Text = "2 - Зеленый";
            this.зеленыйToolStripMenuItem.Click += new System.EventHandler(this.зеленыйToolStripMenuItem_Click);
            // 
            // черныйToolStripMenuItem
            // 
            this.черныйToolStripMenuItem.Name = "черныйToolStripMenuItem";
            this.черныйToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.черныйToolStripMenuItem.Text = "3 - Черный";
            this.черныйToolStripMenuItem.Click += new System.EventHandler(this.черныйToolStripMenuItem_Click);
            // 
            // обработатьToolStripMenuItem2
            // 
            this.обработатьToolStripMenuItem2.Name = "обработатьToolStripMenuItem2";
            this.обработатьToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.обработатьToolStripMenuItem2.Text = "Обработать";
            this.обработатьToolStripMenuItem2.Click += new System.EventHandler(this.обработатьToolStripMenuItem2_Click);
            // 
            // eNGToolStripMenuItem
            // 
            this.eNGToolStripMenuItem.Name = "eNGToolStripMenuItem";
            this.eNGToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.eNGToolStripMenuItem.Text = "ENG";
            this.eNGToolStripMenuItem.Click += new System.EventHandler(this.eNGToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 661);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "atDNA Clusterer 0.0.5";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem кластерыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem количествоКластеровToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обработкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem типВыводаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem центровкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem стандартизацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обработатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem кластеризацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обработатьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem выделениеЦветомToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem красныйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зеленыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem черныйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обработатьToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem eNGToolStripMenuItem;
    }
}

