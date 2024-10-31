using Books_Store_Management_App.Models;
using Npgsql; //Todo: Install Npgsql in terminal with "Install-Package Npgsql -Version 5.0.4" in NuGet Package Manager Console
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Books_Store_Management_App.Helpers;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Thực hiện các thao tác với database ở đây chỉ thực hiện kết nối và lấy dữ liệu
    /// </summary>
    public class PsqlDao : IDao
    {
        private string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=mybookstore";

        public ObservableCollection<Book> GetAllBooks()
        {
            var books = new ObservableCollection<Book>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ImageUrl, Title, Publisher, Author, ISBN, Year, selling_price, purchase_price, Genre, Quantity, Index FROM Book";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var book = new Book()
                        {
                            ImageSource = reader.GetString(0),
                            Title = reader.GetString(1),
                            Publisher = reader.GetString(2),
                            Author = reader.GetString(3),
                            ISBN = reader.GetString(4),
                            Year = reader.GetInt32(5),
                            Price = (double)reader.GetDecimal(6),
                            PurchasePrice = (double)reader.GetDecimal(7),
                            Genre = reader.GetString(8),
                            Quantity = reader.GetInt32(9),
                            Index = reader.GetInt32(10)
                        };
                        books.Add(book);
                    }
                }
            }

            return books;
        }

        /* ===============================================================
         * You: Implement code the other methods for taking the database ||
         * ===============================================================
         */

        public ObservableCollection<Genre> GetAllGenres()
        {
            var genres = new ObservableCollection<Genre>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id, Name, Description FROM Genre";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        genres.Add(new Genre
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2)
                        });
                    }
                }
            }

            return genres;
        }

        public ObservableCollection<Order> GetAllOrders()
        {
            var orders = new ObservableCollection<Order>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM \"order\"";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = new Order()
                        {
                            ID = reader.GetInt32(0),
                            Customer = reader.GetString(1),
                            Date = reader.GetDateTime(2).ToString(),
                            IsDelivered = reader.GetBoolean(3),
                            Index = reader.GetInt32(0)
                        };

                        // Get Order_Items
                        List<OrderItem> OrderItems = new List<OrderItem>();
                        string queryOrderItems = $"SELECT oi.id, oi.quantity, b.* FROM order_item oi JOIN book b ON oi.book_id = b.index WHERE oi.order_id = {order.ID}";
                        using (var connectionOrderItems = new NpgsqlConnection(connectionString))
                        {
                            connectionOrderItems.Open();
                            using (var commandOrderItems = new NpgsqlCommand(queryOrderItems, connectionOrderItems))
                            using (var readerOrderItems = commandOrderItems.ExecuteReader())
                            {
                                while (readerOrderItems.Read())
                                {
                                    var orderItem = new OrderItem()
                                    {
                                        Id = readerOrderItems.GetInt32(0),
                                        Quantity = readerOrderItems.GetInt32(1),
                                        Book = new Book()
                                        {
                                            ImageSource = readerOrderItems.GetString(2),
                                            Title = readerOrderItems.GetString(3),
                                            Publisher = readerOrderItems.GetString(4),
                                            Author = readerOrderItems.GetString(5),
                                            ISBN = readerOrderItems.GetString(6),
                                            Year = readerOrderItems.GetInt32(7),
                                            Price = (double)readerOrderItems.GetDecimal(8),
                                            PurchasePrice = (double)readerOrderItems.GetDecimal(9),
                                            Genre = readerOrderItems.GetString(10),
                                            Quantity = readerOrderItems.GetInt32(11),
                                            Index = readerOrderItems.GetInt32(12)
                                        }
                                    };

                                    OrderItems.Add(orderItem);
                                }
                            }
                        }
                        order.OrderItems = OrderItems;

                        // Get Coupons
                        List<Coupon> Coupons = new List<Coupon>();
                        string queryCoupons = $"SELECT c.* FROM order_coupon oc JOIN coupon c ON oc.coupon_id = c.id WHERE order_id = {order.ID}";
                        using (var connectionCoupons = new NpgsqlConnection(connectionString))
                        {
                            connectionCoupons.Open();
                            using (var commandCoupons = new NpgsqlCommand(queryCoupons, connectionCoupons))
                            using (var readerCoupons = commandCoupons.ExecuteReader())
                            {
                                while (readerCoupons.Read())
                                {
                                    var coupon = new Coupon()
                                    {
                                        Id = readerCoupons.GetInt32(0),
                                        Name = readerCoupons.GetString(1),
                                        Discount = (double)readerCoupons.GetDecimal(2),
                                        ExpiryDate = readerCoupons.GetDateTime(3)
                                    };

                                    Coupons.Add(coupon);
                                }
                            }
                        }
                        order.Coupons = new FullObservableCollection<Coupon>(Coupons);

                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

    }
}
