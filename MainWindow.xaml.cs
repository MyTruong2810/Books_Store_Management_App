using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;
using Windows.ApplicationModel.Core;
using Microsoft.UI;
using WinRT.Interop;
using System;
using Windows.Media.Capture;

namespace Books_Store_Management_App
{
    public sealed partial class MainWindow : Window
    {
        public static Frame AppFrame { get; private set; }
        public MainWindow()
        {
            this.InitializeComponent();
            Activated += Window_Activated;
            AppFrame = contentMain;
        }

        public void Window_Activated(object sender, WindowActivatedEventArgs e)
        {
            //Todo: Change the icon of app 
            this.Title = "Book Store Management";
            contentMain.Navigate(typeof(Views.LoginPage));

        }
    }
}
