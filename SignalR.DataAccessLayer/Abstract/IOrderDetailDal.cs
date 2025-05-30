﻿using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface IOrderDetailDal : IGenericDal<OrderDetail>
    {
        List<OrderDetail> OrderDetailListByOrderIdWithProducts(int id);
        void ChangeDeliveryStatusToCooking(int id);
        void ChangeDeliveryStatusToReady(int id);
    }
}
