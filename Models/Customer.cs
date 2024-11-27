using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models
{
    public partial class Customer : INotifyPropertyChanged
    {
        public int _index;
        public int ID { get; set; }
        public string Name { get; set; }

        public int Index
        {
            get => _index;
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged(nameof(Index));
                    OnPropertyChanged(nameof(IsEven)); // Notify when IsEven changes
                }
            }
        }
        public bool IsEven => Index % 2 == 0;


        // BitmapImage used to store the customer's profile picture.
        // This is an advanced technique allowing image display in the UI.
        private string _avatar;
        public string Avatar
        {
            get => _avatar;
            set
            {
                _avatar = value;
                // OnPropertyChanged ensures the UI updates when the profile picture changes.
                OnPropertyChanged(nameof(Avatar));
            }
        }

        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Payment { get; set; }
        public int CVV { get; set; }
        public string Address { get; set; }

        // PropertyChanged event to update the UI when property values change
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Override of ToString method to return basic customer information
        public override string ToString()
        {
            return $"Customer: {ID} - {Name}";
        }
    }
}