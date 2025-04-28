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
    public class EfBookingDal : GenericRepository<Booking>, IBookingDal
    {
        public EfBookingDal(SignalRContext context) : base(context)
        {
        }

        public void BookingStatusApproved(int id)
        {
            using var context = new SignalRContext();
            var value = context.Bookings.Find(id);
            if(value != null)
            {
                value.Description = "Rezervasyon Onaylandı";
                context.SaveChanges();
            }
        }

        public void BookingStatusCanceled(int id)
        {
            using var context = new SignalRContext();
            var value = context.Bookings.Find(id);
            if (value != null)
            {
                value.Description = "Rezervasyon İptal Edildi";
                context.SaveChanges();
            }
        }

        public int MonthlyBookingCount()
        {
            using var context = new SignalRContext();
            return context.Bookings.Where(x => x.Date.Month == DateTime.UtcNow.Month).Count();
        }

        public int TotalBookingCount()
        {
            using var context = new SignalRContext();
            return context.Bookings.Count();
        }
    }
}
