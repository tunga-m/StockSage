using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace StockSage
{
    /// <summary>
    ///     Represents a single candlestick in stock data with properties for date, open, high, low, close prices, and volume.
    public class Class_candlestick
    {
        // Properties of the Class_candlestick class

        /// <summary>
        ///    Gets or sets the date of the candlestick.
        public DateTime Date { get; set; }

        /// <summary>
        ///     Gets or sets the opening price of the candlestick.
        public decimal Open { get; set; }

        /// <summary>
        ///     Gets or sets the highest price of the candlestick.
        public decimal High { get; set; }

        /// <summary>
        ///     Gets or sets the lowest price of the candlestick.
        public decimal Low { get; set; }

        /// <summary>
        ///     Gets or sets the closing price of the candlestick.
        public decimal Close { get; set; }

        /// <summary>
        ///     Gets or sets the trading volume of the candlestick.
        public long Volume { get; set; }

        /// <summary>
        ///     Initializes a new instance of the Class_candlestick class with specified values.
        /// <parameters>
        ///     name="date" The date of the candlesticks.
        ///     name="open" The opening price of the candlestick.
        ///     name="high"The highest price of the candlestick.
        ///     name="low" The lowest price of the candlestick.
        ///     name="close" The closing price of the candlestick.
        ///     name="volume" The trading volume of the candlestick.
        public Class_candlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, long volume)
        {
            Date = date; // Assigning the date parameter to the Date property.
            Open = open; // Assigning the open parameter to the Open property.
            High = high; // Assigning the high parameter to the High property.
            Low = low; // Assigning the low parameter to the Low property.
            Close = close; // Assigning the close parameter to the Close property.
            Volume = volume; // Assigning the volume parameter to the Volume property.
        }

        /// <summary>
        ///     Returns a string representation of the candlestick, including all its properties.
        ///     returns A string detailing the candlestick's date, open, high, low, close prices, and volume.
        public override string ToString()
        {
            // Return a formatted string with all properties of the candlestick.
            return $"Date: {Date}, Open: {Open}, High: {High}, Low: {Low}, Close: {Close}, Volume: {Volume}";
        }
    }

    

}
