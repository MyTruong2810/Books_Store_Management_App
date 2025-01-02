using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.UI.Xaml;
using System;
using System.ComponentModel;

namespace Books_Store_Management_App.ViewModels
{
    /// <summary>
    /// ViewModel for managing application settings, including theme preferences and visual configurations.
    /// </summary>
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private bool _isDarkModeEnabled;

        /// <summary>
        /// Gets or sets the paint used for chart titles.
        /// </summary>
        public SolidColorPaint TitlePaint { get; set; }

        /// <summary>
        /// Gets or sets the paint used for axis names.
        /// </summary>
        public SolidColorPaint AxisNamePaint { get; set; }

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether dark mode is enabled.
        /// </summary>
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

        /// <summary>
        /// Gets the current application theme based on the dark mode setting.
        /// </summary>
        public ElementTheme CurrentTheme
        {
            get => IsDarkModeEnabled ? ElementTheme.Dark : ElementTheme.Light;
        }

        /// <summary>
        /// Toggles between light and dark themes.
        /// </summary>
        public void ToggleTheme()
        {
            IsDarkModeEnabled = !IsDarkModeEnabled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class with default settings.
        /// </summary>
        public SettingsViewModel()
        {
            _isDarkModeEnabled = false;
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
