using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRWebUI.Dtos.OrderDetailDtos
{
    public class ResultOrderDetailForKitchenDto
    {
        public int OrderDetailID { get; set; }
        public string? ProductName { get; set; }
        public int Count { get; set; }
        public string? DeliveryStatus { get; set; }
    }
}
