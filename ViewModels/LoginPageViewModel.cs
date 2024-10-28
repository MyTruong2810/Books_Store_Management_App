using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Books_Store_Management_App.Views;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

namespace Books_Store_Management_App.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private bool _isPasswordSaved;

        private readonly Dictionary<string, string> _usersDatabase = new Dictionary<string, string>
        {
            { "user1", "123" },
            { "user2", "456" },
            { "user3", "789" }
        };

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsPasswordSaved
        {
            get => _isPasswordSaved;
            set => SetProperty(ref _isPasswordSaved, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand SignupCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(async () => await LoginAsync());
            SignupCommand = new RelayCommand(Signup);
            LoadSavedCredentials();
        }

        private async Task LoginAsync()
        {
            if (AuthenticateUser(Username, Password))
            {
                if (IsPasswordSaved)
                {
                    await SaveCredentialsAsync(Username, Password);
                }
                MainWindow.AppFrame.Navigate(typeof(MainPage));
            }
            else
            {
                //Todo: Show error message
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            return _usersDatabase.ContainsKey(username) && _usersDatabase[username] == password;
        }

        private async Task SaveCredentialsAsync(string username, string passwordRaw)
        {
            var passwordInBytes = Encoding.UTF8.GetBytes(passwordRaw);
            var entropyInBytes = new byte[20];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(entropyInBytes);
            }
            var encryptedPasswordInBytes = ProtectedData.Protect(passwordInBytes, entropyInBytes, DataProtectionScope.CurrentUser);
            var encryptedPasswordBase64 = Convert.ToBase64String(encryptedPasswordInBytes);
            var entropyInBase64 = Convert.ToBase64String(entropyInBytes);
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["Username"] = username;
            localSettings.Values["PasswordInBase64"] = encryptedPasswordBase64;
            localSettings.Values["EntropyInBase64"] = entropyInBase64;
        }

        private void LoadSavedCredentials()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue("Username", out object savedUsername) &&
                localSettings.Values.TryGetValue("PasswordInBase64", out object encryptedPassword) &&
                localSettings.Values.TryGetValue("EntropyInBase64", out object savedEntropy))
            {
                Username = savedUsername as string;
                var encryptedPasswordInBytes = Convert.FromBase64String(encryptedPassword as string);
                var entropyInBytes = Convert.FromBase64String(savedEntropy as string);
                var passwordInBytes = ProtectedData.Unprotect(encryptedPasswordInBytes, entropyInBytes, DataProtectionScope.CurrentUser);
                Password = Encoding.UTF8.GetString(passwordInBytes);
                IsPasswordSaved = true;
            }
        }

        private void Signup()
        {
           //Todo: Implement code later
        }

        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
