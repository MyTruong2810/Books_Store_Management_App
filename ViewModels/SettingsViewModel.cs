using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.UI.Xaml;
using System;
using System.ComponentModel;

namespace Books_Store_Management_App.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private bool _isDarkModeEnabled; // Mặc định là chế độ sáng

        // Paint for titles and axes
        public SolidColorPaint TitlePaint { get; set; }
        public SolidColorPaint AxisNamePaint { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsDarkModeEnabled
        {
            get => _isDarkModeEnabled;
            set
            {
                if (_isDarkModeEnabled != value)
                {
                    _isDarkModeEnabled = value;
                    OnPropertyChanged(nameof(IsDarkModeEnabled));
                    OnPropertyChanged(nameof(CurrentTheme));
                }
            }
        }

        public ElementTheme CurrentTheme
        {
            get => IsDarkModeEnabled ? ElementTheme.Dark : ElementTheme.Light; // Chuyển đổi theme
        }

        public void ToggleTheme()
        {
            IsDarkModeEnabled = !IsDarkModeEnabled; 
        }

        public SettingsViewModel()
        {
            _isDarkModeEnabled = false; // Mặc định là chế độ Light khi khởi chạy
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
