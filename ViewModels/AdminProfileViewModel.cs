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
    /// <summary>
    /// Lớp ViewModel cho trang AdminProfile để hiển thị và quản lý thông tin cá nhân của Admin.
    /// </summary>
    public class AdminProfileViewModel : INotifyPropertyChanged
    {
        // Private fields for profile properties
        private string id;
        private string fullName;
        private string email;
        private string phone;
        private string dateOfBirth;
        private string address;

        // DAO interface to manage profile data persistence
        private readonly IDaos<AdminProfileViewModel> _profileDao;

        // Constructor with DAO dependency injection
        public AdminProfileViewModel(IDaos<AdminProfileViewModel> profileDao)
        {
            _profileDao = profileDao;

            // Load initial data from DAO (example usage)
            var loadedProfile = _profileDao.LoadProfile("0"); // "0" is example ID
            ID = loadedProfile.ID;
            FullName = loadedProfile.FullName;
            Email = loadedProfile.Email;
            Phone = loadedProfile.Phone;
            dateOfBirth = loadedProfile.DateOfBirth;
            Address = loadedProfile.Address;
        }

        // Parameterless constructor (optional)
        public AdminProfileViewModel()
        {
        }

        // Properties with INotifyPropertyChanged to notify UI on change

        public string ID
        {
            get { return id; }
            set { SetProperty(ref id, value); } // SetProperty to raise PropertyChanged
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

        // INotifyPropertyChanged event to update bound UI elements
        public event PropertyChangedEventHandler PropertyChanged;

        // Helper method to set property and raise PropertyChanged if value changes
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Save method to persist profile changes using DAO
        public void SaveProfile()
        {
            _profileDao.Save(this);  // Saves the current profile to storage
        }
    }
}
