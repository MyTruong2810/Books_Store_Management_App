using Microsoft.UI.Xaml.Controls;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using OxyPlot.Wpf;

namespace Books_Store_Management_App.Views
{
    public sealed partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            this.InitializeComponent();

            // Tạo PlotModel cho biểu đồ
            var plotModel = new PlotModel { Title = "Số Lượng Sản Phẩm Theo Thời Gian" };

            // Tạo một LineSeries và thêm dữ liệu vào
            var lineSeries = new LineSeries
            {
                Title = "Sản Phẩm Bán Ra",
                MarkerType = MarkerType.Circle
            };

            // Thêm dữ liệu mẫu
            lineSeries.Points.Add(new DataPoint(0, 5));
            lineSeries.Points.Add(new DataPoint(1, 15));
            lineSeries.Points.Add(new DataPoint(2, 25));
            lineSeries.Points.Add(new DataPoint(3, 35));
            lineSeries.Points.Add(new DataPoint(4, 45));

            // Thêm LineSeries vào PlotModel
            plotModel.Series.Add(lineSeries);

            // Khởi tạo PlotView và gán mô hình cho nó
            var plotView = new PlotView
            {
                Model = plotModel,
                Width = 800,
                Height = 400
            };

        }
    }
}