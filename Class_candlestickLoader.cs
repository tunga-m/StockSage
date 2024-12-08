using StockSage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

internal class CandlestickLoader
{
    /// <summary>
    ///     Loads candlestick data from a CSV file and returns a list of Class_candlestick objects.
    /// <parameter>
    ///     name="filePath" The path to the CSV file containing the candlestick data.
    /// <return>
    ///     A list of Class_candlestick objects loaded from the file.
    public static List<Class_candlestick> LoadCandlesticksFromCsv(string filePath)
    {
        // Initialize a new list to store the candlestick objects.
        var candlesticks = new List<Class_candlestick>();

        try
        {
            // Open the file for reading.
            using (var reader = new StreamReader(filePath))
            {
                // Optional: Skip the header line if it exists.
                reader.ReadLine();

                string line; // Variable to hold each line read from the CSV.
                             // Read each line until there are no more lines to read.
                while ((line = reader.ReadLine()) != null)
                {
                    // Split the line into an array of values using a comma as the delimiter.
                    var values = line.Split(',');

                    // Parse the date field from the first column.
                    DateTime date = DateTime.Parse(values[0], CultureInfo.InvariantCulture);
                    // Parse the open price from the second column, dividing by 100 and rounding to the nearest whole number.
                    decimal open = Math.Round(decimal.Parse(values[1], CultureInfo.InvariantCulture));
                    // Parse the high price from the third column, dividing by 100 and rounding to the nearest whole number.
                    decimal high = Math.Round(decimal.Parse(values[2], CultureInfo.InvariantCulture));
                    // Parse the low price from the fourth column, dividing by 100 and rounding to the nearest whole number.
                    decimal low = Math.Round(decimal.Parse(values[3], CultureInfo.InvariantCulture));
                    // Parse the close price from the fifth column, dividing by 100 and rounding to the nearest whole number.
                    decimal close = Math.Round(decimal.Parse(values[4], CultureInfo.InvariantCulture));

                    // Parse the volume from the sixth column, rounding to the nearest whole number.
                    long volume = (long)Math.Round(decimal.Parse(values[5], CultureInfo.InvariantCulture));


                    // Create a new Class_candlestick object using the parsed values.
                    var candlestick = new Class_candlestick(date, open, high, low, close, volume);
                    // Add the newly created candlestick to the list.
                    candlesticks.Add(candlestick);
                }
            }
        }
        catch (Exception ex) // Catch any exceptions that occur during file reading or parsing.
        {
            // Print an error message to the console if an exception occurs.
            Console.WriteLine($"Error loading candlesticks: {ex.Message}");
        }

        // Return the list of loaded candlestick objects.
        return candlesticks;
    }
}