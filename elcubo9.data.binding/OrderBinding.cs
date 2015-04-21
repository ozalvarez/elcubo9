using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.data.binding
{
    public enum OrderStatus
    {
        SENT=1,
        RECEIVED=2,
        DELIVERED=3,
        INVALID=4,
        PRINTPENDING=5
    }
    public class OrderBase
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public int CustomerID { get; set; }
        public string TableNumber { get; set; }
        public DateTime DateOrder { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public bool Received { get; set; }
        public DateTime DateReceived { get; set; }

    }
    public class OrderBinding : OrderBase
    {
        public string NameOfUser { get; set; }
        public IEnumerable<OrderItemBinding> Items { get; set; }
    }
    public class OrderAllBinding : OrderBinding
    {
        public int? TimeInReceive { get; set; }
        public string CustomerName { get; set; }
    }
    public class OrderItemBinding
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public float Price { get; set; }
        public int MenuID { get; set; }
        public int Quantity { get; set; }
        public string AdditionalInfo { get; set; }
        public IEnumerable<OrderItemAdditionalBinding> Items { get; set; }

    }
    public class OrderItemAdditionalBinding
    {
        public float Price { get; set; }
        public string AdditionalItemName { get; set; }
        public int AdditionalItemID { get; set; }
    }
    public class ChangeStatusBinding
    {
        public int OrderID { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
