using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface IOrderDal : IGenericDal<Order>
    {
        int TotalOrderCount();
        int ActiveOrderCount();
        decimal LastOrderPrice();
        decimal TodayTotalPrice();
        decimal MonthlyTotalPrice();
        int MonthlyTotalOrderCount();
        void SumTotalOrderDetailById(int id);
        List<Order> GetActiveOrders();
        List<Order> GetOrderDetailByMenuTableId(int id);
        void UpdatePayment(int id);
        int CreateOrderReturnOrderID(Order entity);
    }
}
