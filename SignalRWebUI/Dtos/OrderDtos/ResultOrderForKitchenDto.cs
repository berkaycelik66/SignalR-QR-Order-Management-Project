
using SignalRWebUI.Dtos.OrderDetailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRWebUI.Dtos.OrderDtos
{
    public class ResultOrderForKitchenDto
    {
        public int OrderID { get; set; }
        public int MenuTableID { get; set; }
        public string? MenuTableName { get; set; }
        public bool MenuTableStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ResultOrderDetailForKitchenDto>? OrderDetails { get; set; }
    }
}
