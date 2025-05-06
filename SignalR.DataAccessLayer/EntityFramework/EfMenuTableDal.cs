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
    public class EfMenuTableDal : GenericRepository<MenuTable>, IMenuTableDal
    {
        public EfMenuTableDal(SignalRContext context) : base(context)
        {
        }

        public void ChangeMenuTableStatusToFalse(int id)
        {
            using var context = new SignalRContext();
            var value = context.MenuTables.Find(id);
            if(value != null)
            {
                value.Status = false;
                context.SaveChanges();
            }
        }

        public void ChangeMenuTableStatusToTrue(int id)
        {
            using var context = new SignalRContext();
            var value = context.MenuTables.Find(id);
            if (value != null)
            {
                value.Status = true;
                context.SaveChanges();
            }
        }


        public int MenuTableCount()
        {
            using var context = new SignalRContext();
            return context.MenuTables.Count();
        }

        public int OccupiedMenuTableCount()
        {
            using var context = new SignalRContext();
            return context.MenuTables.Where(x => x.Status == true).Count();
        }
    }
}
