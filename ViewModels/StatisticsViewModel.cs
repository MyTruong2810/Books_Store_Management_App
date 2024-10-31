using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Books_Store_Management_App.Models;
using OxyPlot;
using OxyPlot.Series;

namespace Books_Store_Management_App.ViewModels
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        public PlotModel PlotModel { get; private set; }

        public StatisticsViewModel()
        {
            PlotModel = new PlotModel { Title = "Biểu Đồ Số Lượng Sản Phẩm" };
            var lineSeries = new LineSeries { Title = "Số Lượng" };

            // Dữ liệu mẫu
            var dataPoints = new List<DataPoint>
            {
                new DataPoint(0, 5), // Thứ 2
                new DataPoint(1, 10), // Thứ 3
                new DataPoint(2, 15), // Thứ 4
                new DataPoint(3, 7),  // Thứ 5
                new DataPoint(4, 12)  // Thứ 6
            };

            lineSeries.ItemsSource = dataPoints;
            PlotModel.Series.Add(lineSeries);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}