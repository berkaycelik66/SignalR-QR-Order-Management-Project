using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using QRCoder;
using SignalRWebUI.Dtos.MenuTableDtos;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;

namespace SignalRWebUI.Controllers
{
    public class QRCodeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public QRCodeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7202/api/MenuTables");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultMenuTableDto>>(jsonData);
            List<SelectListItem> menuTables = (from x in values
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = "SignalRRestaurant: " + x.Name
                                               }).ToList();
            ViewBag.MenuTables = menuTables;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string value)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                QRCodeGenerator createQrCode = new QRCodeGenerator();
                QRCodeGenerator.QRCode qrCode = createQrCode.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
                using (Bitmap image = qrCode.GetGraphic(10))
                {
                    image.Save(memoryStream, ImageFormat.Png);
                    ViewBag.QrCodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7202/api/MenuTables");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultMenuTableDto>>(jsonData);
            List<SelectListItem> menuTables = (from x in values
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = "SignalRRestaurant: " + x.Name,
                                                   Selected = !string.IsNullOrEmpty(x.Name) && value.Contains(x.Name) ? true : false
                                               }).ToList();
            ViewBag.MenuTables = menuTables;
            return View();
        }
    }
}
