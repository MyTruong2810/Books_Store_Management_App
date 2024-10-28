using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Books_Store_Management_App.Models;
using Books_Store_Management_App.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Books_Store_Management_App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        public AdminProfileViewModel ViewModel { get; }
        public AdminPage()
        {
            this.InitializeComponent();

            // Injecting DAO vào ViewModel
            var profileDao = new AdminProfileDao();  // Có thể thay đổi thành DAO khác nếu cần
            ViewModel = new AdminProfileViewModel(profileDao);

            this.DataContext = ViewModel;
        }

        private async void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            ContentDialogResult result = await EditProfileDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // Save button was clicked
                ViewModel.SaveProfile();
            }
        }
    }
}
