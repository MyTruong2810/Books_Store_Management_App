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
    /// Lớp xử lý các sự kiện của trang Admin.
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        public AdminProfileViewModel ViewModel { get; }

        public AdminPage()
        {
            this.InitializeComponent();

            // Injecting the DAO into the ViewModel.
            var profileDao = new AdminProfileDao();  // Can be replaced with a different DAO if needed.
            ViewModel = new AdminProfileViewModel(profileDao);

            this.DataContext = ViewModel; // Set the data context for data binding.
        }

        // Event handler for the Edit Profile button click.
        private async void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            // Show the Edit Profile dialog and wait for the result.
            ContentDialogResult result = await EditProfileDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // If the save button was clicked in the dialog, save the profile changes.
                ViewModel.SaveProfile();
            }
        }
    }
}