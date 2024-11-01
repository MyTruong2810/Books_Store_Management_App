using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Books_Store_Management_App.Models;


public class StatisticDataManager
{
    private Dictionary<string, List<int>> data;

    public StatisticDataManager()
    {
        data = new Dictionary<string, List<int>>();
        data["Monthly"] = new List<int> { 200, 500, 800, 300, 600, 700, 400, 800, 200, 400, 600, 300 };
        data["Daily"] = GenerateDailyData();
        data["Yearly"] = new List<int> { 1500, 1800, 1200, 1600, 1400, 1700, 2000 };
    }

    private List<int> GenerateDailyData()
    {
        List<int> dailyData = new List<int>();
        Random random = new Random();
        for (int i = 0; i < 30; i++)
        {
            dailyData.Add(random.Next(100, 1000));
        }
        return dailyData;
    }

    public List<int> GetData(string timeframe)
    {
        return data.ContainsKey(timeframe) ? data[timeframe] : new List<int>();
    }
}
