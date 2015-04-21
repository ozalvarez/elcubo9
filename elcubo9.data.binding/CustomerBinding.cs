using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.data.binding
{
    public enum CustomerUserType
    {
        Orders = 1, Menu = 2, Print=3, Configuration=4
    }
    public class CustomerBinding
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public bool Enabled { get; set; }
        public DateTime DateCreated { get; set; }
        public string CustomerImg { get; set; }
        public string Symbol { get; set; }
    }
    public class CustomerByUserBinding : CustomerBinding
    {
        public IEnumerable<CustomerUserType> Roles { get; set; }
    }
    public class CustomerEnabledBinding
    {
        public int CustomerID { get; set; }
        public bool Enabled { get; set; }
    }
    public class AddUserInCustomerBinding
    {
        public string Email { get; set; }
        public int CustomerID { get; set; }
    }
    public class CustomerListUsers
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<CustomerUserType> Roles { get; set; }
    }
    public class CustomerAddRol
    {
        public string UserID { get; set; }
        public int CustomerID { get; set; }
        public bool Enabled { get; set; }
        public CustomerUserType CustomerUserType { get; set; }
    }
    public class CustomerEmailBase
    {
        public int CustomerEmailID { get; set; }
        public int CustomerID { get; set; }
        public string Email { get; set; }
    }
    public class CustomerEmailBinding : CustomerEmailBase
    {

    }
}
