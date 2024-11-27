using Microsoft.UI.Xaml.Controls;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using OxyPlot.Wpf;
using Books_Store_Management_App.ViewModels;
using Books_Store_Management_App.Helpers;
using Books_Store_Management_App.Models;

namespace Books_Store_Management_App.Views
{
    public sealed partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            this.InitializeComponent();
            this.DataContext = new StatisticsViewModel();
        }
    }
}