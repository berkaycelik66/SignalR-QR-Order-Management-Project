using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public int TActiveOrderCount()
        {
            return _orderDal.ActiveOrderCount();
        }

        public void TAdd(Order entity)
        {
            _orderDal.Add(entity);
        }

        public int TCreateOrderReturnOrderID(Order entity)
        {
            return _orderDal.CreateOrderReturnOrderID(entity);
        }

        public void TDelete(Order entity)
        {
            _orderDal.Delete(entity);
        }

        public void TGenerateCompletionCode(int id)
        {
            _orderDal.GenerateCompletionCode(id);
        }

        public List<Order> TGetActiveOrders()
        {
            return _orderDal.GetActiveOrders();
        }

        public Order TGetByID(int id)
        {
            return _orderDal.GetByID(id);
        }

        public string TGetCompletionCode(int id)
        {
            return _orderDal.GetCompletionCode(id);
        }

        public List<Order> TGetListAll()
        {
            return _orderDal.GetListAll();
        }

        public List<Order> TGetOrderDetailByMenuTableId(int id)
        {
            return _orderDal.GetOrderDetailByMenuTableId(id);
        }

        public decimal TLastOrderPrice()
        {
            return _orderDal.LastOrderPrice();
        }

        public int TMonthlyTotalOrderCount()
        {
            return _orderDal.MonthlyTotalOrderCount();
        }

        public decimal TMonthlyTotalPrice()
        {
            return _orderDal.MonthlyTotalPrice();
        }

        public void TSumTotalOrderDetailById(int id)
        {
            _orderDal.SumTotalOrderDetailById(id);
        }

        public decimal TTodayTotalPrice()
        {
            return _orderDal.TodayTotalPrice();
        }

        public int TTotalOrderCount()
        {
            return _orderDal.TotalOrderCount();
        }

        public void TUpdate(Order entity)
        {
            _orderDal.Update(entity);
        }

        public void TUpdatePayment(int id)
        {
            _orderDal.UpdatePayment(id);
        }
    }
}
