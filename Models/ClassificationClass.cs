using System;
using System.ComponentModel;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Represents a book classification category.
    /// </summary>
    public class ClassificationClass : INotifyPropertyChanged
    {
        private int _index;

        /// <summary>
        /// Gets or sets the index of the classification.
        /// </summary>
        public int Index
        {
            get => _index;
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged(nameof(Index));
                    OnPropertyChanged(nameof(IsEven));
                }
            }
        }

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes the PropertyChanged event.
        /// </summary>
        /// <param name="name">The name of the property that changed.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Determines if the index is even.
        /// </summary>
        public bool IsEven => Index % 2 == 0;

        /// <summary>
        /// Gets or sets the classification ID.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the classification tags.
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the classification description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Returns a string representation of the classification.
        /// </summary>
        public override string ToString()
        {
            return $"Class: {ID} - {Tags}";
        }
    }
}
