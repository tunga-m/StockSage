# StockSage
A Stocks Candlestick Analyzer

StockSage is a work-in-progress project designed to analyze candlestick data and provide insights into stock price movements. The application is being developed to help users visually select candlestick waves, compute their beauty scores, and display Fibonacci levels for deeper stock analysis. This project serves as an educational tool for learning C#, Windows Forms, the .NET Framework, and concepts related to stock market analysis.


# Features
Candlestick Wave Selection:
Allows users to drag and select candlestick waves directly on a chart.
Validates the selected wave to ensure it meets predefined criteria.
Fibonacci Level Visualization:

Calculates and overlays Fibonacci levels on the selected wave.
Displays levels dynamically in real-time as the wave is adjusted.
Beauty Score Calculation:

Evaluates individual candlesticks based on their alignment with Fibonacci levels.
Computes the total beauty score for a wave.

Price Beauty Optimization:
Computes the beauty score for adjusted prices (up to 25% below the original low price).
Identifies the price with the highest beauty score and displays it.

Chart Visualization:
Plots beauty scores against adjusted prices for visual representation.


# Upcoming Features
Implement additional candlestick analysis tools (e.g., moving averages, trend lines).
Save and load candlestick data for offline use.
Enhance UI for a more interactive and user-friendly experience.
Incorporate error handling and performance optimizations.


#Learning Objectives
This project is being developed as part of a personal learning journey in:

C# Programming:
Gaining hands-on experience with object-oriented programming concepts.
Understanding advanced topics like event handling, LINQ, and dynamic graphics.

Windows Forms:
Learning to build desktop applications using the .NET Framework.
Managing user interactions, event-driven programming, and charting libraries.

Stock Market Concepts:
Applying technical analysis principles, such as Fibonacci levels and candlestick patterns.
Understanding stock price movements and their visual representation.


# Requirements
Development Environment: Visual Studio 2019 or newer.
Framework: .NET Framework 4.8.

Dependencies:
System.Windows.Forms.DataVisualization.Charting (for charting).
Standard .NET Framework libraries.


# Usage
Load Data:
Open candlestick data from a file to populate the chart.

Select Wave:
Drag your mouse over a section of the chart to select a wave.

Analyze Wave:
View Fibonacci levels and beauty scores for the selected wave.
See the optimal price with the highest beauty score.

Visualize Results:
Check the chart for plotted beauty scores against adjusted prices.


# Project Structure
Forms:
Form_displayData: Main form for interacting with and visualizing candlestick data.

Classes:
Class_candlestick: Represents a single candlestick with OHLC (Open, High, Low, Close) data.
Class_smartCandlestick (Planned): Advanced candlestick features for detailed analysis.

Methods:
Wave validation, beauty calculation, Fibonacci level computation, and chart plotting.


# Notes
This project is still in progress. Features may change as I continue to learn and implement new concepts.
Constructive feedback and suggestions are always welcome! Do not use this program to buy stocks!


# Acknowledgments
I would like to thank my mentors, classmates, and the C# developer community for their guidance and support. This project has been a rewarding way to combine programming skills with an interest in stock market analysis.


# Contact
If you have any questions, suggestions, or want to collaborate, feel free to reach out via GitHub or email.

Thank you for taking the time to explore StockSage! 😊
