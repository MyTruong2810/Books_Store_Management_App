using Books_Store_Management_App.Models;
using Npgsql; //Todo: Install Npgsql in terminal with "Install-Package Npgsql -Version 5.0.4" in NuGet Package Manager Console
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Books_Store_Management_App.Helpers;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models
{
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
                            Index = reader.GetString(10)
                        };
                        books.Add(book);
                    }
                }
            }

            return books;
        }

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
                                            Index = readerOrderItems.GetString(12)
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
        public List<Coupon> GetAllCoupons()
        {
            var coupons = new List<Coupon>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Coupon";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        coupons.Add(new Coupon
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Discount = (double)reader.GetDecimal(2),
                            ExpiryDate = reader.GetDateTime(3)
                        });
                    }
                }
            }

            return coupons;
        }
        public async Task<bool> SaveBookAsync(Book book)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Book (imageurl, title, publisher, author, isbn, year, selling_price, purchase_price, genre, quantity, index) " +
                               "VALUES (@ImageUrl, @Title, @Publisher, @Author, @ISBN, @Year, @SellingPrice, @PurchasePrice, @Genre, @Quantity, @Index)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ImageUrl", book.ImageSource);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Publisher", book.Publisher);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@Year", book.Year);
                    command.Parameters.AddWithValue("@SellingPrice", book.Price);
                    command.Parameters.AddWithValue("@PurchasePrice", book.PurchasePrice);
                    command.Parameters.AddWithValue("@Genre", book.Genre);
                    command.Parameters.AddWithValue("@Quantity", book.Quantity);
                    command.Parameters.AddWithValue("@Index", book.Index);

                    int result = await command.ExecuteNonQueryAsync();

                    return result > 0;
                }
            }
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Book SET imageurl = @ImageUrl, title = @Title, publisher = @Publisher, author = @Author, isbn = @ISBN, year = @Year, selling_price = @SellingPrice, purchase_price = @PurchasePrice, genre = @Genre, quantity = @Quantity WHERE index = @Index";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ImageUrl", book.ImageSource);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Publisher", book.Publisher);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@Year", book.Year);
                    command.Parameters.AddWithValue("@SellingPrice", book.Price);
                    command.Parameters.AddWithValue("@PurchasePrice", book.PurchasePrice);
                    command.Parameters.AddWithValue("@Genre", book.Genre);
                    command.Parameters.AddWithValue("@Quantity", book.Quantity);
                    command.Parameters.AddWithValue("@Index", book.Index);

                    int result = await command.ExecuteNonQueryAsync();

                    return result > 0;
                }
            }
        }

        public async Task<bool> DeleteBookAsync(string index)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Book WHERE index = @Index";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Index", index);

                    int result = await command.ExecuteNonQueryAsync();

                    return result > 0;
                }
            }
        }
    }
}
