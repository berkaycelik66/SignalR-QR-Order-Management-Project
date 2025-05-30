﻿using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Concrete
{
    public class OrderDetailManager : IOrderDetailService
    {
        private readonly IOrderDetailDal _orderDetailDal;

        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }

        public void TAdd(OrderDetail entity)
        {
            _orderDetailDal.Add(entity);
        }

        public void TChangeDeliveryStatusToCooking(int id)
        {
            _orderDetailDal.ChangeDeliveryStatusToCooking(id);
        }

        public void TChangeDeliveryStatusToReady(int id)
        {
            _orderDetailDal.ChangeDeliveryStatusToReady(id);
        }

        public void TDelete(OrderDetail entity)
        {
            _orderDetailDal.Delete(entity);
        }

        public OrderDetail TGetByID(int id)
        {
            return _orderDetailDal.GetByID(id);
        }

        public List<OrderDetail> TGetListAll()
        {
            return _orderDetailDal.GetListAll();
        }

        public List<OrderDetail> TOrderDetailListByOrderIdWithProducts(int id)
        {
            return _orderDetailDal.OrderDetailListByOrderIdWithProducts(id);
        }

        public void TUpdate(OrderDetail entity)
        {
            _orderDetailDal.Update(entity);
        }
    }
}
