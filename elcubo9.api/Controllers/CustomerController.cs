using elcubo9.bll;
using elcubo9.bll.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using elcubo9.data.binding;

namespace elcubo9.api.Controllers
{
    [Authorize]
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        CustomerBLL CustomerService = new CustomerBLL();

        #region WEB
        public IHttpActionResult Get()
        {
            var Id = User.Identity.GetUserId();
            return Ok(CustomerService.Get());
        }
        #endregion

        #region Customer
        [Route("by-user")]
        public IHttpActionResult GetByUser()
        {
            return Ok(CustomerService.Get(User.Identity.GetUserId()));
        }
        [Route("email")]
        public IHttpActionResult GetEmails(int CustomerID)
        {
            return Ok(CustomerService.GetEmails(CustomerID, User.Identity.GetUserId()));
        }
        [Route("email")]
        public IHttpActionResult PostAddEmail(CustomerEmailBinding Model)
        {
            CustomerService.AddEmail(Model, User.Identity.GetUserId());
            return Ok();
        }
        [Route("email")]
        public IHttpActionResult DeleteEmail(int CustomerEmailID)
        {
            CustomerService.DeleteEmail(CustomerEmailID, User.Identity.GetUserId());
            return Ok();
        }
        #endregion

        #region Admin
        [Authorize(Roles = "admin")]
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            return Ok(CustomerService.GetAll());
        }
        [Authorize(Roles = "admin")]
        public IHttpActionResult PostSave(CustomerBinding Model)
        {
            CustomerService.Save(Model);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        public IHttpActionResult Delete(int CustomerID)
        {
            CustomerService.Delete(CustomerID);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("enable")]
        public IHttpActionResult PostEnable(CustomerEnabledBinding Model)
        {
            CustomerService.Enable(Model);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("user")]
        public IHttpActionResult PostAddUser(AddUserInCustomerBinding Model)
        {
                CustomerService.AddUser(Model);
                return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("users")]
        public IHttpActionResult GetUsers(int CustomerID)
        {
            return Ok(CustomerService.GetUsers(CustomerID));
        }
        [Authorize(Roles = "admin")]
        [Route("rol")]
        public IHttpActionResult PostAddRol(CustomerAddRol Model)
        {
            CustomerService.AddUserToRol(Model);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("user")]
        public IHttpActionResult DeleteUser(int CustomerID, String UserID)
        {
            CustomerService.DeleteUser(UserID, CustomerID);
            return Ok();
        }
        #endregion
    }
}
