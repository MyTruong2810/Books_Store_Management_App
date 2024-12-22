using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Lớp DAO cho dữ liệu hồ sơ quản trị viên.
    /// </summary>
    public class AdminProfileDao : IDaos<AdminProfileViewModel>
    {
        // Load profile data (simulation)
        public PsqlDao psqlDao = new PsqlDao();
        public AdminProfileViewModel LoadProfile(string id)
        {
            // In practice, data would be fetched from a database
            return psqlDao.GetAdminByUsername(id);
        }

        // Save or update profile data (simulation)
        public void Save(AdminProfileViewModel profile, string newpass)
        {
            string username = Windows.Storage.ApplicationData.Current.LocalSettings.Values["username"].ToString();
            string hassedPass = "";
            if (newpass != "")
            {
                hassedPass = LoginViewModel.SHA_256(newpass);
            }
            psqlDao.UpdateAdminInfo(profile, username, hassedPass);

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
