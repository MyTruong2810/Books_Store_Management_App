using Books_Store_Management_App.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Books_Store_Management_App.Views
{
    public sealed partial class LoginPage : Page
    {
        /// <summary>
        /// Lớp xử lý các sự kiện của trang đăng nhập.
        /// </summary>
        public LoginPage()
        {
            this.InitializeComponent();
            DataContext = new ViewModels.LoginViewModel();

            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.LoginFailed += OnLoginFailed;
            }
        }

        private async void OnLoginFailed()
        {
            await ShowLoginFailedDialog.ShowAsync();
        }
    }

}
