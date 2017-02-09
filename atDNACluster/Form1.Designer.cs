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
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openGedmatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFTDNAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveKitsOfMatchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standartizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.типОбработкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SumOfSegmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LongestSegmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clusterizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numberOfClustersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.colorHighlightningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.eNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.processingToolStripMenuItem,
            this.clusterizationToolStripMenuItem,
            this.colorHighlightningToolStripMenuItem,
            this.eNGToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openGedmatchToolStripMenuItem,
            this.openFTDNAToolStripMenuItem});
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.openToolStripMenuItem1.Text = "Загрузить";
            // 
            // openGedmatchToolStripMenuItem
            // 
            this.openGedmatchToolStripMenuItem.Name = "openGedmatchToolStripMenuItem";
            this.openGedmatchToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.openGedmatchToolStripMenuItem.Text = "GedMatch";
            this.openGedmatchToolStripMenuItem.Click += new System.EventHandler(this.openGedmatchToolStripMenuItem_Click);
            // 
            // openFTDNAToolStripMenuItem
            // 
            this.openFTDNAToolStripMenuItem.Name = "openFTDNAToolStripMenuItem";
            this.openFTDNAToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.openFTDNAToolStripMenuItem.Text = "FTDNA";
            this.openFTDNAToolStripMenuItem.Click += new System.EventHandler(this.openFTDNAToolStripMenuItem_Click_1);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveKitsOfMatchesToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.saveToolStripMenuItem.Text = "Сохранить";
            // 
            // saveKitsOfMatchesToolStripMenuItem
            // 
            this.saveKitsOfMatchesToolStripMenuItem.Name = "saveKitsOfMatchesToolStripMenuItem";
            this.saveKitsOfMatchesToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.saveKitsOfMatchesToolStripMenuItem.Text = "Номера совпаденцев";
            this.saveKitsOfMatchesToolStripMenuItem.Click += new System.EventHandler(this.saveKitsOfMatchesToolStripMenuItem_Click);
            // 
            // processingToolStripMenuItem
            // 
            this.processingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.outputTypeToolStripMenuItem,
            this.типОбработкиToolStripMenuItem,
            this.processToolStripMenuItem});
            this.processingToolStripMenuItem.Name = "processingToolStripMenuItem";
            this.processingToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.processingToolStripMenuItem.Text = "Обработка";
            // 
            // outputTypeToolStripMenuItem
            // 
            this.outputTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.centerToolStripMenuItem,
            this.standartizeToolStripMenuItem});
            this.outputTypeToolStripMenuItem.Name = "outputTypeToolStripMenuItem";
            this.outputTypeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.outputTypeToolStripMenuItem.Text = "Тип вывода";
            // 
            // centerToolStripMenuItem
            // 
            this.centerToolStripMenuItem.Checked = true;
            this.centerToolStripMenuItem.CheckOnClick = true;
            this.centerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.centerToolStripMenuItem.Name = "centerToolStripMenuItem";
            this.centerToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.centerToolStripMenuItem.Text = "Центровка";
            this.centerToolStripMenuItem.Click += new System.EventHandler(this.centerToolStripMenuItem_Click);
            // 
            // standartizeToolStripMenuItem
            // 
            this.standartizeToolStripMenuItem.CheckOnClick = true;
            this.standartizeToolStripMenuItem.Name = "standartizeToolStripMenuItem";
            this.standartizeToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.standartizeToolStripMenuItem.Text = "Стандартизация";
            this.standartizeToolStripMenuItem.Click += new System.EventHandler(this.standartizeToolStripMenuItem_Click);
            // 
            // типОбработкиToolStripMenuItem
            // 
            this.типОбработкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SumOfSegmentsToolStripMenuItem,
            this.LongestSegmentToolStripMenuItem});
            this.типОбработкиToolStripMenuItem.Name = "типОбработкиToolStripMenuItem";
            this.типОбработкиToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.типОбработкиToolStripMenuItem.Text = "Тип обработки";
            // 
            // SumOfSegmentsToolStripMenuItem
            // 
            this.SumOfSegmentsToolStripMenuItem.Checked = true;
            this.SumOfSegmentsToolStripMenuItem.CheckOnClick = true;
            this.SumOfSegmentsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SumOfSegmentsToolStripMenuItem.Name = "SumOfSegmentsToolStripMenuItem";
            this.SumOfSegmentsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.SumOfSegmentsToolStripMenuItem.Text = "Сумма участков";
            this.SumOfSegmentsToolStripMenuItem.Click += new System.EventHandler(this.SumOfSegmentsToolStripMenuItem_Click);
            // 
            // LongestSegmentToolStripMenuItem
            // 
            this.LongestSegmentToolStripMenuItem.CheckOnClick = true;
            this.LongestSegmentToolStripMenuItem.Name = "LongestSegmentToolStripMenuItem";
            this.LongestSegmentToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.LongestSegmentToolStripMenuItem.Text = "Наибольший участок";
            this.LongestSegmentToolStripMenuItem.Click += new System.EventHandler(this.LongestSegmentToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.processToolStripMenuItem.Text = "Обработать (МГК)";
            this.processToolStripMenuItem.Click += new System.EventHandler(this.processToolStripMenuItem_Click);
            // 
            // clusterizationToolStripMenuItem
            // 
            this.clusterizationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.numberOfClustersToolStripMenuItem,
            this.processToolStripMenuItem1});
            this.clusterizationToolStripMenuItem.Name = "clusterizationToolStripMenuItem";
            this.clusterizationToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.clusterizationToolStripMenuItem.Text = "Кластеризация";
            // 
            // numberOfClustersToolStripMenuItem
            // 
            this.numberOfClustersToolStripMenuItem.Name = "numberOfClustersToolStripMenuItem";
            this.numberOfClustersToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.numberOfClustersToolStripMenuItem.Text = "Количество кластеров";
            this.numberOfClustersToolStripMenuItem.Click += new System.EventHandler(this.numberOfClustersToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem1
            // 
            this.processToolStripMenuItem1.Name = "processToolStripMenuItem1";
            this.processToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.processToolStripMenuItem1.Text = "Обработать (К-средних)";
            this.processToolStripMenuItem1.Click += new System.EventHandler(this.processToolStripMenuItem1_Click);
            // 
            // colorHighlightningToolStripMenuItem
            // 
            this.colorHighlightningToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.redToolStripMenuItem,
            this.greenToolStripMenuItem,
            this.blackToolStripMenuItem,
            this.processToolStripMenuItem2});
            this.colorHighlightningToolStripMenuItem.Name = "colorHighlightningToolStripMenuItem";
            this.colorHighlightningToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
            this.colorHighlightningToolStripMenuItem.Text = "Выделение цветом";
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.redToolStripMenuItem.Text = "1 - Красный";
            this.redToolStripMenuItem.Click += new System.EventHandler(this.redToolStripMenuItem_Click);
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.greenToolStripMenuItem.Text = "2 - Зеленый";
            this.greenToolStripMenuItem.Click += new System.EventHandler(this.greenToolStripMenuItem_Click);
            // 
            // blackToolStripMenuItem
            // 
            this.blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            this.blackToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.blackToolStripMenuItem.Text = "3 - Черный";
            this.blackToolStripMenuItem.Click += new System.EventHandler(this.blackToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem2
            // 
            this.processToolStripMenuItem2.Name = "processToolStripMenuItem2";
            this.processToolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
            this.processToolStripMenuItem2.Text = "Обработать";
            this.processToolStripMenuItem2.Click += new System.EventHandler(this.processToolStripMenuItem2_Click);
            // 
            // eNGToolStripMenuItem
            // 
            this.eNGToolStripMenuItem.Name = "eNGToolStripMenuItem";
            this.eNGToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.eNGToolStripMenuItem.Text = "ENG";
            this.eNGToolStripMenuItem.Click += new System.EventHandler(this.eNGToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1264, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(127, 17);
            this.toolStripStatusLabel1.Text = "Число совпаденцев: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 661);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "atDNA Clusterer v0.1.9";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem standartizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clusterizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem colorHighlightningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem eNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem numberOfClustersToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveKitsOfMatchesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openGedmatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFTDNAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem типОбработкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SumOfSegmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LongestSegmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

