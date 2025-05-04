using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.OrderDetailDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailService orderDetailService, IMapper mapper, IOrderService orderService)
        {
            _orderDetailService = orderDetailService;
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult OrderDetailList()
        {
            var values = _orderDetailService.TGetListAll();

            return Ok(_mapper.Map<List<ResultOrderDetailDto>>(values));
        }

        [HttpGet("OrderDetailListByOrderIdWithProducts/{id}")]
        public IActionResult OrderDetailListByOrderIdWithProducts(int id)
        {
            var values = _orderDetailService.TOrderDetailListByOrderIdWithProducts(id);

            return Ok(_mapper.Map<List<ResultOrderDetailByOrderIdWithProducts>>(values));
        }

        [HttpPost]
        public IActionResult CreateOrderDetail(CreateOrderDetailDto createOrderDetailDto)
        {
            var value = _mapper.Map<OrderDetail>(createOrderDetailDto);
            _orderDetailService.TAdd(value);

            _orderService.TSumTotalOrderDetailById(createOrderDetailDto.OrderID);

            return Ok("Ekleme işlemi başarılı.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            var value = _orderDetailService.TGetByID(id);

            if (value != null)
            {
                _orderDetailService.TDelete(value);
                _orderService.TSumTotalOrderDetailById(value.OrderID);

                return Ok("Silindi.");
            }

            return NotFound("İçerik bulunamadı");
        }

        [HttpPut]
        public IActionResult UpdateOrderDetail(UpdateOrderDetailDto updateOrderDetailDto)
        {
            var value = _mapper.Map<OrderDetail>(updateOrderDetailDto);
            _orderDetailService.TUpdate(value);
            return Ok("Güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetail(int id)
        {
            var value = _orderDetailService.TGetByID(id);
            return Ok(_mapper.Map<GetOrderDetailDto>(value));
        }
    }
}
