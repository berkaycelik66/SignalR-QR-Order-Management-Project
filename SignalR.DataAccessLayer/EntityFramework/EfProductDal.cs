﻿using Microsoft.EntityFrameworkCore;
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
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        public EfProductDal(SignalRContext context) : base(context)
        {
        }

        public decimal AverageProductPrice()
        {
            using var context = new SignalRContext();

            return context.Products.Average(x => x.Price);
        }

        public List<Product> GetProductsWithCategories()
        {
            var context = new SignalRContext();

            var values = context.Products.Include(x => x.Category).ToList();

            return values;
        }

        public int ProductCount()
        {
            using var context = new SignalRContext();

            return context.Products.Count();
        }

        public int ProductCountByCategoryNameDrink()
        {
            using var context = new SignalRContext();

            return context.Products.Where(x => x.CategoryID == (context.Categories.Where(y => y.CategoryName == "İçecek").Select(z => z.CategoryID)).FirstOrDefault()).Count();
        }

        public int ProductCountByCategoryNameHamburger()
        {
            using var context = new SignalRContext();

            return context.Products.Where(x => x.CategoryID == (context.Categories.Where(y => y.CategoryName == "Hamburger").Select(z => z.CategoryID)).FirstOrDefault()).Count();
        }

        public string ProductNameByMaxPrice()
        {
            using var context = new SignalRContext();

            return context.Products.Where(x => x.Price == (context.Products.Max(x => x.Price))).Select(x => x.ProductName).FirstOrDefault()!;
        }

        public string ProductNameByMinPrice()
        {
            using var context = new SignalRContext();

            return context.Products.Where(x => x.Price == (context.Products.Min(x => x.Price))).Select(x => x.ProductName).FirstOrDefault()!;
        }

        public decimal AverageProductPriceByHamburger()
        {
            using var context = new SignalRContext();
            return context.Products.Where(x => x.CategoryID == (context.Categories.Where(x => x.CategoryName == "Hamburger").Select(x => x.CategoryID).FirstOrDefault())).Average(x=> x.Price);
        }

        public decimal TotalProductPrice()
        {
            using var context = new SignalRContext();
            return context.Products.Sum(x => x.Price);
        }
    }
}
