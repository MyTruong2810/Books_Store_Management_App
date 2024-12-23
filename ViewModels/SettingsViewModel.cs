using Microsoft.UI.Xaml;
using System;
using System.ComponentModel;
using Windows.Storage;

namespace Books_Store_Management_App.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private bool _isDarkModeEnabled; // Dùng bool để kiểm soát chế độ tối

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsDarkModeEnabled
        {
            get => _isDarkModeEnabled;
            set
            {
                if (_isDarkModeEnabled != value)
                {
                    _isDarkModeEnabled = value;
                    SaveThemeToSettings(_isDarkModeEnabled); // Lưu trạng thái vào LocalSettings
                    OnPropertyChanged(nameof(IsDarkModeEnabled));
                }
            }
        }

        public ElementTheme CurrentTheme
        {
            get => IsDarkModeEnabled ? ElementTheme.Dark : ElementTheme.Light; // Nếu bật Dark Mode thì dùng Dark, không thì dùng Light
        }

        public void ToggleTheme()
        {
            IsDarkModeEnabled = !IsDarkModeEnabled; // Đảo ngược trạng thái
        }

        private void SaveThemeToSettings(bool isDarkModeEnabled)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["IsDarkModeEnabled"] = isDarkModeEnabled;
        }

        private bool LoadThemeFromSettings()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            return localSettings.Values.ContainsKey("IsDarkModeEnabled")
                ? (bool)localSettings.Values["IsDarkModeEnabled"]
                : false; // Mặc định là false (Light Mode)
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Khi khởi tạo ViewModel, tải theme từ LocalSettings
        public SettingsViewModel()
        {
            _isDarkModeEnabled = LoadThemeFromSettings();
            OnPropertyChanged(nameof(IsDarkModeEnabled));
        }
    }
}
