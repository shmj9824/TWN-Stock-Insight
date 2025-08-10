// Services/CompanyService.cs
using System.Globalization;
using System.Text.Json;
using TWN_Stock_Insight.Models;

namespace TWN_Stock_Insight.Services
{
    public class CompanyService
    {
        private readonly HttpClient _http;

        public CompanyService(HttpClient http)
        { 
            _http = http;
        }

        public async Task<List<CompanyInfo>> GetCompanyInfosAsync()
        {
            string url = $"https://openapi.twse.com.tw/v1/opendata/t187ap03_L";
            var json = await _http.GetStringAsync(url);
            var doc = JsonDocument.Parse(json);

            if (!doc.RootElement.TryGetProperty("data", out var dataElement))
            {
                return new List<CompanyInfo>();
            }

            var result = new List<CompanyInfo>();

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
                /*
                result.Add(new CompanyInfo
                {
                    CompanyCode = 
                    Date = date,
                    Open = decimal.Parse(openStr, CultureInfo.InvariantCulture),
                    High = decimal.Parse(highStr, CultureInfo.InvariantCulture),
                    Low = decimal.Parse(lowStr, CultureInfo.InvariantCulture),
                    Close = decimal.Parse(closeStr, CultureInfo.InvariantCulture),
                    Volume = long.Parse(volumeStr.Replace(",", "")),
                    Count = decimal.Parse(countStr, CultureInfo.InvariantCulture)
                });
                */
            }
            /*
            public string CompanyCode { get; set; }     //公司代號
        public string CompanyName { get; set; }     //公司名稱
        public string CompanyAbbreviation { get; set; } // 公司簡稱
        public string Industry { get; set; }        //產業別
        public string Address { get; set; }         //住址
        public string Chairman { get; set; }        //董事長
        public string GeneralManager { get; set; }  //總經理
        public DateTime EstablishDate { get; set; }   //設立日期
        public decimal PaidInCapital { get; set; }   //實收資本額
        public decimal IssuedShares { get; set; }    //已發行普通股數或TDR原發行股數
        public DateTime IPODate { get; set; }         //公開發行日期
            */
            return result.OrderBy(p => p.CompanyCode).ToList();
        }
    }
}
