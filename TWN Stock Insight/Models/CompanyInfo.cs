namespace TWN_Stock_Insight.Models
{
    public class CompanyInfo
    {
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
    }
}
