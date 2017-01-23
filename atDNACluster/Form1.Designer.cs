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
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьFTDNAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clustersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clustersNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standartizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clusterizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.colorHighlightningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.eNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.clustersToolStripMenuItem,
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
            this.openToolStripMenuItem,
            this.открытьFTDNAToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openToolStripMenuItem.Text = "Открыть (GedMatch)";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // открытьFTDNAToolStripMenuItem
            // 
            this.открытьFTDNAToolStripMenuItem.Name = "открытьFTDNAToolStripMenuItem";
            this.открытьFTDNAToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.открытьFTDNAToolStripMenuItem.Text = "Открыть (FTDNA)";
            this.открытьFTDNAToolStripMenuItem.Click += new System.EventHandler(this.openFTDNAToolStripMenuItem_Click);
            // 
            // clustersToolStripMenuItem
            // 
            this.clustersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clustersNumberToolStripMenuItem});
            this.clustersToolStripMenuItem.Name = "clustersToolStripMenuItem";
            this.clustersToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.clustersToolStripMenuItem.Text = "Кластеры";
            // 
            // clustersNumberToolStripMenuItem
            // 
            this.clustersNumberToolStripMenuItem.Name = "clustersNumberToolStripMenuItem";
            this.clustersNumberToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.clustersNumberToolStripMenuItem.Text = "Количество кластеров";
            this.clustersNumberToolStripMenuItem.Click += new System.EventHandler(this.clustersNumberToolStripMenuItem_Click);
            // 
            // processingToolStripMenuItem
            // 
            this.processingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.outputTypeToolStripMenuItem,
            this.processToolStripMenuItem});
            this.processingToolStripMenuItem.Name = "processingToolStripMenuItem";
            this.processingToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.processingToolStripMenuItem.Text = "Обработка (МГК)";
            // 
            // outputTypeToolStripMenuItem
            // 
            this.outputTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.centerToolStripMenuItem,
            this.standartizeToolStripMenuItem});
            this.outputTypeToolStripMenuItem.Name = "outputTypeToolStripMenuItem";
            this.outputTypeToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.outputTypeToolStripMenuItem.Text = "Тип вывода";
            // 
            // centerToolStripMenuItem
            // 
            this.centerToolStripMenuItem.CheckOnClick = true;
            this.centerToolStripMenuItem.Name = "centerToolStripMenuItem";
            this.centerToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.centerToolStripMenuItem.Text = "Центровка";
            this.centerToolStripMenuItem.Click += new System.EventHandler(this.centerToolStripMenuItem_Click);
            // 
            // standartizeToolStripMenuItem
            // 
            this.standartizeToolStripMenuItem.Checked = true;
            this.standartizeToolStripMenuItem.CheckOnClick = true;
            this.standartizeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.standartizeToolStripMenuItem.Name = "standartizeToolStripMenuItem";
            this.standartizeToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.standartizeToolStripMenuItem.Text = "Стандартизация";
            this.standartizeToolStripMenuItem.Click += new System.EventHandler(this.standartizeToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.processToolStripMenuItem.Text = "Обработать";
            this.processToolStripMenuItem.Click += new System.EventHandler(this.processToolStripMenuItem_Click);
            // 
            // clusterizationToolStripMenuItem
            // 
            this.clusterizationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processToolStripMenuItem1});
            this.clusterizationToolStripMenuItem.Name = "clusterizationToolStripMenuItem";
            this.clusterizationToolStripMenuItem.Size = new System.Drawing.Size(168, 20);
            this.clusterizationToolStripMenuItem.Text = "Кластеризация (К-средних)";
            // 
            // processToolStripMenuItem1
            // 
            this.processToolStripMenuItem1.Name = "processToolStripMenuItem1";
            this.processToolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.processToolStripMenuItem1.Text = "Обработать";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 661);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "atDNA Clusterer v0.1.1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clustersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clustersNumberToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem открытьFTDNAToolStripMenuItem;
    }
}

