using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IOrderService : IGenericService<Order>
    {
        int TTotalOrderCount();
        int TActiveOrderCount();
        decimal TLastOrderPrice();
        decimal TTodayTotalPrice();
        decimal TMonthlyTotalPrice();
        int TMonthlyTotalOrderCount();
        void TSumTotalOrderDetailById(int id);
        List<Order> TGetActiveOrders();
        List<Order> TGetOrderDetailByMenuTableId(int id);
        void TUpdatePayment(int id);
        int TCreateOrderReturnOrderID(Order entity);
    }
}
