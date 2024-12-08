using System;

namespace StockSage
{
    partial class Form_displayData
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_displayData));
            this.chart_dataDisplay = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dateTimePicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_endDate = new System.Windows.Forms.DateTimePicker();
            this.button_updateDate = new System.Windows.Forms.Button();
            this.button_loadStocks = new System.Windows.Forms.Button();
            this.label_startDate = new System.Windows.Forms.Label();
            this.label_endDate = new System.Windows.Forms.Label();
            this.openFileDialog_loadStocks = new System.Windows.Forms.OpenFileDialog();
            this.comboBox_fibLeeway = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.classcandlestickBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart_dataDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.classcandlestickBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_dataDisplay
            // 
            chartArea1.AlignWithChartArea = "ChartArea_volume";
            chartArea1.Name = "ChartArea_OHLC";
            chartArea2.Name = "ChartArea_volume";
            this.chart_dataDisplay.ChartAreas.Add(chartArea1);
            this.chart_dataDisplay.ChartAreas.Add(chartArea2);
            this.chart_dataDisplay.DataSource = this.classcandlestickBindingSource;
            this.chart_dataDisplay.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart_dataDisplay.Legends.Add(legend1);
            this.chart_dataDisplay.Location = new System.Drawing.Point(0, 0);
            this.chart_dataDisplay.Name = "chart_dataDisplay";
            series1.ChartArea = "ChartArea_OHLC";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Color = System.Drawing.Color.Green;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Green";
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Series_OHLC";
            series1.XValueMember = "Date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueMembers = "Open, High, Low, Close";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea_volume";
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "Series_volume";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int64;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart_dataDisplay.Series.Add(series1);
            this.chart_dataDisplay.Series.Add(series2);
            this.chart_dataDisplay.Size = new System.Drawing.Size(2422, 870);
            this.chart_dataDisplay.TabIndex = 1;
            this.chart_dataDisplay.Text = "chart1";
            this.chart_dataDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart_dataDisplay_MouseDown);
            this.chart_dataDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart_dataDisplay_MouseMove);
            this.chart_dataDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart_dataDisplay_MouseUp);
            // 
            // dateTimePicker_startDate
            // 
            this.dateTimePicker_startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_startDate.Location = new System.Drawing.Point(231, 905);
            this.dateTimePicker_startDate.Name = "dateTimePicker_startDate";
            this.dateTimePicker_startDate.Size = new System.Drawing.Size(545, 44);
            this.dateTimePicker_startDate.TabIndex = 2;
            this.dateTimePicker_startDate.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimePicker_endDate
            // 
            this.dateTimePicker_endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_endDate.Location = new System.Drawing.Point(230, 971);
            this.dateTimePicker_endDate.Name = "dateTimePicker_endDate";
            this.dateTimePicker_endDate.Size = new System.Drawing.Size(546, 44);
            this.dateTimePicker_endDate.TabIndex = 3;
            // 
            // button_updateDate
            // 
            this.button_updateDate.Location = new System.Drawing.Point(1769, 905);
            this.button_updateDate.Name = "button_updateDate";
            this.button_updateDate.Size = new System.Drawing.Size(217, 80);
            this.button_updateDate.TabIndex = 6;
            this.button_updateDate.Text = "Update Date";
            this.button_updateDate.UseVisualStyleBackColor = true;
            this.button_updateDate.Click += new System.EventHandler(this.button_updateDate_Click);
            // 
            // button_loadStocks
            // 
            this.button_loadStocks.Location = new System.Drawing.Point(2161, 905);
            this.button_loadStocks.Name = "button_loadStocks";
            this.button_loadStocks.Size = new System.Drawing.Size(218, 80);
            this.button_loadStocks.TabIndex = 7;
            this.button_loadStocks.Text = "Load Stock(s)";
            this.button_loadStocks.UseVisualStyleBackColor = true;
            this.button_loadStocks.Click += new System.EventHandler(this.button_loadStocks_Click);
            // 
            // label_startDate
            // 
            this.label_startDate.AutoSize = true;
            this.label_startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_startDate.Location = new System.Drawing.Point(55, 905);
            this.label_startDate.Name = "label_startDate";
            this.label_startDate.Size = new System.Drawing.Size(170, 37);
            this.label_startDate.TabIndex = 8;
            this.label_startDate.Text = "Start Date:";
            // 
            // label_endDate
            // 
            this.label_endDate.AutoSize = true;
            this.label_endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_endDate.Location = new System.Drawing.Point(66, 971);
            this.label_endDate.Name = "label_endDate";
            this.label_endDate.Size = new System.Drawing.Size(159, 37);
            this.label_endDate.TabIndex = 9;
            this.label_endDate.Text = "End Date:";
            // 
            // openFileDialog_loadStocks
            // 
            this.openFileDialog_loadStocks.Filter = "CSV files (*.csv)|*.csv";
            this.openFileDialog_loadStocks.Multiselect = true;
            // 
            // comboBox_fibLeeway
            // 
            this.comboBox_fibLeeway.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_fibLeeway.FormattingEnabled = true;
            this.comboBox_fibLeeway.Items.AddRange(new object[] {
            "3%",
            "2.75%",
            "2.50%",
            "2.25%",
            "2%",
            "1.75%",
            "1.50%",
            "1.25%",
            "1%",
            ".75%",
            ".50%",
            ".25%"});
            this.comboBox_fibLeeway.Location = new System.Drawing.Point(1242, 971);
            this.comboBox_fibLeeway.Name = "comboBox_fibLeeway";
            this.comboBox_fibLeeway.Size = new System.Drawing.Size(121, 45);
            this.comboBox_fibLeeway.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1113, 905);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 37);
            this.label1.TabIndex = 11;
            this.label1.Text = "Fibonacci Level Tolerence";
            // 
            // classcandlestickBindingSource
            // 
            this.classcandlestickBindingSource.DataSource = typeof(StockSage.Class_candlestick);
            // 
            // Form_displayData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2422, 1031);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_fibLeeway);
            this.Controls.Add(this.label_endDate);
            this.Controls.Add(this.label_startDate);
            this.Controls.Add(this.button_loadStocks);
            this.Controls.Add(this.button_updateDate);
            this.Controls.Add(this.dateTimePicker_endDate);
            this.Controls.Add(this.dateTimePicker_startDate);
            this.Controls.Add(this.chart_dataDisplay);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_displayData";
            this.Text = "StockSage";
            ((System.ComponentModel.ISupportInitialize)(this.chart_dataDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.classcandlestickBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource classcandlestickBindingSource;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_dataDisplay;
        private System.Windows.Forms.DateTimePicker dateTimePicker_startDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_endDate;
        private System.Windows.Forms.Button button_updateDate;
        private System.Windows.Forms.Button button_loadStocks;
        private System.Windows.Forms.Label label_startDate;
        private System.Windows.Forms.Label label_endDate;
        private System.Windows.Forms.OpenFileDialog openFileDialog_loadStocks;
        private System.Windows.Forms.ComboBox comboBox_fibLeeway;
        private System.Windows.Forms.Label label1;
    }
    }