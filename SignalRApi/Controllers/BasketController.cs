using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.BasketDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        [HttpGet("GetBasketByMenuTableNumber")]
        public IActionResult GetBasketByMenuTableNumber(int id)
        {
            var value = _mapper.Map<List<ResultBasketListWithProducts>>(_basketService.TGetBasketByMenuTableNumber(id));
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateBasket(CreateBasketDto createBasketDto)
        {
            var value = _mapper.Map<Basket>(createBasketDto);
            _basketService.TAdd(value);
            return Ok("Ürün Sepete Eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBasket(int id)
        {
            var value = _basketService.TGetByID(id);
            if (value != null)
            {
                _basketService.TDelete(value);
                return Ok("Ürün Sepetten Çıkarıldı");
            }
            return NotFound("İçerik bulunamadı");
        }
    }
}
