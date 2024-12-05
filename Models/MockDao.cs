﻿using Books_Store_Management_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Lớp MockDao chứa dữ liệu mẫu.
    /// </summary>
    public class MockDao : IDao
    {
        public ObservableCollection<Book> GetAllBooks()
        {
            var result = new ObservableCollection<Book>
                {
                    //new Book("ms-appx:///Assets/image1.jpg", "Title 1", "Publisher 1", "Author 1", "9786047751181", 2022, 10.99, "Fiction", 100, 0),
                    //new Book("ms-appx:///Assets/image2.jpg", "Title 2", "Publisher 2", "Author 2", "9786047751182", 2021, 15.99, "Non-Fiction", 50, 1),
                    //new Book("ms-appx:///Assets/image1.jpg", "Title 3", "Publisher 3", "Author 3", "9786047751183", 2019, 7.99, "Drama", 80, 2),
                    //new Book("ms-appx:///Assets/image2.jpg", "Title 4", "Publisher 4", "Author 4", "9786047751184", 2022, 12.99, "Science", 120, 3),
                };

            return result;
        }
        public List<Coupon> GetAllCoupons()
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<Genre> GetAllGenres()
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<ClassificationClass> GetClassificationClasses()
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<Customer> GetAllCustomers() {
            throw new NotImplementedException();
        }
        public ObservableCollection<Order> GetAllOrders()
        {
            var result = new ObservableCollection<Order>
                {
                    //new Order("#001", "Peter Pan", "12:00AM - 12/10/2024", 30, 3, 12.2, 0),
                    //new Order("#001", "Peter Pan", "12:00AM - 12/10/2024", 30, 3, 12.2, 1),
                    //new Order("#001", "Peter Pan", "12:00AM - 12/10/2024", 30, 3, 12.2, 2),
                    //new Order("#001", "Peter Pan", "12:00AM - 12/10/2024", 30, 3, 12.2, 3),
                };

            return result;
        }
        public bool DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }
        public bool DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }
        public bool DeleteClassificationClasses(ClassificationClass genre)
        {
            throw new NotImplementedException();
        }
        public bool DeleteCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
