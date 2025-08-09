// Services/StockService.cs
using System.Globalization;
using System.Text.Json;
using TWN_Stock_Insight.Models;

namespace TWN_Stock_Insight.Services
{
    public class StockService
    {
        private readonly HttpClient _http;

        public StockService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<StockPrice>> GetStockPricesAsync(string symbol)
        {
            // 取當月第一天
            var now = DateTime.Now;
            string dateParam = new DateTime(now.Year, now.Month, 1).ToString("yyyyMMdd");

            string url = $"https://www.twse.com.tw/exchangeReport/STOCK_DAY?response=json&date={dateParam}&stockNo={symbol}";
            var json = await _http.GetStringAsync(url);
            var doc = JsonDocument.Parse(json);

            if (!doc.RootElement.TryGetProperty("data", out var dataElement))
            {
                return new List<StockPrice>();
            }

            var result = new List<StockPrice>();

            foreach (var item in dataElement.EnumerateArray())
            {
                var dateStr = item[0].GetString(); // "113/01/02"
                var openStr = item[3].GetString();
                var highStr = item[4].GetString();
                var lowStr = item[5].GetString();
                var closeStr = item[6].GetString();
                var volumeStr = item[1].GetString();
                var countStr = item[8].GetString();

                // 民國年轉西元年
                var dateParts = dateStr.Split('/');
                int year = int.Parse(dateParts[0]) + 1911;
                int month = int.Parse(dateParts[1]);
                int day = int.Parse(dateParts[2]);
                DateTime date = new DateTime(year, month, day);

                result.Add(new StockPrice
                {
                    Date = date,
                    Open = decimal.Parse(openStr, CultureInfo.InvariantCulture),
                    High = decimal.Parse(highStr, CultureInfo.InvariantCulture),
                    Low = decimal.Parse(lowStr, CultureInfo.InvariantCulture),
                    Close = decimal.Parse(closeStr, CultureInfo.InvariantCulture),
                    Volume = long.Parse(volumeStr.Replace(",", "")),
                    Count = decimal.Parse(countStr,CultureInfo.InvariantCulture)
                });
            }

            return result.OrderBy(p => p.Date).ToList();
        }
    }
}
