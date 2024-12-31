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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
using System.Globalization;

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

                ViewModel.SaveProfile(newpass.Password);
            }

            Frame.Navigate(typeof(AdminPage));
        }
        private void OnSaveButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(ViewModel.FullName))
            {
                MessageBox.Show("Fullname is required.");
                args.Cancel = true;
                return;
            }
            if (!IsValidEmail(ViewModel.Email))
            {
                MessageBox.Show("Please enter a valid email address.");
                args.Cancel = true;
                return;
            }
            if (!IsValidDate(ViewModel.DateOfBirth))
            {
                MessageBox.Show("Please enter a valid Date of Birth (yyyy-mm-dd).");
                args.Cancel = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(ViewModel.Phone))
            {
                MessageBox.Show("Phone number is required.");
                args.Cancel = true;
                return;
            }
            if (!IsPhoneNumberValid(ViewModel.Phone))
            {
                MessageBox.Show("Phone number must contain only digits.");
                args.Cancel = true;
                return;
            }

            SaveProfile();
        }

        private bool IsPhoneNumberValid(string phone)
        {
            return phone.All(char.IsDigit);
        }


        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidDate(string date)
        {
            // Kiểm tra định dạng ngày (yyyy-mm-dd)
            DateTime tempDate;
            return DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate);
        }

        private void SaveProfile()
        {
            // Lưu thông tin profile tại đây
            MessageBox.Show("Profile saved successfully!");
        }
    }
}