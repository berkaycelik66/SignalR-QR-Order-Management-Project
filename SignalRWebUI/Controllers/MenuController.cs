using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignalRWebUI.Dtos.OrderDetailDtos;
using SignalRWebUI.Dtos.OrderDtos;
using SignalRWebUI.Dtos.ProductDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous]
    public class MenuController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MenuController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            using var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7202/api/Product/ProductListWithCategory");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket([FromBody]CreateOrderDetailDto createOrderDetailDto)
        {
            var orderID = HttpContext.Session.GetInt32("OrderID");
            if (orderID.HasValue)
            {
                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" + orderID.Value);
                createOrderDetailDto.OrderID = orderID.Value;
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createOrderDetailDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7202/api/OrderDetail", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    GetOrder(createOrderDetailDto.OrderID);
                    return RedirectToAction("Index");
                }
            }
            return Json(createOrderDetailDto);
        }

        public async Task<IActionResult> ChangeMenuTableStatusToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(id);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7202/api/MenuTables/ChangeMenuTableStatusToTrue", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async void GetOrder(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7202/api/Orders/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetOrderDto>(jsonData);
                if(value != null )
                {
                    await ChangeMenuTableStatusToTrue(value.MenuTableID);
                }
            }
        }
    }
}
