using SignalRWebUI.Dtos.OrderDetailDtos;

namespace SignalRWebUI.Dtos.OrderDtos
{
    public class ResultOrderForPayment
    {
        public int OrderID { get; set; }
        public int MenuTableID { get; set; }
        public string? MenuTableName { get; set; }
        public bool MenuTableStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? CompletionCode { get; set; }
        public List<ResultOrderDetailForPayment>? OrderDetails { get; set; }
    }
}
