namespace SearchEngine
{
    partial class MainWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem2 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Search_btn = new System.Windows.Forms.Button();
            this.SearchChoice = new System.Windows.Forms.ComboBox();
            this.ResultTextBox = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateStoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateDatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storeDataFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainQueryChoice = new System.Windows.Forms.ComboBox();
            this.FirstChoice = new System.Windows.Forms.ComboBox();
            this.SecondChoice = new System.Windows.Forms.ComboBox();
            this.ThirdChoice = new System.Windows.Forms.ComboBox();
            this.QueryResultBox = new System.Windows.Forms.ListBox();
            this.Show_Results_Btn = new System.Windows.Forms.Button();
            this.First_Choice_lbl = new System.Windows.Forms.Label();
            this.Second_Choice_lbl = new System.Windows.Forms.Label();
            this.Third_Choice_lbl = new System.Windows.Forms.Label();
            this.Query_type_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.First_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Chart_Loading_Label = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.First_Chart)).BeginInit();
            this.SuspendLayout();
            // 
            // Search_btn
            // 
            this.Search_btn.Location = new System.Drawing.Point(180, 354);
            this.Search_btn.Name = "Search_btn";
            this.Search_btn.Size = new System.Drawing.Size(99, 21);
            this.Search_btn.TabIndex = 1;
            this.Search_btn.Text = "Show All";
            this.Search_btn.UseVisualStyleBackColor = true;
            this.Search_btn.Click += new System.EventHandler(this.Search_btn_Click);
            // 
            // SearchChoice
            // 
            this.SearchChoice.FormattingEnabled = true;
            this.SearchChoice.Location = new System.Drawing.Point(12, 355);
            this.SearchChoice.Name = "SearchChoice";
            this.SearchChoice.Size = new System.Drawing.Size(162, 21);
            this.SearchChoice.TabIndex = 4;
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultTextBox.ItemHeight = 14;
            this.ResultTextBox.Location = new System.Drawing.Point(12, 382);
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.Size = new System.Drawing.Size(835, 242);
            this.ResultTextBox.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.changePathToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(859, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataToolStripMenuItem,
            this.updateStoresToolStripMenuItem,
            this.updateDatesToolStripMenuItem,
            this.updateOrdersToolStripMenuItem,
            this.restartToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // loadDataToolStripMenuItem
            // 
            this.loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.loadDataToolStripMenuItem.Text = "Update Data";
            this.loadDataToolStripMenuItem.Click += new System.EventHandler(this.loadDataToolStripMenuItem_Click);
            // 
            // updateStoresToolStripMenuItem
            // 
            this.updateStoresToolStripMenuItem.Name = "updateStoresToolStripMenuItem";
            this.updateStoresToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.updateStoresToolStripMenuItem.Text = "Update Stores";
            this.updateStoresToolStripMenuItem.Click += new System.EventHandler(this.updateStoresToolStripMenuItem_Click);
            // 
            // updateDatesToolStripMenuItem
            // 
            this.updateDatesToolStripMenuItem.Name = "updateDatesToolStripMenuItem";
            this.updateDatesToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.updateDatesToolStripMenuItem.Text = "Update Dates";
            this.updateDatesToolStripMenuItem.Click += new System.EventHandler(this.updateDatesToolStripMenuItem_Click);
            // 
            // updateOrdersToolStripMenuItem
            // 
            this.updateOrdersToolStripMenuItem.Name = "updateOrdersToolStripMenuItem";
            this.updateOrdersToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.updateOrdersToolStripMenuItem.Text = "Update Orders";
            this.updateOrdersToolStripMenuItem.Click += new System.EventHandler(this.updateOrdersToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // changePathToolStripMenuItem
            // 
            this.changePathToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.storeDataFolderToolStripMenuItem,
            this.ordersToolStripMenuItem});
            this.changePathToolStripMenuItem.Name = "changePathToolStripMenuItem";
            this.changePathToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.changePathToolStripMenuItem.Text = "Change Path";
            // 
            // storeDataFolderToolStripMenuItem
            // 
            this.storeDataFolderToolStripMenuItem.Name = "storeDataFolderToolStripMenuItem";
            this.storeDataFolderToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.storeDataFolderToolStripMenuItem.Text = "Store Codes File";
            this.storeDataFolderToolStripMenuItem.Click += new System.EventHandler(this.storeDataFolderToolStripMenuItem_Click);
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.ordersToolStripMenuItem.Text = "Orders Folder";
            this.ordersToolStripMenuItem.Click += new System.EventHandler(this.ordersToolStripMenuItem_Click);
            // 
            // MainQueryChoice
            // 
            this.MainQueryChoice.FormattingEnabled = true;
            this.MainQueryChoice.Location = new System.Drawing.Point(12, 44);
            this.MainQueryChoice.Name = "MainQueryChoice";
            this.MainQueryChoice.Size = new System.Drawing.Size(328, 21);
            this.MainQueryChoice.TabIndex = 8;
            this.MainQueryChoice.SelectedIndexChanged += new System.EventHandler(this.MainQueryChoice_SelectedIndexChanged);
            // 
            // FirstChoice
            // 
            this.FirstChoice.FormattingEnabled = true;
            this.FirstChoice.Location = new System.Drawing.Point(12, 83);
            this.FirstChoice.Name = "FirstChoice";
            this.FirstChoice.Size = new System.Drawing.Size(274, 21);
            this.FirstChoice.TabIndex = 9;
            this.FirstChoice.Visible = false;
            // 
            // SecondChoice
            // 
            this.SecondChoice.FormattingEnabled = true;
            this.SecondChoice.Location = new System.Drawing.Point(292, 83);
            this.SecondChoice.Name = "SecondChoice";
            this.SecondChoice.Size = new System.Drawing.Size(284, 21);
            this.SecondChoice.TabIndex = 10;
            this.SecondChoice.Visible = false;
            // 
            // ThirdChoice
            // 
            this.ThirdChoice.FormattingEnabled = true;
            this.ThirdChoice.Location = new System.Drawing.Point(582, 83);
            this.ThirdChoice.Name = "ThirdChoice";
            this.ThirdChoice.Size = new System.Drawing.Size(265, 21);
            this.ThirdChoice.TabIndex = 11;
            this.ThirdChoice.Visible = false;
            // 
            // QueryResultBox
            // 
            this.QueryResultBox.FormattingEnabled = true;
            this.QueryResultBox.Location = new System.Drawing.Point(117, 111);
            this.QueryResultBox.Name = "QueryResultBox";
            this.QueryResultBox.Size = new System.Drawing.Size(730, 30);
            this.QueryResultBox.TabIndex = 12;
            // 
            // Show_Results_Btn
            // 
            this.Show_Results_Btn.Location = new System.Drawing.Point(12, 110);
            this.Show_Results_Btn.Name = "Show_Results_Btn";
            this.Show_Results_Btn.Size = new System.Drawing.Size(99, 31);
            this.Show_Results_Btn.TabIndex = 13;
            this.Show_Results_Btn.Text = "Show Results";
            this.Show_Results_Btn.UseVisualStyleBackColor = true;
            this.Show_Results_Btn.Click += new System.EventHandler(this.Show_Results_Btn_Click);
            // 
            // First_Choice_lbl
            // 
            this.First_Choice_lbl.AutoSize = true;
            this.First_Choice_lbl.Location = new System.Drawing.Point(13, 68);
            this.First_Choice_lbl.Name = "First_Choice_lbl";
            this.First_Choice_lbl.Size = new System.Drawing.Size(35, 13);
            this.First_Choice_lbl.TabIndex = 14;
            this.First_Choice_lbl.Text = "label1";
            this.First_Choice_lbl.Visible = false;
            // 
            // Second_Choice_lbl
            // 
            this.Second_Choice_lbl.AutoSize = true;
            this.Second_Choice_lbl.Location = new System.Drawing.Point(289, 68);
            this.Second_Choice_lbl.Name = "Second_Choice_lbl";
            this.Second_Choice_lbl.Size = new System.Drawing.Size(35, 13);
            this.Second_Choice_lbl.TabIndex = 15;
            this.Second_Choice_lbl.Text = "label2";
            this.Second_Choice_lbl.Visible = false;
            // 
            // Third_Choice_lbl
            // 
            this.Third_Choice_lbl.AutoSize = true;
            this.Third_Choice_lbl.Location = new System.Drawing.Point(579, 68);
            this.Third_Choice_lbl.Name = "Third_Choice_lbl";
            this.Third_Choice_lbl.Size = new System.Drawing.Size(35, 13);
            this.Third_Choice_lbl.TabIndex = 16;
            this.Third_Choice_lbl.Text = "label3";
            this.Third_Choice_lbl.Visible = false;
            // 
            // Query_type_lbl
            // 
            this.Query_type_lbl.AutoSize = true;
            this.Query_type_lbl.Location = new System.Drawing.Point(12, 28);
            this.Query_type_lbl.Name = "Query_type_lbl";
            this.Query_type_lbl.Size = new System.Drawing.Size(62, 13);
            this.Query_type_lbl.TabIndex = 17;
            this.Query_type_lbl.Text = "Query Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "All Results";
            // 
            // First_Chart
            // 
            chartArea2.Name = "ChartArea1";
            this.First_Chart.ChartAreas.Add(chartArea2);
            this.First_Chart.Enabled = false;
            this.First_Chart.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            legend2.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.TopRight;
            legend2.CustomItems.Add(legendItem2);
            legend2.Name = "First_Chart_Legend";
            legend2.Title = "Name";
            this.First_Chart.Legends.Add(legend2);
            this.First_Chart.Location = new System.Drawing.Point(16, 147);
            this.First_Chart.Name = "First_Chart";
            this.First_Chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "First_Chart_Legend";
            series2.Name = "Series1";
            this.First_Chart.Series.Add(series2);
            this.First_Chart.Size = new System.Drawing.Size(831, 188);
            this.First_Chart.TabIndex = 19;
            this.First_Chart.Text = "First Chart";
            // 
            // Chart_Loading_Label
            // 
            this.Chart_Loading_Label.AutoSize = true;
            this.Chart_Loading_Label.Location = new System.Drawing.Point(366, 230);
            this.Chart_Loading_Label.Name = "Chart_Loading_Label";
            this.Chart_Loading_Label.Size = new System.Drawing.Size(139, 13);
            this.Chart_Loading_Label.TabIndex = 20;
            this.Chart_Loading_Label.Text = "Your chart is being loaded...";
            this.Chart_Loading_Label.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 636);
            this.Controls.Add(this.Chart_Loading_Label);
            this.Controls.Add(this.First_Chart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Query_type_lbl);
            this.Controls.Add(this.Third_Choice_lbl);
            this.Controls.Add(this.Second_Choice_lbl);
            this.Controls.Add(this.First_Choice_lbl);
            this.Controls.Add(this.Show_Results_Btn);
            this.Controls.Add(this.QueryResultBox);
            this.Controls.Add(this.ThirdChoice);
            this.Controls.Add(this.SecondChoice);
            this.Controls.Add(this.FirstChoice);
            this.Controls.Add(this.MainQueryChoice);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.SearchChoice);
            this.Controls.Add(this.Search_btn);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.First_Chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Search_btn;
        private System.Windows.Forms.ComboBox SearchChoice;
        private System.Windows.Forms.ListBox ResultTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storeDataFolderToolStripMenuItem;
        private System.Windows.Forms.ComboBox MainQueryChoice;
        private System.Windows.Forms.ComboBox FirstChoice;
        private System.Windows.Forms.ComboBox SecondChoice;
        private System.Windows.Forms.ComboBox ThirdChoice;
        private System.Windows.Forms.ToolStripMenuItem updateStoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateDatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateOrdersToolStripMenuItem;
        private System.Windows.Forms.ListBox QueryResultBox;
        private System.Windows.Forms.Button Show_Results_Btn;
        private System.Windows.Forms.Label First_Choice_lbl;
        private System.Windows.Forms.Label Second_Choice_lbl;
        private System.Windows.Forms.Label Third_Choice_lbl;
        private System.Windows.Forms.Label Query_type_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart First_Chart;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.Label Chart_Loading_Label;
    }
}

