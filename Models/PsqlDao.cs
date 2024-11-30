using Books_Store_Management_App.Models;
using Npgsql; //Todo: Install Npgsql in terminal with "Install-Package Npgsql -Version 5.0.4" in NuGet Package Manager Console
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Books_Store_Management_App.Helpers;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Security.Policy;
using System.Windows.Media.Imaging;
using BitmapImage = Microsoft.UI.Xaml.Media.Imaging.BitmapImage;
using Windows.Gaming.Input;
using Books_Store_Management_App.ViewModels;
using System.Xml.Linq;
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
                string query = "SELECT * FROM classification";

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
        public ObservableCollection<ClassificationClass> GetClassificationClasses()
        {
            var genres = new ObservableCollection<ClassificationClass>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT code, ten, description FROM classification";
                int cnt = 0;
                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var classification = new ClassificationClass
                        {
                            ID = reader.GetInt32(0).ToString(),
                            Tags = reader.GetString(1),
                            Description = reader.GetString(2)
                        };
                        genres.Add(classification);
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
                            ID = reader.GetString(0),
                            Customer = reader.GetString(1),
                            Date = reader.GetDateTime(2),
                            IsDelivered = reader.GetBoolean(3),
                        };

                        // Get Order_Items
                        List<OrderItem> OrderItems = new List<OrderItem>();
                        string queryOrderItems = $"SELECT oi.id, oi.quantity, b.* FROM order_item oi JOIN book b ON oi.book_id = b.index WHERE oi.order_id = @OrderId";
                        using (var connectionOrderItems = new NpgsqlConnection(connectionString))
                        {
                            connectionOrderItems.Open();
                            using (var commandOrderItems = new NpgsqlCommand(queryOrderItems, connectionOrderItems))
                            {
                                commandOrderItems.Parameters.AddWithValue("@OrderId", order.ID);
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
                        }
                        order.OrderItems = OrderItems;

                        // Get Coupons
                        List<Coupon> Coupons = new List<Coupon>();
                        string queryCoupons = $"SELECT c.* FROM order_coupon oc JOIN coupon c ON oc.coupon_id = c.id WHERE order_id = @OrderId";
                        using (var connectionCoupons = new NpgsqlConnection(connectionString))
                        {
                            connectionCoupons.Open();
                            using (var commandCoupons = new NpgsqlCommand(queryCoupons, connectionCoupons))
                            {
                                commandCoupons.Parameters.AddWithValue("@OrderId", order.ID);
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
        public ObservableCollection<Customer> GetAllCustomers()
        {
            var customers = new ObservableCollection<Customer>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Gender, Phone, Address, AvatarLink, CVV, PaymentMethod FROM Customer";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customer = new Customer()
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Gender = reader.GetString(2),
                            Phone = reader.GetString(3),
                            Address = reader.GetString(4),
                            Avatar = reader.GetString(5),
                            CVV = reader.GetInt32(6),
                            Payment = reader.GetString(7)
                        };
                        customers.Add(customer);
                    }
                }
            }
            return customers;
        }
        public bool DeleteBook(Book book)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Book WHERE Title = @BookId"; // Đảm bảo tên bảng và cột đúng với cơ sở dữ liệu

                using (var command = new NpgsqlCommand(query, connection))
                {
                    // Thêm tham số để tránh SQL Injection
                    command.Parameters.AddWithValue("@BookId", book.Title);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Trả về true nếu có bản ghi bị xóa
                }
            }
        }
        public bool DeleteOrder(Order order)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM \"Order\" WHERE Id = @OrderId"; // Bọc tên bảng bằng ngoặc kép

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        // Thêm tham số để tránh SQL Injection
                        command.Parameters.AddWithValue("@OrderId", order.ID);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về true nếu có bản ghi bị xóa
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log hoặc thông báo lỗi (tùy thuộc vào yêu cầu dự án)
                Console.WriteLine($"Error while deleting order: {ex.Message}");
                return false;
            }
        }

        public bool DeleteClassificationClasses(ClassificationClass genre)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Classification WHERE Code = @GenreId"; // Đảm bảo tên bảng và cột đúng với cơ sở dữ liệu

                using (var command = new NpgsqlCommand(query, connection))
                {
                    // Thêm tham số để tránh SQL Injection
                    command.Parameters.AddWithValue("@GenreId", Convert.ToInt32(genre.ID));
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Trả về true nếu có bản ghi bị xóa
                }
            }
        }
        public bool DeleteCustomer(Customer customer)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Customer WHERE Id = @CustomerId"; // Đảm bảo tên bảng và cột đúng với cơ sở dữ liệu

                using (var command = new NpgsqlCommand(query, connection))
                {
                    // Thêm tham số để tránh SQL Injection
                    command.Parameters.AddWithValue("@CustomerId", customer.ID);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Trả về true nếu có bản ghi bị xóa
                }
            }
        }

        public bool UpdateClassification(ClassificationClass classification)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Classification SET Ten = @Name, Description = @Description WHERE Code = @ID";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", Convert.ToInt32(classification.ID)); 
                    command.Parameters.AddWithValue("@Name", classification.Tags); 
                    command.Parameters.AddWithValue("@Description", classification.Description);  

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;  
                }
            }
        }
        public bool UpdateCustomer(Customer customer)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Customer SET Name = @Name, Gender = @Gender, Phone = @Phone, Address = @Address, AvatarLink = @Avatar, CVV = @CVV, PaymentMethod = @Payment WHERE Id = @ID";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", customer.ID);
                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Gender", customer.Gender);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@Avatar", customer.Avatar);
                    command.Parameters.AddWithValue("@CVV", customer.CVV);
                    command.Parameters.AddWithValue("@Payment", customer.Payment);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public bool InsertClassification(ClassificationClass genre)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Classification (Code, Ten, Description) VALUES (@ID, @Name, @Description)";

                using (var command = new NpgsqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@ID", Convert.ToInt32(genre.ID));
                    command.Parameters.AddWithValue("@Name", genre.Tags);
                    command.Parameters.AddWithValue("@Description", genre.Description);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if a record is added
                }
            }
        }
        public bool InsertCustomer(Customer customer)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Customer (Name, Gender, Phone, Address, AvatarLink, CVV, PaymentMethod) " +
                               "VALUES (@Name, @Gender, @Phone, @Address, @Avatar, @CVV, @Payment)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Gender", customer.Gender);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@Avatar", customer.Avatar);
                    command.Parameters.AddWithValue("@CVV", customer.CVV);
                    command.Parameters.AddWithValue("@Payment", customer.Payment);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if a record is added
                }
            }
        }

        public List<Book> GetAllAvailableBooks()
        {
            var books = new List<Book>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ImageUrl, Title, Publisher, Author, ISBN, Year, selling_price, purchase_price, Genre, Quantity, Index FROM Book WHERE Quantity > 0";

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

        public async Task<bool> SaveOrderAsync(Order order)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO \"order\" (id, customer, date, is_delivered) " +
                               "VALUES (@Id, @Customer, @Date, @IsDelivered)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", order.ID);
                    command.Parameters.AddWithValue("@Customer", order.Customer);
                    command.Parameters.AddWithValue("@Date", order.Date);
                    command.Parameters.AddWithValue("@IsDelivered", order.IsDelivered);

                    int result = await command.ExecuteNonQueryAsync();

                    if (result <= 0)
                    {
                        return false;
                    }
                }
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO order_item (order_id, book_id, quantity) " +
                               "VALUES (@OrderId, @BookId, @Quantity)";

                foreach (OrderItem item in order.OrderItems)
                {
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", order.ID);
                        command.Parameters.AddWithValue("@BookId", item.Book.Index);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);

                        int result = await command.ExecuteNonQueryAsync();

                        if (result <= 0)
                        {
                            return false;
                        }
                    }
                }
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO order_coupon (order_id, coupon_id) " +
                               "VALUES (@OrderId, @CouponId)";

                foreach (Coupon coupon in order.Coupons)
                {
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", order.ID);
                        command.Parameters.AddWithValue("@CouponId", coupon.Id);

                        int result = await command.ExecuteNonQueryAsync();

                        if (result <= 0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE \"order\" SET customer = @Customer, date = @Date, is_delivered = @IsDelivered WHERE id = @Id";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", order.ID);
                    command.Parameters.AddWithValue("@Customer", order.Customer);
                    command.Parameters.AddWithValue("@Date", order.Date);
                    command.Parameters.AddWithValue("@IsDelivered", order.IsDelivered);

                    int result = await command.ExecuteNonQueryAsync();

                    if (result <= 0)
                    {
                        return false;

                    }
                }
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM order_item WHERE order_id = @OrderId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", order.ID);

                    int result = await command.ExecuteNonQueryAsync();
                }

                query = "INSERT INTO order_item (order_id, book_id, quantity) " +
                        "VALUES (@OrderId, @BookId, @Quantity)";

                foreach (OrderItem item in order.OrderItems)
                {
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", order.ID);
                        command.Parameters.AddWithValue("@BookId", item.Book.Index);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);

                        int result = await command.ExecuteNonQueryAsync();

                    }
                }
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM order_coupon WHERE order_id = @OrderId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", order.ID);

                    int result = await command.ExecuteNonQueryAsync();

                    if (result <= 0)
                    {
                        return false;
                    }
                }

                query = "INSERT INTO order_coupon (order_id, coupon_id) " +
                        "VALUES (@OrderId, @CouponId)";

                foreach (Coupon coupon in order.Coupons)
                {
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", order.ID);
                        command.Parameters.AddWithValue("@CouponId", coupon.Id);

                        int result = await command.ExecuteNonQueryAsync();

                        if (result <= 0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM order_item WHERE order_id = @OrderId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", id);
                }
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM order_coupon WHERE order_id = @OrderId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", id);

                    int result = await command.ExecuteNonQueryAsync();
                }
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM \"order\" WHERE id = @Id";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int result = await command.ExecuteNonQueryAsync();

                    if (result <= 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
