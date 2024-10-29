using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models
{
    public partial class Customer : INotifyPropertyChanged
    {
        public string ID { get; set; }
        public string Name { get; set; }
        private BitmapImage _avatar;
        public BitmapImage Avatar
        {
            get => _avatar;
            set
            {
                _avatar = value;
                OnPropertyChanged(nameof(Avatar));
            }
        }

        public string Phone { get; set; }
        public string Gender { get; set; }
        public string DateofBirth { get; set; }
        public string Payment { get; set; }
        public int CVV { get; set; }
        public string Address { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public override string ToString()
        {
            return $"Customer: {ID} - {Name}";
        }
    }
}
