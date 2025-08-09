using Microsoft.AspNetCore.Mvc;
using TWN_Stock_Insight.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TWN_Stock_Insight.Controllers
{
    public class StocksController : Controller
    {
        private readonly StockService _stockService;

        public StocksController(StockService stockService)
        {
            _stockService = stockService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(string symbol)
        {
            var prices = await _stockService.GetStockPricesAsync(symbol);
            ViewBag.Symbol = symbol;
            //return View(prices);
            return PartialView("_StockResult", prices);
        }

        [HttpPost]
        public async Task<JsonResult> DetailsJson(string symbol)
        {
            var result = await _stockService.GetStockPricesAsync(symbol);
            return Json(result); // 回傳 JSON
        }
    }
}
