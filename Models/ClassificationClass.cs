using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models
{
    public class ClassificationClass : INotifyPropertyChanged
    {
        public string ID { get; set; }
        public string Tags { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"Class: {ID} - {Tags}";
        }
    }
}
