using Catel.MVVM;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using Npgsql;
using Catel.Data;
using Books_Store_Management_App.Models;
using Windows.Devices.Geolocation;

namespace Books_Store_Management_App.ViewModels
{

    /// <summary>
    /// Lớp ViewModel cho trang Statistics để hiển thị biểu đồ doanh thu và cảnh báo hàng tồn kho.
    /// </summary>
    public class StatisticsViewModel : ViewModelBase
    {
        // Chart properties
        public ObservableCollection<ISeries> LineChartSeries { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        // Command properties for setting charts
        public ICommand SetDailyChartCommand { get; }
        public ICommand SetMonthlyChartCommand { get; }
        public ICommand SetYearlyChartCommand { get; }

        // Private fields
        private readonly OrderPageViewModel _orderPageViewModel;
        private List<RevenueData> _dailyRevenue; // Daily revenue data

        // Stock alert properties
        public ObservableCollection<StockItem> StockItems { get; set; }
        public bool IsStockAlertVisible { get; set; }
        public ICommand OpenStockAlertCommand { get; }
        public ICommand CloseStockAlertCommand { get; }

        // Constructor to initialize commands and load initial data
        public StatisticsViewModel(OrderPageViewModel orderPageViewModel)
        {
            _orderPageViewModel = orderPageViewModel;

            // Calculate daily revenue based on order data
            _dailyRevenue = CalculateDailyRevenue();

            // Initialize commands for chart changes
            SetDailyChartCommand = new Command(SetDailyChart);
            SetMonthlyChartCommand = new Command(SetMonthlyChart);
            SetYearlyChartCommand = new Command(SetYearlyChart);
            SetDailyChart(); // Default chart set to daily

            // Initialize stock alert data and visibility
            StockItems = new ObservableCollection<StockItem>();
            IsStockAlertVisible = false;

            // Commands to open and close stock alerts
            OpenStockAlertCommand = new RelayCommand(OpenStockAlert);
            CloseStockAlertCommand = new RelayCommand(CloseStockAlert);
        }

        #region Revenue Calculation Methods

        // Method to calculate daily revenue from orders
        private List<RevenueData> CalculateDailyRevenue()
        {
            return _orderPageViewModel.Orders
                .GroupBy(order => DateTime.Parse(order.Date.ToString()).Date) // Group by date
                .Select(group => new RevenueData
                {
                    Date = group.Key, // Store the date
                    TotalRevenue = group.Sum(order => order.Price) // Total revenue for the day
                })
                .ToList();
        }

        // Method to refresh the daily revenue data and update the chart
        public void RefreshDailyRevenue()
        {
            _dailyRevenue = CalculateDailyRevenue(); // Recalculate daily revenue
            SetDailyChart(); // Update the daily chart with new data
        }

        #endregion

        #region Chart Methods

        // Method to set the daily revenue chart
        private void SetDailyChart()
        {
            // Define line chart for daily revenue
            LineChartSeries = new ObservableCollection<ISeries>
            {
                new LineSeries<double>
                {
                    Name = "Doanh thu theo ngày", // Chart title
                    Values = _dailyRevenue.Select(d => (double)d.TotalRevenue).ToArray() // Revenue data
                }
            };

            // Set the X and Y axes labels
            XAxes = new[]
            {
                new Axis
                {
                    Name = "Ngày", // X-axis label (Date)
                    Labels = _dailyRevenue.Select(d => d.Date.ToShortDateString()).ToArray() // Labels for each day
                }
            };

            YAxes = new[]
            {
                new Axis
                {
                    Name = "Doanh thu (VNĐ)" // Y-axis label (Revenue in VND)
                }
            };

            // Notify the view to update the chart
            RaisePropertyChanged(nameof(LineChartSeries));
            RaisePropertyChanged(nameof(XAxes));
            RaisePropertyChanged(nameof(YAxes));
        }

        // Method to set the monthly revenue chart
        private void SetMonthlyChart()
        {
            var monthlyRevenue = _dailyRevenue
                .GroupBy(d => new { d.Date.Year, d.Date.Month }) // Group by year and month
                .Select(g => new { Month = $"{g.Key.Month}/{g.Key.Year}", Total = g.Sum(d => d.TotalRevenue) }) // Sum revenue per month
                .ToList();

            // Define line chart for monthly revenue
            LineChartSeries = new ObservableCollection<ISeries>
            {
                new LineSeries<double>
                {
                    Name = "Doanh thu theo tháng", // Chart title
                    Values = monthlyRevenue.Select(m => (double)m.Total).ToArray() // Revenue data
                }
            };

            // Set the X and Y axes labels for monthly chart
            XAxes = new[]
            {
                new Axis
                {
                    Name = "Tháng", // X-axis label (Month)
                    Labels = monthlyRevenue.Select(m => m.Month).ToArray() // Labels for each month
                }
            };

            // Notify the view to update the chart
            RaisePropertyChanged(nameof(LineChartSeries));
            RaisePropertyChanged(nameof(XAxes));
        }

        // Method to set the yearly revenue chart
        private void SetYearlyChart()
        {
            var yearlyRevenue = _dailyRevenue
                .GroupBy(d => d.Date.Year) // Group by year
                .Select(g => new { Year = g.Key, Total = g.Sum(d => d.TotalRevenue) }) // Sum revenue per year
                .ToList();

            // Define line chart for yearly revenue
            LineChartSeries = new ObservableCollection<ISeries>
            {
                new LineSeries<double>
                {
                    Name = "Doanh thu theo năm", // Chart title
                    Values = yearlyRevenue.Select(y => (double)y.Total).ToArray() // Revenue data
                }
            };

            // Set the X and Y axes labels for yearly chart
            XAxes = new[]
            {
                new Axis
                {
                    Name = "Năm", // X-axis label (Year)
                    Labels = yearlyRevenue.Select(y => y.Year.ToString()).ToArray() // Labels for each year
                }
            };

            // Notify the view to update the chart
            RaisePropertyChanged(nameof(LineChartSeries));
            RaisePropertyChanged(nameof(XAxes));
        }

        #endregion

        #region Stock Alert Methods

        // Method to open the stock alert and load stock data
        private void OpenStockAlert()
        {
            LoadStockData(); // Load data for stock items
            IsStockAlertVisible = true; // Show the stock alert
        }

        // Method to close the stock alert
        private void CloseStockAlert()
        {
            IsStockAlertVisible = false; // Hide the stock alert
        }

        // Method to load stock data from the database
        private void LoadStockData()
        {
            StockItems.Clear(); // Clear any existing stock data

            string connectionString = ConfigurationManager.Instance.GetConnectionString();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT title, quantity FROM public.\"book\" WHERE quantity < 20"; // Query to find low stock items
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) // Read each stock item
                    {
                        StockItems.Add(new StockItem
                        {
                            ProductName = reader.GetString(0), // Product name
                            RemainingQuantity = $"{reader.GetInt32(1)} Packet" // Remaining quantity
                        });
                    }
                }
            }
        }

        #endregion
    }
}
