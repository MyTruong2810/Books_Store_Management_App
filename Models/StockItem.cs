using Catel.Data;
using System.Windows.Input;
using Npgsql;
using Catel.MVVM;
using System;
using System.Text.RegularExpressions;

namespace Books_Store_Management_App.Models
{
    public class StockItem : ModelBase
    {
        // Properties for product name and remaining quantity
        public string ProductName { get; set; }

        private string _remainingQuantity;
        public string RemainingQuantity
        {
            get => _remainingQuantity;
            set
            {
                if (_remainingQuantity != value)
                {
                    _remainingQuantity = value;
                    RaisePropertyChanged(nameof(RemainingQuantity)); // Notify UI about the change
                }
            }
        }

        // Current available stock quantity
        public int CurrentQuantity { get; set; }

        // Command for reorder action
        public ICommand ReorderCommand { get; set; }

        // Constructor to initialize the reorder command
        public StockItem()
        {
            ReorderCommand = new RelayCommand(Reorder); // Bind the Reorder method to the command
        }

        #region Reorder and Stock Update Methods

        // Method to handle reorder action
        private void Reorder()
        {
            // Extract numeric value from RemainingQuantity string
            string numericPart = Regex.Match(RemainingQuantity, @"\d+").Value;

            // Parse the extracted numeric value to update stock
            if (int.TryParse(numericPart, out int newQuantity) && newQuantity > CurrentQuantity)
            {
                UpdateStockInDatabase(newQuantity); // Update stock in database
                CurrentQuantity = newQuantity; // Update local CurrentQuantity
                Console.WriteLine("Main thread starting...");

                // Notify property changes to update UI
                RaisePropertyChanged(nameof(CurrentQuantity));
                RaisePropertyChanged(nameof(RemainingQuantity));
            }
        }

        // Method to update stock quantity in the database
        private void UpdateStockInDatabase(int newQuantity)
        {
            try
            {
                // Connection string to connect to the PostgreSQL database
                string connectionString = "Host=localhost;Username=postgres;Password=admin;Database=mybookstore";

                // Open database connection
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL query to update stock quantity for the given product
                    string query = "UPDATE public.\"book\" SET quantity = @newQuantity WHERE title = @title";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("newQuantity", newQuantity);
                        cmd.Parameters.AddWithValue("title", ProductName);

                        // Execute the query and check how many rows were affected
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Stock updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No rows were updated. Check the title.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during the stock update process
                Console.WriteLine($"Error updating stock: {ex.Message}");
            }
        }

        #endregion
    }
}
