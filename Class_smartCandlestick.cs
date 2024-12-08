using System;

namespace StockSage
{
    /// <summary>
    /// Represents an advanced candlestick with additional computed properties such as range, body range, tails, etc.
    /// </summary>
    public class Class_smartCandlestick : Class_candlestick
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Class_smartCandlestick"/> class
        ///     by calling the base constructor with specified values.
        /// <parameters>
        ///     name="date">The date of the candlestick.
        ///     name="open">The opening price of the candlestick.
        ///     name="high">The highest price of the candlestick.
        ///     name="low">The lowest price of the candlestick.
        ///     name="close">The closing price of the candlestick.
        ///     name="volume">The trading volume of the candlestick.
        public Class_smartCandlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, long volume)
            : base(date, open, high, low, close, volume)
        {
        }

        /// <summary>
        /// Gets the range of the candlestick (High - Low).
        /// </summary>
        public decimal Range => High - Low;

        /// <summary>
        /// Gets the body range of the candlestick (absolute difference between Open and Close).
        /// </summary>
        public decimal BodyRange => Math.Abs(Open - Close);

        /// <summary>
        /// Gets the top price, which is the larger value between Open and Close.
        /// </summary>
        public decimal TopPrice => Math.Max(Open, Close);

        /// <summary>
        /// Gets the bottom price, which is the smaller value between Open and Close.
        /// </summary>
        public decimal BottomPrice => Math.Min(Open, Close);

        /// <summary>
        /// Gets the height of the upper tail (High - TopPrice).
        /// </summary>
        public decimal UpperTail => High - TopPrice;

        /// <summary>
        /// Gets the height of the lower tail (BottomPrice - Low).
        /// </summary>
        public decimal LowerTail => BottomPrice - Low;

        /// <summary>
        /// Determines whether the candlestick is bullish (closing price is greater than opening price).
        /// </summary>
        public bool IsBullish => Close > Open;

        /// <summary>
        /// Determines whether the candlestick is bearish (closing price is less than opening price).
        /// </summary>
        public bool IsBearish => Close < Open;

        /// <summary>
        /// Determines whether the candlestick is neutral (close is equal to open).
        /// </summary>
        public bool IsNeutral => Close == Open;

        /// <summary>
        /// Determines whether the candlestick is a Marubozu (no upper and lower tails, long body).
        /// </summary>
        public bool IsMarubozu => UpperTail == 0 && LowerTail == 0 && BodyRange > 0;

        /// <summary>
        /// Determines whether the candlestick is a Hammer (small body with long lower tail).
        /// </summary>
        public bool IsHammer => BodyRange < Range * 0.3m && LowerTail > BodyRange * 2 && UpperTail < BodyRange * 0.1m;

        /// <summary>
        /// Determines whether the candlestick is a Doji (open and close are very close).
        /// </summary>
        public bool IsDoji => BodyRange < Range * 0.1m;

        /// <summary>
        /// Determines whether the candlestick is a Dragonfly Doji (Doji with long lower tail).
        /// </summary>
        public bool IsDragonflyDoji => IsDoji && LowerTail > Range * 0.5m;

        /// <summary>
        /// Determines whether the candlestick is a Gravestone Doji (Doji with long upper tail).
        /// </summary>
        public bool IsGravestoneDoji => IsDoji && UpperTail > Range * 0.5m;

        /// <summary>
        /// Provides a string representation of the candlestick pattern.
        /// </summary>
        public string Pattern
        {
            get
            {
                if (IsBullish) return "Bullish";
                if (IsBearish) return "Bearish";
                if (IsNeutral) return "Neutral";
                if (IsMarubozu) return "Marubozu";
                if (IsHammer) return "Hammer";
                if (IsDoji) return "Doji";
                if (IsDragonflyDoji) return "Dragonfly Doji";
                if (IsGravestoneDoji) return "Gravestone Doji";
                return "Unknown Pattern";
            }
        }
    }
}
