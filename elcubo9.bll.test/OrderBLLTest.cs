using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using elcubo9.data.binding;
using System.Collections.Generic;

namespace elcubo9.bll.test
{
    [TestClass]
    public class OrderBLLTest : BaseTest
    {
        private OrderBLL OrderService = new OrderBLL();
        
        [TestMethod]
        public void CreateOrderTest()
        {
            var _OrderID=OrderService.CreateOrder(new data.binding.OrderBinding
            {
                CustomerID = CUSTOMER.CustomerID,
                TableNumber = "TestTable",
                UserID = USER.Id,
                Items = new List<OrderItemBinding>()
            }, USER.Id);

            Assert.IsTrue(_OrderID > 0);
        }

        [TestMethod]
        public void GetAllOrderTest()
        {
            var _Orders = OrderService.GetOrders(40,1,0);
            Assert.IsTrue(_Orders!=null);
        }
    }
}
