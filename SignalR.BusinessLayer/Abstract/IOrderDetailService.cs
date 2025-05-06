using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IOrderDetailService : IGenericService<OrderDetail>
    {
        List<OrderDetail> TOrderDetailListByOrderIdWithProducts(int id);
        void TChangeDeliveryStatusToCooking(int id);
        void TChangeDeliveryStatusToReady(int id);
    }
}
