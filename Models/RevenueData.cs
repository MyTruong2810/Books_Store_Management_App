using System;

namespace Books_Store_Management_App.Models
{
    // This class stores the revenue data for a specific date.
    public class RevenueData
    {
        // Date property to store the date for which revenue is calculated.
        public DateTime Date { get; set; }

        // TotalRevenue property to store the total revenue for the specified date.
        public double TotalRevenue { get; set; }
    }
}
