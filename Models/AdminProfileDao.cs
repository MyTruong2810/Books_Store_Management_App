using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.ViewModels;
using System.Collections.ObjectModel;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Lớp DAO cho dữ liệu hồ sơ quản trị viên.
    /// </summary>
    public class AdminProfileDao : IDaos<AdminProfileViewModel>
    {
        // Mock data source, simulating a database, API, or file storage
        private AdminProfileViewModel mockDatabase = new AdminProfileViewModel
        {
            FullName = "LTCuberik",
            Email = "pdlinh1402@gmail.com",
            Phone = "+1 (123) 123 4654",
            DateOfBirth = "01-Jan-2002",
            Address = "Dong Hoa, Di An, Binh Duong, Viet Nam"
        };

        // Load profile data (simulation)
        public AdminProfileViewModel LoadProfile(string id)
        {
            // In practice, data would be fetched from a database
            return mockDatabase;
        }

        // Save or update profile data (simulation)
        public void Save(AdminProfileViewModel profile)
        {
            // In practice, data would be saved to a database
            mockDatabase.FullName = profile.FullName;
            mockDatabase.Email = profile.Email;
            mockDatabase.DateOfBirth = profile.DateOfBirth;
            mockDatabase.Phone = profile.Phone;
            mockDatabase.Address = profile.Address;
        }

        // Optional implementation to delete profile by ID

        public Tuple<int, ObservableCollection<AdminProfileViewModel>> GetAll(
            int page = 1, int rowsPerPage = 10, string keyword = "", bool nameAscending = false, bool IdAscending = false)
        {
            throw new NotImplementedException();
        }

        public void loadDatafromDbList()
        {
            throw new NotImplementedException();
        }

        public Tuple<int, ObservableCollection<AdminProfileViewModel>> GetSearch(int page = 1, int rowsPerPage = 10, string keyword = "", string typeofSearch = "", bool nameAscending = false, bool IdAscending = false)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(AdminProfileViewModel insertItem)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, ObservableCollection<AdminProfileViewModel>> GetAll(int page, int rowsPerPage, string keyword, int typerOfSearch, int typerOfSort)
        {
            throw new NotImplementedException();
        }
    }
}
