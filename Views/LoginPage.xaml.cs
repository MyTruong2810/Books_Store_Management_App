using Microsoft.UI.Xaml.Controls;

namespace Books_Store_Management_App.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            DataContext = new ViewModels.LoginViewModel();
        }
    }
}
