using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.Models;

namespace Books_Store_Management_App.ViewModels
{
    public class AdminProfileViewModel : INotifyPropertyChanged
    {
        private string id;
        private string fullName;
        private string email;
        private string phone;
        private string dateOfBirth;
        private string address;

        private readonly IDaos<AdminProfileViewModel> _profileDao;

        public AdminProfileViewModel(IDaos<AdminProfileViewModel> profileDao)
        {
            _profileDao = profileDao;

            // Load initial data from DAO
            var loadedProfile = _profileDao.LoadProfile("0");
            ID = loadedProfile.ID;
            FullName = loadedProfile.FullName;
            Email = loadedProfile.Email;
            Phone = loadedProfile.Phone;
            dateOfBirth = loadedProfile.DateOfBirth;
            Address = loadedProfile.Address;
        }

        // Constructor (optional)
        public AdminProfileViewModel()
        {
        }

        // Properties with INotifyPropertyChanged

        public string ID
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        
        public string FullName
        {
            get { return fullName; }
            set { SetProperty(ref fullName, value); }
        }

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        public string Phone
        {
            get { return phone; }
            set { SetProperty(ref phone, value); }
        }

        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set { SetProperty(ref dateOfBirth, value); }
        }

        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Save profile data
        public void SaveProfile()
        {
            _profileDao.Save(this);  // Saving through DAO
        }
    }

    // Example: Load data from database
}
