using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Lớp đại diện cho thể loại sách 1.
    /// </summary>
    public class ClassificationClass : INotifyPropertyChanged
    {
        public int _index;
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
        // PropertyChanged event to update the UI when property values change
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public bool IsEven => Index % 2 == 0;
        public string ID { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public override string ToString()
        {
            return $"Class: {ID} - {Tags}";
        }
    }
}