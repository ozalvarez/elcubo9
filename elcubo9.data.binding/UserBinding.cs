using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.data.binding
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string UserID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public int NumOrders { get; set; }
        public bool Block { get; set; }
    }
    public class UserBlockBinding
    {
        public string UserID { get; set; }
        public bool Block { get; set; }
    }
}
