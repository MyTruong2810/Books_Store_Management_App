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
    // This is the code-behind for the StatisticsPage.
    // It initializes the page and sets up the DataContext for binding.
    public sealed partial class StatisticsPage : Page
    {
        // Constructor for StatisticsPage.
        // It initializes the page and sets the DataContext to StatisticsViewModel.
        public StatisticsPage()
        {
            this.InitializeComponent(); // Initializes the UI components of the page.

            // Create an instance of OrderPageViewModel to fetch data from the database.
            var orderPageViewModel = new OrderPageViewModel(); // Hàm lấy dữ liệu từ CSDL

            // Set the DataContext of the page to StatisticsViewModel.
            // This allows binding between the UI and the view model.
            this.DataContext = new StatisticsViewModel(orderPageViewModel);
        }
    }
}
