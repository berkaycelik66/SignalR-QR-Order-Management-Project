using SignalR.DtoLayer.OrderDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DtoLayer.OrderDto
{
    public class ResultOrderForPayment
    {
        public int OrderID { get; set; }
        public int MenuTableID { get; set; }
        public string? MenuTableName { get; set; }
        public bool MenuTableStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ResultOrderDetailForPayment>? OrderDetails { get; set; }
    }
}
