using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockSage
{
    public partial class Form_displayData : Form
    {
        private Form_displayData displayForm; // Field to store the display form reference
        private List<Class_candlestick> allCandlesticks; // Store all loaded candlestick data

        private DateTime startDate; // Add this field for start date
        private DateTime endDate;   // Add this field for end date

        // Constructor that takes a filename parameter
        public Form_displayData(string fileName)
        {
            InitializeComponent();
            // Set the form's title to the chosen file name as well as pass the start and end dates
            this.Text = fileName;
            chart_dataDisplay.MouseDown += new MouseEventHandler(chart_dataDisplay_MouseDown);
            chart_dataDisplay.MouseMove += new MouseEventHandler(chart_dataDisplay_MouseMove);
            chart_dataDisplay.MouseUp += new MouseEventHandler(chart_dataDisplay_MouseUp);
            chart_dataDisplay.Paint += new PaintEventHandler(chart_dataDisplay_Paint);

            // Load and display data based on the start and end dates from DateTimePickers
            DateTime startDate = dateTimePicker_startDate.Value.Date;
            DateTime endDate = dateTimePicker_endDate.Value.Date;

            // Load and display data based on the provided date range
            UpdateDisplayedCandlesticks(startDate, endDate);

            // Populate the combo box with tolerance values
            comboBox_fibLeeway.Items.AddRange(new object[]
            {
                0.0025m, // 0.25%
                0.005m,  // 0.5%
                0.0075m, // 0.75%
                0.01m,   // 1%
                0.015m,  // 1.5%
                0.02m,   // 2%
                0.025m,  // 2.5%
                0.03m    // 3%
            });

            // Set the default tolerance to 1%
            comboBox_fibLeeway.SelectedIndex = 3;
        }


        //new constructor for opening multiple forms files

        /// <summary>
        ///     Handles the Click event of the Load Stocks button.
        ///     Validates date range and loads candlestick data from selected CSV files.
        /// <parameters> 
        ///     name="sender" The source of the event.
        ///     name="e" The event data.
        private void button_loadStocks_Click(object sender, EventArgs e)
        {
            // Ensure the user has selected valid start and end dates
            if (dateTimePicker_startDate.Value > dateTimePicker_endDate.Value)
            {
                MessageBox.Show("End date must be greater than or equal to start date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime startDate = dateTimePicker_startDate.Value.Date;
            DateTime endDate = dateTimePicker_endDate.Value.Date;

            // Show the OpenFileDialog to select CSV files
            if (openFileDialog_loadStocks.ShowDialog() == DialogResult.OK)
            {
                string[] selectedFiles = openFileDialog_loadStocks.FileNames;

                // Load the first file in the existing form
                if (selectedFiles.Length > 0)
                {
                    string firstFile = selectedFiles[0];
                    allCandlesticks = CandlestickLoader.LoadCandlesticksFromCsv(firstFile).ToList();
                    this.Text = firstFile; // Set the form title to the first file's name
                    UpdateDisplayedCandlesticks(startDate, endDate);
                }

                // Open a new form for each additional file
                for (int i = 1; i < selectedFiles.Length; i++)
                {
                    string additionalFile = selectedFiles[i];
                    Form_displayData newForm = new Form_displayData(additionalFile);
                    newForm.allCandlesticks = CandlestickLoader.LoadCandlesticksFromCsv(additionalFile).ToList();
                    newForm.UpdateDisplayedCandlesticks(startDate, endDate);
                    newForm.Show();
                }
            }
        }

        /// <summary>
        ///     Updates the displayed candlestick data based on the selected date range.
        private void UpdateDisplayedCandlesticks(DateTime startDate, DateTime endDate)
        {
            if (allCandlesticks == null)
            {
                return;
            }

            var filteredCandlesticks = allCandlesticks
                .Where(c => c.Date >= startDate && c.Date <= endDate)
                .ToList();

            DisplayCandlesticks(filteredCandlesticks);
        }

        /// <summary>
        ///     Handles the Click event of the Update Date button.
        ///     Updates the displayed candlestick data based on the new date range.
        /// <parameter>
        ///     name="sender">The source of the event.
        ///     name="e">The event data.
        private void button_updateDate_Click(object sender, EventArgs e)
        {
            // Ensure the user has selected valid start and end dates
            if (dateTimePicker_startDate.Value > dateTimePicker_endDate.Value)
            {
                // Show a warning message if the date range is invalid
                MessageBox.Show("End date must be greater than or equal to start date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if the dates are invalid
            }

            // Retrieve the selected start and end dates
            DateTime startDate = dateTimePicker_startDate.Value.Date;
            DateTime endDate = dateTimePicker_endDate.Value.Date;

            // Update the displayed candlestick data based on the new date range
            UpdateDisplayedCandlesticks(startDate, endDate);
        }

        /// <summary>
        ///     Displays the candlestick data in the DataGridView and charts.
        ///     parameter name=candlesticks A list of Class_candlestick objects representing the stock data.
        public void DisplayCandlesticks(List<Class_candlestick> candlesticks)
        {
            // Clear any existing points in the OHLC series to avoid duplications.
            chart_dataDisplay.Series["Series_OHLC"].Points.Clear();
            // Clear any existing points in the volume series to avoid duplications.
            chart_dataDisplay.Series["Series_volume"].Points.Clear();

            // Populate the OHLC and volume series with candlestick data.
            foreach (var candlestick in candlesticks)
            {
                // Set the BindingSource to the new list of candlesticks
                classcandlestickBindingSource.DataSource = candlesticks;

                // Bind the chart to use the BindingSource, clearing any existing points
                chart_dataDisplay.DataSource = classcandlestickBindingSource;

                // Configure the OHLC series to use properties from Class_candlestick
                chart_dataDisplay.Series["Series_OHLC"].XValueMember = "Date";
                chart_dataDisplay.Series["Series_OHLC"].YValueMembers = "High,Low,Open,Close";

                // Configure the volume series to use the Date for X and Volume for Y
                chart_dataDisplay.Series["Series_volume"].XValueMember = "Date";
                chart_dataDisplay.Series["Series_volume"].YValueMembers = "Volume";
            }

            // Refresh the chart to apply the new data
            chart_dataDisplay.DataBind();

            // Normalize the charts to adjust the axis scaling based on the data.
            Normalize();

            // Mark peaks and valleys on the OHLC chart
            MarkPeaksAndValleys(chart_dataDisplay.Series["Series_OHLC"], chart_dataDisplay.ChartAreas["ChartArea_OHLC"]);
        }

        /// <summary>
        ///     Normalizes the display of OHLC and volume data in the charts.
        private void Normalize()
        {
            // Normalize the OHLC data for the OHLC series and its chart area.
            NormalizeOHLC(chart_dataDisplay.Series["Series_OHLC"], chart_dataDisplay.ChartAreas["ChartArea_OHLC"]);

            // Normalize the volume data for the volume series and its chart area.
            NormalizeVolume(chart_dataDisplay.Series["Series_volume"], chart_dataDisplay.ChartAreas["ChartArea_volume"]);
        }

        /// <summary>
        ///     Normalizes the Y-axis scale of the OHLC series in the specified chart area.
        ///     parameter name="series" The OHLC series to be normalized.
        ///     parameter name="chartArea" The chart area containing the OHLC series.
        private void NormalizeOHLC(Series series, ChartArea chartArea)
        {
            // Check if the series contains any points; if not, exit the method.
            if (series.Points.Count == 0) return;

            // Find the minimum Y value (lowest price) in the series.
            double minY = series.Points.FindMinByValue().YValues[0];
            // Find the maximum Y value (highest price) in the series.
            double maxY = series.Points.FindMaxByValue().YValues[0];

            // Adjust the minimum Y value to be 2% smaller.
            double minAdjusted = minY - (0.02 * minY);
            // Adjust the maximum Y value to be 2% bigger.
            double maxAdjusted = maxY + (0.02 * maxY);

            // Zoom the Y-axis to the adjusted min and max values.
            chartArea.AxisY.ScaleView.Zoom(minAdjusted, maxAdjusted);
        }

        /// <summary>
        ///     Normalizes the Y-axis scale of the volume series in the specified chart area.
        ///     parameter name="series" The volume series to be normalized.
        ///     parameter name="chartArea" The chart area containing the volume series.
        private void NormalizeVolume(Series series, ChartArea chartArea)
        {
            // Check if the series contains any points; if not, exit the method.
            if (series.Points.Count == 0) return;

            // Find the minimum Y value (lowest volume) in the series.
            double minY = series.Points.FindMinByValue().YValues[0];
            // Find the maximum Y value (highest volume) in the series.
            double maxY = series.Points.FindMaxByValue().YValues[0];

            // Adjust the minimum Y value to be 2% smaller.
            double minAdjusted = minY - (0.02 * minY);
            // Adjust the maximum Y value to be 2% bigger.
            double maxAdjusted = maxY + (0.02 * maxY);

            // Zoom the Y-axis to the adjusted min and max values.
            chartArea.AxisY.ScaleView.Zoom(minAdjusted, maxAdjusted);
        }

        /// <summary>
        /// Marks the peaks and valleys on the OHLC chart with green and red markers.
        /// </summary>
        private void MarkPeaksAndValleys(Series ohlcSeries, ChartArea chartArea)
        {
            for (int i = 1; i < ohlcSeries.Points.Count - 1; i++)
            {
                var previous = ohlcSeries.Points[i - 1];
                var current = ohlcSeries.Points[i];
                var next = ohlcSeries.Points[i + 1];

                // Determine if it's a peak or valley
                bool isPeak = current.YValues[0] > previous.YValues[0] && current.YValues[0] > next.YValues[0];
                bool isValley = current.YValues[0] < previous.YValues[0] && current.YValues[0] < next.YValues[0];

                if (isPeak || isValley)
                {
                    // Add debug output for verification
                    Debug.WriteLine($"Marking Peak/Valley at Index {i}: High={current.YValues[0]}, Low={current.YValues[1]}");

                    // Mark the point
                    current.MarkerStyle = MarkerStyle.Cross;
                    current.MarkerColor = isPeak ? Color.Green : Color.Red;
                }
            }

            chart_dataDisplay.Invalidate();
        }

        // Point indicating the start of the user's drag selection.
        private Point _startPoint;

        // Point indicating the end of the user's drag selection.
        private Point _endPoint;

        // Boolean to track whether the user is currently dragging to select a wave.
        private bool _isDragging = false;

        // The candlestick corresponding to the user's starting point of selection.
        private Class_candlestick _startCandlestick;

        // The candlestick corresponding to the user's ending point of selection.
        private Class_candlestick _endCandlestick;


        /// <summary>
        /// Handles the mouse down event to initiate candlestick selection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Mouse event arguments.</param>
        private void chart_dataDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            // Determine the candlestick at the mouse-down point.
            _startCandlestick = GetCandlestickAt(e.Location);

            if (_startCandlestick != null)
            {
                // Start drag selection if a valid candlestick is found.
                _isDragging = true;
                _startPoint = e.Location;
            }
            else
            {
                // Reset selection if no valid candlestick is found.
                _isDragging = false;
                _startPoint = Point.Empty;
                MessageBox.Show("Please start your selection on a valid candlestick.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Handles mouse movement to dynamically update the selection rectangle.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Mouse event arguments.</param>
        private void chart_dataDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Update the end point of the selection rectangle.
                _endPoint = e.Location;

                // Refresh the chart to show the updated selection rectangle.
                chart_dataDisplay.Invalidate();
            }
        }


        /// <summary>
        /// Handles the mouse up event to finalize selection and analyze the selected wave.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Mouse event arguments.</param>
        private void chart_dataDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Finalize the end point of the selection.
                _endPoint = e.Location;

                // Determine the candlestick at the mouse-up point.
                _endCandlestick = GetCandlestickAt(e.Location);

                if (_startCandlestick != null && _endCandlestick != null && IsValidWave(_startCandlestick, _endCandlestick))
                {
                    // Determine the low and high prices of the selected wave.
                    decimal originalLow = Math.Min(_startCandlestick.Low, _endCandlestick.Low);
                    decimal originalHigh = Math.Max(_startCandlestick.High, _endCandlestick.High);

                    // Define Fibonacci ratios and calculate levels for the wave.
                    var fibRatios = new List<decimal> { 0.236m, 0.382m, 0.5m, 0.618m, 1m };
                    var fibLevels = CalculateFibonacciLevels(originalLow, originalHigh);

                    // Draw Fibonacci levels on the chart.
                    chart_dataDisplay.Paint += (s, paintArgs) =>
                    {
                        Rectangle waveBox = new Rectangle(
                            Math.Min(_startPoint.X, _endPoint.X),
                            Math.Min(_startPoint.Y, _endPoint.Y),
                            Math.Abs(_startPoint.X - _endPoint.X),
                            Math.Abs(_startPoint.Y - _endPoint.Y));

                        DrawFibonacciLevels(paintArgs.Graphics, waveBox, originalLow, originalHigh, fibLevels);
                    };
                    chart_dataDisplay.Invalidate();

                    // Get the range of candlesticks within the selected wave.
                    int startIndex = allCandlesticks.IndexOf(_startCandlestick);
                    int endIndex = allCandlesticks.IndexOf(_endCandlestick);
                    var waveCandlesticks = allCandlesticks.GetRange(startIndex, endIndex - startIndex + 1);

                    // Compute beauty scores for adjusted prices.
                    var beautyScores = ComputeBeautyAtPrices(originalLow, originalHigh, waveCandlesticks, fibRatios);

                    // Identify the price with the highest beauty score.
                    var (bestPrice, maxBeauty) = FindMaxBeauty(beautyScores);

                    // Display the optimal price and beauty score to the user.
                    MessageBox.Show($"Best Price: {bestPrice:C}, Beauty Score: {maxBeauty}", "Wave Beauty Optimization", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optionally, plot beauty scores on the chart.
                    PlotBeautyScores(beautyScores);
                }
                else
                {
                    // Notify the user if the wave is invalid.
                    MessageBox.Show("Invalid wave. Please select a valid wave.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // End the dragging process.
                _isDragging = false;
            }
        }


        /// <summary>
        /// Handles the paint event to visually display the selection rectangle on the chart.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Paint event arguments.</param>
        private void chart_dataDisplay_Paint(object sender, PaintEventArgs e)
        {
            if (_isDragging || (_startPoint != Point.Empty && _endPoint != Point.Empty))
            {
                // Define the rectangle based on the start and end points.
                Rectangle rect = new Rectangle(
                    Math.Min(_startPoint.X, _endPoint.X),
                    Math.Min(_startPoint.Y, _endPoint.Y),
                    Math.Abs(_startPoint.X - _endPoint.X),
                    Math.Abs(_startPoint.Y - _endPoint.Y));

                // Determine the rectangle's color based on wave validity.
                bool isValidWave = _startCandlestick != null && _endCandlestick != null &&
                                   IsValidWave(_startCandlestick, _endCandlestick);
                Pen pen = isValidWave ? Pens.Green : Pens.Red;

                // Draw the rectangle on the chart.
                e.Graphics.DrawRectangle(pen, rect);
            }
        }



        /// <summary>
        /// Determines whether the selected wave is valid by checking that no candlestick
        /// within the range exceeds the bounds set by the start and end candlesticks.
        /// </summary>
        /// <param name="start">The starting candlestick of the wave.</param>
        /// <param name="end">The ending candlestick of the wave.</param>
        /// <returns>True if the wave is valid; otherwise, false.</returns>
        private bool IsValidWave(Class_candlestick start, Class_candlestick end)
        {
            // Get indices of the start and end candlesticks
            int startIndex = allCandlesticks.IndexOf(start);
            int endIndex = allCandlesticks.IndexOf(end);

            // Validate indices
            if (startIndex < 0 || endIndex < 0 || startIndex > endIndex)
                return false;

            // Determine bounds for the wave
            decimal upperBound = Math.Max(start.High, end.High);
            decimal lowerBound = Math.Min(start.Low, end.Low);

            // Ensure no candlestick exceeds these bounds
            for (int i = startIndex + 1; i < endIndex; i++)
            {
                if (allCandlesticks[i].High > upperBound || allCandlesticks[i].Low < lowerBound)
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Identifies the candlestick corresponding to a point on the chart.
        /// </summary>
        /// <param name="point">The point on the chart to check.</param>
        /// <returns>The matching candlestick, or null if no match is found.</returns>
        private Class_candlestick GetCandlestickAt(Point point)
        {
            var result = chart_dataDisplay.HitTest(point.X, point.Y);

            if (result.ChartElementType == ChartElementType.DataPoint && result.PointIndex >= 0)
            {
                Debug.WriteLine($"HitTest matched candlestick at index {result.PointIndex}");
                return allCandlesticks[result.PointIndex];
            }

            Debug.WriteLine("HitTest did not match a valid candlestick.");
            return null;
        }


        /// <summary>
        /// Calculates the Fibonacci levels between a low and high price.
        /// </summary>
        /// <param name="low">The lowest price of the range.</param>
        /// <param name="high">The highest price of the range.</param>
        /// <returns>A list of Fibonacci levels and their labels.</returns>
        private List<(decimal Level, string Label)> CalculateFibonacciLevels(decimal low, decimal high)
        {
            var fibRatios = new List<decimal> { 0.236m, 0.382m, 0.5m, 0.618m, 1m };
            var fibLevels = new List<(decimal, string)>();

            foreach (var ratio in fibRatios)
            {
                decimal level = low + (high - low) * ratio;
                fibLevels.Add((level, $"{ratio * 100:0.0}%"));
            }

            return fibLevels;
        }


        /// <summary>
        /// Draws the Fibonacci levels on the chart within a specified rectangle.
        /// </summary>
        /// <param name="graphics">The graphics object to use for drawing.</param>
        /// <param name="waveBox">The rectangle representing the selected wave area.</param>
        /// <param name="low">The lowest price of the range.</param>
        /// <param name="high">The highest price of the range.</param>
        /// <param name="fibLevels">The Fibonacci levels to draw.</param>
        private void DrawFibonacciLevels(Graphics graphics, Rectangle waveBox, decimal low, decimal high, List<(decimal Level, string Label)> fibLevels)
        {
            float pixelHeight = waveBox.Height;
            float pixelLow = waveBox.Bottom;
            float priceRange = (float)(high - low);

            // Validate the price range
            if (priceRange <= 0)
            {
                Debug.WriteLine("Invalid price range for Fibonacci levels.");
                return;
            }

            foreach (var fibLevel in fibLevels)
            {
                // Calculate pixel position for the level
                float levelPosition = pixelLow - ((float)(fibLevel.Level - low) / priceRange) * pixelHeight;

                // Skip levels outside the drawable area
                if (levelPosition < waveBox.Top || levelPosition > waveBox.Bottom)
                {
                    Debug.WriteLine($"Fibonacci level {fibLevel.Label} at {fibLevel.Level} is out of bounds. Skipping.");
                    continue;
                }

                // Draw horizontal line and annotation
                using (Pen pen = new Pen(Color.Blue, 2))
                {
                    graphics.DrawLine(pen, waveBox.Left, levelPosition, waveBox.Right, levelPosition);
                }

                using (var font = new Font("Arial", 10, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.Black))
                {
                    graphics.DrawString(fibLevel.Label, font, brush, waveBox.Left + 5, levelPosition - 10);
                }
            }
        }


        /// <summary>
        /// Calculates the beauty score of a single candlestick based on its alignment with Fibonacci levels.
        /// </summary>
        /// <param name="candlestick">The candlestick to evaluate.</param>
        /// <param name="fibLevels">The Fibonacci levels to check against.</param>
        /// <returns>The beauty score, ranging from 0 to 4.</returns>
        private int CalculateCandlestickBeauty(Class_candlestick candlestick, List<(decimal Level, string Label)> fibLevels)
        {
            const decimal tolerance = 0.01m; // 1% tolerance
            int beauty = 0;

            foreach (var fibLevel in fibLevels)
            {
                decimal lowerBound = fibLevel.Level * (1 - tolerance);
                decimal upperBound = fibLevel.Level * (1 + tolerance);

                if ((candlestick.Open >= lowerBound && candlestick.Open <= upperBound) ||
                    (candlestick.High >= lowerBound && candlestick.High <= upperBound) ||
                    (candlestick.Low >= lowerBound && candlestick.Low <= upperBound) ||
                    (candlestick.Close >= lowerBound && candlestick.Close <= upperBound))
                {
                    beauty++;
                }
            }

            return beauty;
        }



        /// <summary>
        /// Calculates the total beauty score for all candlesticks within a wave.
        /// </summary>
        /// <param name="waveCandlesticks">The candlesticks within the wave.</param>
        /// <param name="fibLevels">The Fibonacci levels to check against.</param>
        /// <returns>The total beauty score for the wave.</returns>
        private int CalculateWaveBeauty(List<Class_candlestick> waveCandlesticks, List<(decimal Level, string Label)> fibLevels)
        {
            int totalBeauty = 0;

            foreach (var candlestick in waveCandlesticks)
            {
                totalBeauty += CalculateCandlestickBeauty(candlestick, fibLevels);
            }

            return totalBeauty;
        }

        /// <summary>
        /// Plots the beauty scores of the candlesticks in the wave on the chart.
        /// </summary>
        /// <param name="waveCandlesticks">The candlesticks within the wave.</param>
        /// <param name="fibLevels">The Fibonacci levels to check against.</param>
        private void PlotWaveBeauty(List<Class_candlestick> waveCandlesticks, List<(decimal Level, string Label)> fibLevels)
        {
            // Get or create the volume series
            var series = chart_dataDisplay.Series["Series_volume"];
            series.Points.Clear();
            series.ChartType = SeriesChartType.Line;

            // Add data points to the series
            foreach (var candlestick in waveCandlesticks)
            {
                int beauty = CalculateCandlestickBeauty(candlestick, fibLevels);
                series.Points.AddXY((double)candlestick.Close, beauty);
            }

            // Configure the chart area
            chart_dataDisplay.ChartAreas["ChartArea_volume"].AxisX.Title = "Price";
            chart_dataDisplay.ChartAreas["ChartArea_volume"].AxisY.Title = "Beauty Score";

            // Refresh the chart
            chart_dataDisplay.Invalidate();
        }

        private List<(decimal Level, string Label)> AdjustFibonacciLevels(decimal newLow, decimal originalHigh, List<decimal> fibRatios)
        {
            var adjustedLevels = new List<(decimal, string)>();

            foreach (var ratio in fibRatios)
            {
                decimal level = newLow + (originalHigh - newLow) * ratio;
                adjustedLevels.Add((level, $"{ratio * 100:0.0}%"));
            }

            return adjustedLevels;
        }


        /// <summary>
        /// Computes the beauty scores for a range of adjusted prices, starting from the original low price
        /// and decrementing by $1 until 25% below the original low price.
        /// </summary>
        /// <param name="originalLow">The original lowest price of the wave.</param>
        /// <param name="originalHigh">The original highest price of the wave.</param>
        /// <param name="waveCandlesticks">The list of candlesticks within the wave.</param>
        /// <param name="fibRatios">The Fibonacci ratios to calculate levels for each adjusted price.</param>
        /// <returns>A dictionary where the key is the adjusted price and the value is the corresponding beauty score.</returns>
        private Dictionary<decimal, int> ComputeBeautyAtPrices(
            decimal originalLow,
            decimal originalHigh,
            List<Class_candlestick> waveCandlesticks,
            List<decimal> fibRatios)
        {
            // Dictionary to store beauty scores for each adjusted price.
            var beautyScores = new Dictionary<decimal, int>();

            // Calculate the stopping point, which is 25% below the original low price.
            decimal stopPrice = originalLow * 0.75m;

            // Iterate over adjusted prices, decrementing by $1 each time.
            for (decimal newLow = originalLow; newLow >= stopPrice; newLow -= 1)
            {
                // Adjust the Fibonacci levels for the current price.
                var adjustedFibLevels = AdjustFibonacciLevels(newLow, originalHigh, fibRatios);

                // Calculate the wave beauty score using the adjusted Fibonacci levels.
                int waveBeauty = CalculateWaveBeauty(waveCandlesticks, adjustedFibLevels);

                // Store the beauty score for the current adjusted price.
                beautyScores[newLow] = waveBeauty;
            }

            // Return the computed beauty scores.
            return beautyScores;
        }



        /// <summary>
        /// Finds the price with the highest beauty score from a dictionary of beauty scores.
        /// </summary>
        /// <param name="beautyScores">A dictionary where keys are prices and values are beauty scores.</param>
        /// <returns>A tuple containing the price with the highest beauty score and the score itself.</returns>
        private (decimal Price, int Beauty) FindMaxBeauty(Dictionary<decimal, int> beautyScores)
        {
            // Find the entry with the highest beauty score.
            var maxBeauty = beautyScores.OrderByDescending(kv => kv.Value).First();

            // Return the price and corresponding beauty score as a tuple.
            return (maxBeauty.Key, maxBeauty.Value);
        }


        /// <summary>
        /// Plots beauty scores on the chart.
        /// </summary>
        private void PlotBeautyScores(Dictionary<decimal, int> beautyScores)
        {
            // set chart series and clear existing data and add a chart of line
            var series = chart_dataDisplay.Series["Series_volume"];
            series.Points.Clear();
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            // plot points on chart
            foreach (var kvp in beautyScores)
            {
                series.Points.AddXY((double)kvp.Key, kvp.Value);
            }

            // display the chart
            chart_dataDisplay.ChartAreas["ChartArea_volume"].AxisX.Title = "Price";
            chart_dataDisplay.ChartAreas["ChartArea_volume"].AxisY.Title = "Beauty Score";
            chart_dataDisplay.Invalidate();
        }

    }
}