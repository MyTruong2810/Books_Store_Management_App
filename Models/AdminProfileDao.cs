using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Books_Store_Management_App.ViewModels;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Data Access Object for managing administrator profiles.
    /// </summary>
    public class AdminProfileDao : IDaos<AdminProfileViewModel>
    {
        public PsqlDao psqlDao = new PsqlDao();

        /// <summary>
        /// Retrieves the profile of an administrator by ID.
        /// </summary>
        /// <param name="id">The administrator's ID.</param>
        /// <returns>The administrator profile view model.</returns>
        public AdminProfileViewModel LoadProfile(string id)
        {
            return psqlDao.GetAdminByUsername(id);
        }

        /// <summary>
        /// Saves or updates an administrator profile.
        /// </summary>
        /// <param name="profile">The profile view model to save or update.</param>
        /// <param name="newpass">The new password, if applicable.</param>
        public void Save(AdminProfileViewModel profile, string newpass)
        {
            string username = Windows.Storage.ApplicationData.Current.LocalSettings.Values["username"].ToString();
            string hassedPass = "";

            if (!string.IsNullOrEmpty(newpass))
            {
                hassedPass = LoginViewModel.SHA_256(newpass);
            }

            psqlDao.UpdateAdminInfo(profile, username, hassedPass);
        }

        /// <summary>
        /// Retrieves all administrator profiles with pagination and sorting options.
        /// </summary>
        public Tuple<int, ObservableCollection<AdminProfileViewModel>> GetAll(
            int page = 1, int rowsPerPage = 10, string keyword = "", bool nameAscending = false, bool IdAscending = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads data from the database into a list.
        /// </summary>
        public void loadDatafromDbList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves administrator profiles based on search criteria.
        /// </summary>
        public Tuple<int, ObservableCollection<AdminProfileViewModel>> GetSearch(
            int page = 1, int rowsPerPage = 10, string keyword = "", string typeofSearch = "", bool nameAscending = false, bool IdAscending = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an administrator profile by ID.
        /// </summary>
        /// <param name="id">The ID of the profile to delete.</param>
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserts a new administrator profile.
        /// </summary>
        /// <param name="insertItem">The profile to insert.</param>
        public void Insert(AdminProfileViewModel insertItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves all administrator profiles with additional filtering and sorting options.
        /// </summary>
        public Tuple<int, ObservableCollection<AdminProfileViewModel>> GetAll(
            int page, int rowsPerPage, string keyword, int typerOfSearch, int typerOfSort)
        {
            throw new NotImplementedException();
        }
    }
}
