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
    public class EfOrderDetailDal : GenericRepository<OrderDetail>, IOrderDetailDal
    {
        public EfOrderDetailDal(SignalRContext context) : base(context)
        {
        }

        public void ChangeDeliveryStatusToCooking(int id)
        {
            using var context = new SignalRContext();
            var value = context.OrderDetails.Find(id);
            if(value != null)
            {
                value.DeliveryStatus = "Pişirmede";
                context.SaveChanges();
            }
        }

        public void ChangeDeliveryStatusToReady(int id)
        {
            using var context = new SignalRContext();
            var value = context.OrderDetails.Find(id);
            if (value != null)
            {
                value.DeliveryStatus = "Hazırlandı";
                context.SaveChanges();
            }
        }

        public List<OrderDetail> OrderDetailListByOrderIdWithProducts(int id)
        {
            using var context = new SignalRContext();
            return context.OrderDetails.Include(x => x.Product).Where(x => x.OrderID == id).ToList();
        }
    }
}
