using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.OrderDetailDto;
using SignalR.DtoLayer.OrderDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMoneyCaseService _moneyCaseService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper, IMoneyCaseService moneyCaseService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _moneyCaseService = moneyCaseService;
        }

        [HttpGet]
        public IActionResult OrderList()
        {
            var values = _orderService.TGetListAll();

            return Ok(_mapper.Map<List<ResultOrderDto>>(values));
        }

        [HttpGet("GetActiveOrders")]
        public IActionResult GetActiveOrders()
        {
            var values = _orderService.TGetActiveOrders();

            var activeOrderDtos = _mapper.Map<List<ResultOrderForKitchenDto>>(values);

            return Ok(activeOrderDtos);
        }

        [HttpGet("GetOrderDetailByMenuTableId/{id}")]
        public IActionResult GetOrderDetailByMenuTableId(int id)
        {
            var values = _orderService.TGetOrderDetailByMenuTableId(id);

            var orderByMenuTableId = _mapper.Map<List<ResultOrderForPayment>>(values);

            return Ok(orderByMenuTableId);
        }

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderDto createOrderDto)
        {
            var value = _mapper.Map<Order>(createOrderDto);

            _orderService.TAdd(value);

            return Ok("Ekleme işlemi başarılı.");
        }

        [HttpPost("CreateOrderReturnOrderID")]
        public IActionResult CreateOrderReturnOrderID(CreateOrderDto createOrderDto)
        {
            var value = _mapper.Map<Order>(createOrderDto);
            var id = _orderService.TCreateOrderReturnOrderID(value);

            return Ok(id);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var value = _orderService.TGetByID(id);

            if (value != null)
            {
                _orderService.TDelete(value);

                return Ok("Silindi.");
            }

            return NotFound("İçerik bulunamadı");
        }

        [HttpPut]
        public IActionResult UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            var value = _mapper.Map<Order>(updateOrderDto);
            _orderService.TUpdate(value);
            _moneyCaseService.TSumTotalMoneyCase();
            return Ok("Güncellendi.");
        }

        [HttpPut("UpdatePayment")]
        public IActionResult UpdatePayment([FromBody] int id)
        {
            _orderService.TUpdatePayment(id);
            _moneyCaseService.TSumTotalMoneyCase();
            return Ok("Ödeme Gerçekleşti.");
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var value = _orderService.TGetByID(id);
            return Ok(_mapper.Map<GetOrderDto>(value));
        }

        [HttpGet("TotalOrderCount")]
        public IActionResult TotalOrderCount()
        {
            return Ok(_orderService.TTotalOrderCount());
        }

        [HttpGet("ActiveOrderCount")]
        public IActionResult ActiveOrderCount()
        {
            return Ok(_orderService.TActiveOrderCount());
        }

        [HttpGet("LastOrderPrice")]
        public IActionResult LastOrderPrice()
        {
            return Ok(_orderService.TLastOrderPrice());
        }

        [HttpGet("TodayTotalPrice")]
        public IActionResult TodayTotalPrice()
        {
            return Ok(_orderService.TTodayTotalPrice());
        }

    }
}
