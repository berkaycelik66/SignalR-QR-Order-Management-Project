using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Session;
using SignalRWebUI.Dtos.OrderDetailDtos;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SignalRWebUI.Controllers
{
    public class BasketsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasketsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var orderId = HttpContext.Session.GetInt32("OrderID");

            if(orderId != null)
            {

                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7202/api/OrderDetail/OrderDetailListByOrderIdWithProducts/" + orderId);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultOrderDetailByOrderIdWithProducts>>(jsonData);
                    return View(values);
                }

                return View();
            }

            return RedirectToAction("CustomerTableList", "CustomerTable");
        }

        public async Task<IActionResult> DeleteBasket(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7202/api/OrderDetail/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateCompletionCode()
        {
            var orderId = HttpContext.Session.GetInt32("OrderID");
            if(orderId != null)
            { 
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(orderId);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PutAsync("https://localhost:7202/api/Orders/GenerateCompletionCode", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("CompletionCode");
                }

                return View();
            }

            return RedirectToAction("CustomerTableList", "CustomerTable");
        }

        [HttpGet]
        public async Task<IActionResult> CompletionCode()
        {
            var orderId = HttpContext.Session.GetInt32("OrderID");
            if (orderId != null)
            {

                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7202/api/Orders/GetCompletionCode/" + orderId);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    ViewBag.CompletionCodeData = jsonData;
                    HttpContext.Session.Remove("OrderID");
                    return View();
                }

                return View();
            }

            return RedirectToAction("CustomerTableList", "CustomerTable");
        }
    }
}
