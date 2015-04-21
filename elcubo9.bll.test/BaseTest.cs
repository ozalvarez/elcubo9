using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using elcubo9.data.Models;
using elcubo9.data;
using elcubo9.data.binding;
using elcubo9.bll.Exceptions;

namespace elcubo9.bll.test
{
    /// <summary>
    /// Summary description for BaseTest
    /// </summary>
    [TestClass]
    public class BaseTest
    {
        private UserBLL UserService = new UserBLL();
        private CustomerBLL CustomerService = new CustomerBLL();

        public String MYEMAIL = "oswalalvarez@gmail.com";
        public String EMAIL = "test@elcubo9.com";
        public String CUSTOMER_NAME = "TEST - CUSTOMER";

        public ApplicationUser USER;
        public ApplicationUser CUSTOMER_USER;
        public Customer CUSTOMER;
        public BaseTest()
        {
            USER = UserService.Find(EMAIL);
            if (USER == null)
            {
                USER = UserService.Create("test@elcubo9.com", "123456","Oz in Test");
            }

            CUSTOMER = CustomerService.GetCustomer(CUSTOMER_NAME);
            if (CUSTOMER == null)
                CUSTOMER = CustomerService.Save(new CustomerBinding
                {
                    CustomerName = CUSTOMER_NAME
                });
            CustomerService.AddUserToAllRoles(new AddUserInCustomerBinding
            {
                CustomerID = CUSTOMER.CustomerID,
                Email = MYEMAIL
            });
            CUSTOMER_USER = UserService.Find(MYEMAIL);
            try
            {
                CustomerService.AddEmail(new CustomerEmailBinding
                {
                    Email = MYEMAIL,
                    CustomerID = CUSTOMER.CustomerID
                }, CUSTOMER_USER.Id);
            }
            catch (RuleException)
            {
                //ALREADY ADDED
            }
        }

    }
}
