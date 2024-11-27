using Catel.MVVM;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using Npgsql;

namespace Books_Store_Management_App.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        // Chart properties
        public ObservableCollection<ISeries> LineChartSeries { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public ICommand SetDailyChartCommand { get; }
        public ICommand SetMonthlyChartCommand { get; }
        public ICommand SetYearlyChartCommand { get; }

        private readonly List<RevenueData> _dailyRevenue;

        // Stock alert properties
        public ObservableCollection<StockItem> StockItems { get; set; }
        public bool IsStockAlertVisible { get; set; }
        public ICommand OpenStockAlertCommand { get; }
        public ICommand CloseStockAlertCommand { get; }

        public StatisticsViewModel()
        {
            // Chart data
            _dailyRevenue = new List<RevenueData>
            {
                new RevenueData { Date = new DateTime(2024, 11, 20), TotalRevenue = 1000 },
                new RevenueData { Date = new DateTime(2024, 11, 21), TotalRevenue = 2000 },
                new RevenueData { Date = new DateTime(2024, 11, 22), TotalRevenue = 1500 },
                new RevenueData { Date = new DateTime(2024, 12, 01), TotalRevenue = 2500 },
                new RevenueData { Date = new DateTime(2024, 12, 02), TotalRevenue = 3000 }
            };

            // Commands for chart
            SetDailyChartCommand = new Command(SetDailyChart);
            SetMonthlyChartCommand = new Command(SetMonthlyChart);
            SetYearlyChartCommand = new Command(SetYearlyChart);
            SetDailyChart(); // Default mode

            // Stock alert data
            StockItems = new ObservableCollection<StockItem>();
            IsStockAlertVisible = false;

            OpenStockAlertCommand = new RelayCommand(OpenStockAlert);
            CloseStockAlertCommand = new RelayCommand(CloseStockAlert);

        }

        // Chart methods
        private void SetDailyChart()
        {
            LineChartSeries = new ObservableCollection<ISeries>
            {
                new LineSeries<double>
                {
                    Name = "Doanh thu theo ngày",
                    Values = _dailyRevenue.Select(d => (double)d.TotalRevenue).ToArray()
                }
            };

            XAxes = new[]
            {
                new Axis
                {
                    Name = "Ngày",
                    Labels = _dailyRevenue.Select(d => d.Date.ToShortDateString()).ToArray()
                }
            };

            YAxes = new[]
            {
                new Axis
                {
                    Name = "Doanh thu (VNĐ)"
                }
            };

            RaisePropertyChanged(nameof(LineChartSeries));
            RaisePropertyChanged(nameof(XAxes));
            RaisePropertyChanged(nameof(YAxes));
        }

        private void SetMonthlyChart()
        {
            var monthlyRevenue = _dailyRevenue
                .GroupBy(d => new { d.Date.Year, d.Date.Month })
                .Select(g => new { Month = $"{g.Key.Month}/{g.Key.Year}", Total = g.Sum(d => d.TotalRevenue) })
                .ToList();

            LineChartSeries = new ObservableCollection<ISeries>
            {
                new LineSeries<double>
                {
                    Name = "Doanh thu theo tháng",
                    Values = monthlyRevenue.Select(m => (double)m.Total).ToArray()
                }
            };

            XAxes = new[]
            {
                new Axis
                {
                    Name = "Tháng",
                    Labels = monthlyRevenue.Select(m => m.Month).ToArray()
                }
            };

            RaisePropertyChanged(nameof(LineChartSeries));
            RaisePropertyChanged(nameof(XAxes));
        }

        private void SetYearlyChart()
        {
            var yearlyRevenue = _dailyRevenue
                .GroupBy(d => d.Date.Year)
                .Select(g => new { Year = g.Key, Total = g.Sum(d => d.TotalRevenue) })
                .ToList();

            LineChartSeries = new ObservableCollection<ISeries>
            {
                new LineSeries<double>
                {
                    Name = "Doanh thu theo năm",
                    Values = yearlyRevenue.Select(y => (double)y.Total).ToArray()
                }
            };

            XAxes = new[]
            {
                new Axis
                {
                    Name = "Năm",
                    Labels = yearlyRevenue.Select(y => y.Year.ToString()).ToArray()
                }
            };

            RaisePropertyChanged(nameof(LineChartSeries));
            RaisePropertyChanged(nameof(XAxes));
        }

        // Stock alert methods
        private void OpenStockAlert()
        {
            LoadStockData();
            IsStockAlertVisible = true;
        }

        private void CloseStockAlert()
        {
            IsStockAlertVisible = false;
        }

        private void LoadStockData()
        {
            StockItems.Clear();

            string connectionString = "Host=localhost;Username=postgres;Password=admin;Database=mybookstore";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT title, quantity FROM public.\"book\" WHERE quantity < 20";
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StockItems.Add(new StockItem
                        {
                            ProductName = reader.GetString(0),
                            RemainingQuantity = $"{reader.GetInt32(1)} Packet"
                        });
                    }
                }
            }
        }
    }

    public class StockItem
    {
        public string ProductName { get; set; }
        public string RemainingQuantity { get; set; }
    }

    public class RevenueData
    {
        public DateTime Date { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
