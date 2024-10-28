using Book_Store_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.ViewModels;

namespace Books_Store_Management_App.Models
{
    public class AdminProfileDao : IDaos<AdminProfileViewModel>
    {
        // Giả lập nguồn dữ liệu, có thể là database, API, hoặc file
        private AdminProfileViewModel mockDatabase = new AdminProfileViewModel
        {
            FullName = "LTCuberik",
            Email = "pdlinh1402@gmail.com",
            Phone = "+1 (123) 123 4654",
            DateOfBirth = "01-Jan-2002",
            Address = "Dong Hoa, Di An, Binh Duong, Viet Nam"
        };

        // Load profile data (giả lập)
        public AdminProfileViewModel LoadProfile(string id)
        {
            // Trong thực tế, dữ liệu sẽ được lấy từ database
            return mockDatabase;
        }

        // Save or update profile data (giả lập)
        public void Save(AdminProfileViewModel profile)
        {
            // Trong thực tế, dữ liệu sẽ được lưu vào database
            mockDatabase.FullName = profile.FullName;
            mockDatabase.Email = profile.Email;
            mockDatabase.DateOfBirth = profile.DateOfBirth;
            mockDatabase.Phone = profile.Phone;
            mockDatabase.Address = profile.Address;
        }

        // Delete profile by ID (optional implementation)

        public Tuple<int, List<AdminProfileViewModel>> GetAll(
            int page = 1, int rowsPerPage = 10, string keyword = "", bool nameAscending = false, bool IdAscending = false)
        {
            throw new NotImplementedException();
        }

        public void loadDatafromDbList()
        {
            throw new NotImplementedException();
        }

        public Tuple<int, List<AdminProfileViewModel>> GetSearch(int page = 1, int rowsPerPage = 10, string keyword = "", string typeofSearch = "", bool nameAscending = false, bool IdAscending = false)
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

        public Tuple<int, List<AdminProfileViewModel>> GetAll(int page, int rowsPerPage, string keyword, int typerOfSearch, int typerOfSort)
        {
            throw new NotImplementedException();
        }
    }

}
