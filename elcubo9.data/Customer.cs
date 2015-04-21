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
    public class Customer : CustomerBinding
    {
        public virtual List<CustomerUser> CustomerUsers { get; set; }
    }
    public class CustomerUser
    {
        [Key, Column(Order = 1)]
        public int CustomerID { get; set; }
        [Key, Column(Order = 2)]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
        public virtual List<CustomerUserRol> CustomerUserRoles { get; set; }
    }
    public class CustomerUserRol
    {
        [Key, Column(Order = 1), ForeignKey("CustomerUser")]
        public int CustomerID { get; set; }
        [Key, Column(Order = 2), ForeignKey("CustomerUser")]
        public string UserID { get; set; }
        [Key, Column(Order = 3)]
        public CustomerUserType CustomerUserType { get; set; }
        public virtual CustomerUser CustomerUser { get; set; }
    }
    public class CustomerEmail : CustomerEmailBase
    {
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
    }
}
