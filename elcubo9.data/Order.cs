using elcubo9.data.binding;
using elcubo9.data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.data
{
    public class Order : OrderBase
    {

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
        public virtual List<OrderMenu> OrderMenus { get; set; }
    }
    public class OrderMenu
    {
        public int OrderMenuID { get; set; }
        public int OrderID { get; set; }
        public int MenuID { get; set; }
        public int Quantity { get; set; }
        public string AdditionalInfo { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
        [ForeignKey("MenuID")]
        public virtual Menu Menu { get; set; }
        public virtual List<OrderMenuAdditional> OrderMenuAdditionals { get; set; }
    }
    public class OrderMenuAdditional
    {
        public int OrderMenuAdditionalID { get; set; }
        public int OrderMenuID { get; set; }
        public int AdditionalItemID { get; set; }

        [ForeignKey("OrderMenuID")]
        public virtual OrderMenu OrderMenu { get; set; }
        [ForeignKey("AdditionalItemID")]
        public virtual AdditionalItem AdditionalItem { get; set; }
    }
    public class OrderEmailSent
    {
        [Key, Column(Order = 1)]
        public int CustomerEmailID { get; set; }
        [Key, Column(Order = 2)]
        public int OrderID { get; set; }

        [ForeignKey("CustomerEmailID")]
        public virtual CustomerEmail CustomerEmail { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

    }
}
