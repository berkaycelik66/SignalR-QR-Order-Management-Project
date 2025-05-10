using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.OrderDetailDto;
using SignalR.DtoLayer.OrderDto;

namespace SignalRApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMoneyCaseService _moneyCaseService;
        private readonly IMenuTableService _menuTableService;
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMapper _mapper;


        private static int clientCount = 0;

        public SignalRHub(ICategoryService categoryService, IProductService productService, IOrderService orderService, IMoneyCaseService moneyCaseService, IMenuTableService menuTableService, IBookingService bookingService, INotificationService notificationService, IMapper mapper, IOrderDetailService orderDetailService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _moneyCaseService = moneyCaseService;
            _menuTableService = menuTableService;
            _bookingService = bookingService;
            _notificationService = notificationService;
            _mapper = mapper;
            _orderDetailService = orderDetailService;
        }

        public async Task SendStatistic()
        {
            var categoryCount = _categoryService.TCategoryCount();
            await Clients.All.SendAsync("ReceiveCategoryCount", categoryCount);

            var productCount = _productService.TProductCount();
            await Clients.All.SendAsync("ReceiveProductCount", productCount);

            var activeValue = _categoryService.TActiveCategoryCount();
            await Clients.All.SendAsync("ReceiveActiveCategoryCount", activeValue);

            var passiveValue = _categoryService.TPassiveCategoryCount();
            await Clients.All.SendAsync("ReceivePassiveCategoryCount", passiveValue);

            var hamburgerCount = _productService.TProductCountByCategoryNameHamburger();
            await Clients.All.SendAsync("ReceiveHamburgerCount", hamburgerCount);

            var drinkCount = _productService.TProductCountByCategoryNameDrink();
            await Clients.All.SendAsync("ReceiveDrinkCount", drinkCount);

            var averageProductPrice = _productService.TAverageProductPrice();
            await Clients.All.SendAsync("ReceiveAverageProductPrice", averageProductPrice.ToString("0.00") + " ₺");

            var maxPriceProduct = _productService.TProductNameByMaxPrice();
            await Clients.All.SendAsync("ReceiveProductNameByMaxPrice", maxPriceProduct);

            var minPriceProduct = _productService.TProductNameByMinPrice();
            await Clients.All.SendAsync("ReceiveProductNameByMinPrice", minPriceProduct);

            var averageHamburgerPrice = _productService.TAverageProductPriceByHamburger();
            await Clients.All.SendAsync("ReceiveAverageProductPriceByHamburger", averageHamburgerPrice.ToString("0.00") +" ₺");

            var totalOrderCount = _orderService.TTotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", totalOrderCount);

            var activeOrderCount = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", activeOrderCount);

            var lastOrderPrice = _orderService.TLastOrderPrice();
            await Clients.All.SendAsync("ReceiveLastOrderPrice", lastOrderPrice);

            var totalMoneyCaseAmount = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", totalMoneyCaseAmount.ToString("0.00") + " ₺");

            var todayTotalPrice = _orderService.TTodayTotalPrice();
            await Clients.All.SendAsync("ReceiveTodayTotalPrice", todayTotalPrice.ToString("0.00") + " ₺");

            var menuTableCount = _menuTableService.TMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", menuTableCount);
        }

        public async Task SendProgress()
        {
            var totalMoneyCaseAmount = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", totalMoneyCaseAmount.ToString("0.00") + " ₺");

            var activeOrderCount = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", activeOrderCount);

            var totalMenuTableCount = _menuTableService.TMenuTableCount();
            var occupiedMenuTableCount = _menuTableService.TOccupiedMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", totalMenuTableCount, occupiedMenuTableCount);

            var avgProductPrice = _productService.TAverageProductPrice();
            await Clients.All.SendAsync("ReceiveAverageProductPrice", avgProductPrice.ToString(".00") + " ₺");

            var totalProductCount = _productService.TProductCount();
            await Clients.All.SendAsync("ReceiveTotalProductCount", totalProductCount);

            var lastOrderPrice = _orderService.TLastOrderPrice();
            await Clients.All.SendAsync("ReceiveLastOrderPrice", lastOrderPrice.ToString(".00") + " ₺");

            var totalCategoryCount = _categoryService.TCategoryCount();
            await Clients.All.SendAsync("ReceiveTotalCategoryCount", totalCategoryCount);

            var totalBookingCount = _bookingService.TTotalBookingCount();
            await Clients.All.SendAsync("ReceiveTotalBookingCount", totalBookingCount);

            var monthlyBookingCount = _bookingService.TMonthlyBookingCount();
            await Clients.All.SendAsync("ReceiveMonthlyBookingCount", monthlyBookingCount);

            var monthlyTotalPrice = _orderService.TMonthlyTotalPrice();
            await Clients.All.SendAsync("ReceiveMonthlyTotalPrice", monthlyTotalPrice);

            var totalProductPrice = _productService.TTotalProductPrice();
            await Clients.All.SendAsync("ReceiveTotalProductPrice", totalProductPrice.ToString(".00") + " ₺");

            var monthlyTotalOrderCount = _orderService.TMonthlyTotalOrderCount();
            await Clients.All.SendAsync("ReceiveMonthlyTotalOrderCount", monthlyTotalOrderCount);
        }

        public async Task SendBookingList()
        {
            var bookingList = _bookingService.TGetListAll();
            await Clients.All.SendAsync("ReceiveBookingList", bookingList);
        }

        public async Task SendNotification()
        {
            var unreadNotificationCount = _notificationService.TNotificationCountByStatusFalse();
            await Clients.All.SendAsync("ReceiveUnreadNotificationCount", unreadNotificationCount);

            var unreadNotificationList = _notificationService.TGetAllNotificationByStatusFalse();
            await Clients.All.SendAsync("ReceiveUnreadNotificaitonList", unreadNotificationList);
        }

        public async Task SendMenuTableList()
        {
            var menuTableList = _menuTableService.TGetListAll();
            await Clients.All.SendAsync("ReceiveMenuTableList", menuTableList);

            var totalMenuTableCount = _menuTableService.TMenuTableCount();
            var occupiedMenuTableCount = _menuTableService.TOccupiedMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", totalMenuTableCount, occupiedMenuTableCount);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendKitchenOrderDetailList()
        {
            var values = _orderService.TGetActiveOrders();
            var activeOrderDtos = _mapper.Map<List<ResultOrderForKitchenDto>>(values);
            await Clients.All.SendAsync("ReceiveOrderDetails", activeOrderDtos);
        }

        public async Task SendBasketOrderDetailList(string orderId)
        {
            var values = _mapper.Map<List<ResultOrderDetailByOrderIdWithProducts>>(_orderDetailService.TOrderDetailListByOrderIdWithProducts(int.Parse(orderId)));

            await Clients.All.SendAsync("ReceiveOrderDetailList", values);
        }
        public override async Task OnConnectedAsync()
        {
            clientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
