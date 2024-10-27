using Books_Store_Management_App.Models;
using Npgsql; //Todo: Install Npgsql in terminal with "Install-Package Npgsql -Version 5.0.4" in NuGet Package Manager Console
using System;
using System.Collections.ObjectModel;

namespace Books_Store_Management_App.Models
{
    public class PsqlDao : IDao
    {
        private string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=28102004;Database=MyBookStore";

        public ObservableCollection<Book> GetAllBooks()
        {
            var books = new ObservableCollection<Book>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ImageUrl, Title, Publisher, Author, ISBN, Year, Price, Genre, Quantity, Index FROM Book";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book(
                            reader.GetString(0),            // ImageUrl
                            reader.GetString(1),            // Title
                            reader.GetString(2),            // Publisher
                            reader.GetString(3),            // Author
                            reader.GetString(4),            // ISBN
                            reader.GetInt32(5),             // Year
                            (double)reader.GetDecimal(6),   // Price
                            reader.GetString(7),            // Genre
                            reader.GetInt32(8),             // Quantity
                            reader.GetInt32(9)              // Index
                        ));
                    }
                }
            }

            return books;
        }

        public ObservableCollection<Order> GetAllOrders()
        {
            var orders = new ObservableCollection<Order>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Customer, Date, Discount, Amount, Price, OrderIndex FROM Order_Book";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order(
                            reader.GetString(0),                   // ID
                            reader.GetString(1),                   // Customer
                            reader.GetDateTime(2).ToString(),      // Date as DateTime
                            reader.GetInt32(3),                    // Discount
                            reader.GetInt32(4),                    // Amount
                            (double)reader.GetDecimal(5),          // Price
                            reader.GetInt32(6)                     // Index
                        ));
                    }
                }
            }

            return orders;
        }

        /* ===============================================================
         * You: Implement code the other methods for taking the database ||
         * ===============================================================
         */
    }
}
