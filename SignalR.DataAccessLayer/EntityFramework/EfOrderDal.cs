using Microsoft.EntityFrameworkCore;
using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfOrderDal : GenericRepository<Order>, IOrderDal
    {
        public EfOrderDal(SignalRContext context) : base(context)
        {
        }

        public int ActiveOrderCount()
        {
            using var context = new SignalRContext();
            return context.Orders.Where(x => x.Description == "Aktif").Count();
        }

        public List<Order> GetActiveOrders()
        {
            using var context = new SignalRContext();
            return context.Orders
                .Where(o => o.Description == "Aktif")
                .Include(od => od.OrderDetails!)
                .ThenInclude( p => p.Product)
                .Include(mt => mt.MenuTable)
                .ToList();
        }

        public List<Order> GetOrderDetailByMenuTableId(int id)
        {
            using var context = new SignalRContext();
            return context.Orders
                .Where(o => o.Description == "Aktif" && o.MenuTableID == id)
                .Include(od => od.OrderDetails!)
                .ThenInclude(p => p.Product)
                .Include(mt => mt.MenuTable)
                .ToList();
        }

        public void UpdatePayment(int id)
        {
            using var context = new SignalRContext();
            var value = context.Orders.Find(id);
            if(value != null)
            {
                var valueMenuTable = context.MenuTables.Where(x => x.MenuTableID == value.MenuTableID).FirstOrDefault();
                if(valueMenuTable != null)
                {
                    value.Description = "Ödendi";
                    valueMenuTable.Status = false;
                    context.SaveChanges();
                }
            }
        }

        public decimal LastOrderPrice()
        {
            using var context = new SignalRContext();
            return context.Orders.OrderByDescending(x => x.OrderID).Take(1).Select(x => x.TotalPrice).FirstOrDefault();
        }

        public int MonthlyTotalOrderCount()
        {
            using var context = new SignalRContext();
            return context.Orders.Where(x => x.Date.Month == DateTime.Now.Month).Count();
        }

        public decimal MonthlyTotalPrice()
        {
            using var context = new SignalRContext();
            return context.Orders.Where(x => x.Date.Month == DateTime.Now.Month && x.Description == "Ödendi").Sum(x => x.TotalPrice);
        }

        public void SumTotalOrderDetailById(int id)
        {
            using var context = new SignalRContext();

            var order = context.Orders.Find(id);
            if (order != null)
            {
                order.TotalPrice = context.OrderDetails.Where(x => x.OrderID == id).Sum(x => x.TotalPrice);
                context.SaveChanges();
            }
        }

        public decimal TodayTotalPrice()
        {
            using var context = new SignalRContext();
            return context.Orders.Where(x => x.Date.Date == DateTime.Now.Date && x.Description == "Ödendi").Sum(x => x.TotalPrice);
        }

        public int TotalOrderCount()
        {
            using var context = new SignalRContext();
            return context.Orders.Count();
        }

        public int CreateOrderReturnOrderID(Order entity)
        {
            using var context = new SignalRContext();
            context.Add(entity);
            context.SaveChanges();
            return entity.OrderID;
        }

        public void GenerateCompletionCode(int id)
        {
            using var context = new SignalRContext();
            var completionCode = new Random().Next(100000, 999999).ToString();
            var value = context.Orders.Find(id);
            if(value != null)
            {
                value.CompletionCode = completionCode;
                context.SaveChanges();
            }

        }

        public string GetCompletionCode(int id)
        {
            using var context = new SignalRContext();
            var value = context.Orders.Find(id);
            if(value != null)
            {

                return value.CompletionCode!;
            }

            return "";
        }
    }
}
