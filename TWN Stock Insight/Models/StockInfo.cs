namespace TWN_Stock_Insight.Models
{
    public class StockInfo
    {
        public string Symbol { get; set; }      // 股票代號
        public string Name { get; set; }        // 公司名稱
        public string Industry { get; set; }    // 產業類別
    }

    public class StockPrice
    {
        public DateTime Date { get; set; }      //日期
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public long Volume { get; set; }
        public decimal Count { get; set; }
    }
}
